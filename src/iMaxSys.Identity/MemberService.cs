//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IMemberService.cs
//摘要: 成员服务接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Extentions;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.Algorithm;
using iMaxSys.Max.Identity;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Security.Cryptography;

using iMaxSys.Data;
using iMaxSys.Data.Entities.App;
using iMaxSys.Data.Repositories;
using iMaxSys.Identity.Common;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Repositories;
using iMaxSys.Identity.Data.Entities;
using iMaxSys.Sns;
using iMaxSys.Sns.Api;
using iMaxSys.Sns.Common.Auth;
using iMaxSys.Sns.Common.Open;

using DbMember = iMaxSys.Identity.Data.Entities.Member;
using AutoMapper.Execution;
using System.Net;
using System.Security.Cryptography;
using MD5 = iMaxSys.Max.Security.Cryptography.MD5;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace iMaxSys.Identity;

/// <summary>
/// MemberService
/// </summary>
public class MemberService : IMemberService
{
    private readonly IMapper _mapper;
    private readonly MaxOption _option;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProvider _userProvider;
    private readonly ISnsFactory _snsFactory;
    private readonly ICheckCodeService _checkCodeService;

    private IUser? _user;

    #region 构造

    public MemberService(IMapper mapper, IOptions<MaxOption> option, IUnitOfWork unitOfWork, IUserProvider userProvider, ISnsFactory snsFactory, ICheckCodeService checkCodeService)
    {
        _mapper = mapper;
        _option = option.Value;
        _unitOfWork = unitOfWork;
        _userProvider = userProvider;
        _snsFactory = snsFactory;
        _checkCodeService = checkCodeService;
    }

    #endregion

    #region GetAccessChainAsync

    /// <summary>
    /// GetAccessChainAsync
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<IAccessChain?> GetAccessChainAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }

        IMemberRepository repository = _unitOfWork.GetCustomRepository<IMemberRepository>();

        //先按Token获取uid
        IAccessSession? access = await repository.GetAccessSessionAsync(token);

        if (access == null)
        {
            return null;
        }

        //按memberId获取member
        IMember? member = null;
        IUser? user = null;

        if (access.MemberId.HasValue)
        {
            //get member
            member = await GetAsync(access.MemberId.Value);

            if (access.MemberId is not null)
            {
                //get user
                user = await _userProvider.GetAsync(access.MemberId.Value, access.Type);
            }
        }

        return new AccessChain
        {
            AccessSession = access,
            Member = member,
            User = user
        };
    }

    #endregion

    #region RegisterAsync

    public async Task<IAccessToken> RegisterAsync(RegisterModel registerModel)
    {
        bool needCheckCode = true;

        //存在检查
        var repository = _unitOfWork.GetRepository<DbMember>();
        var dbMember = await repository.FirstOrDefaultAsync(x => x.Mobile == registerModel.Mobile || x.Email == registerModel.Email || x.UserName == registerModel.UserName, null, x => x.Include(y => y.MemberExts));
        if (dbMember is not null)
        {
            throw new MaxException(ResultCode.UserExists);
        }

        //获取xppSns
        var xppSns = await _unitOfWork.GetRepository<XppSns>().FirstOrDefaultAsync(x => x.Id == registerModel.XppSnsId, null, x => x.Include(y => y.Xpp));

        if (xppSns is null)
        {
            throw new MaxException(ResultCode.XppSnsIdNotExists);
        }

        //验证mobile,email,openId,code
        if (!registerModel.Mobile.ToString().IsMobile())
        {
            throw new MaxException(ResultCode.MobileIsInvalid);
        }

        if (xppSns.Source == SnsSource.Max)
        {
            if (registerModel.UserName.IsNullOrWhiteSpace())
            {
                throw new MaxException(ResultCode.UserNameCantNull);
            }
            if (!registerModel.Password.IsStrong())
            {
                throw new MaxException(ResultCode.PasswordIsWeak);
            }
        }
        else
        {
            //有code,就不需要openId,但二者不可同时为空
            if (registerModel.Code.IsNullOrWhiteSpace() && registerModel.OpenId.IsNullOrWhiteSpace())
            {
                throw new MaxException(ResultCode.CodeOpenIdCantNull);
            }

            //社交平台获取电话号码, 则不需要验证码
            /*
            if (!registerModel.EncryptedData.IsNullOrWhiteSpace())
            {
                MemberSession? memberSession = await _unitOfWork.GetRepository<MemberSession>().FirstOrDefaultAsync(x => x.XppSnsId == registerModel.XppSnsId && x.Token == registerModel.Token);
                if (memberSession is null)
                {
                    throw new MaxException(ResultCode.AccessSessionIsNull);
                }

                SnsPhoneNumber? phoneNumber = _snsFactory.GetService(SnsSource.WeChat).GetPhoneNumber(registerModel.EncryptedData!, memberSession.SessionKey, registerModel.IV!);
                if (phoneNumber is null)
                {
                    throw new MaxException(ResultCode.GetMobileFail);
                }
                mobile = phoneNumber.PurePhoneNumber;
                needCheckCode = false;
            }
            */
        }

        //检查验证码
        if (needCheckCode)
        {
            await _checkCodeService.CheckAsync(registerModel.XppSnsId, BizSource.BindCheckCode.GetHashCode(), registerModel.Mobile.ToString(), registerModel.CheckCode);
        }

        //外接用户注册
        _user = await RegisterUserAsync(registerModel);

        //new member
        dbMember = new DbMember();
        //设置注册信息
        SetNewMember(registerModel, _user, dbMember, xppSns.Xpp.Source, xppSns.Source, registerModel.TenantId, xppSns.XppId);

        //持久化
        await repository.AddAsync(dbMember);
        await _unitOfWork.SaveChangesAsync();

        //刷新缓存
        AccessConfig? accessConfig = null;

        if (registerModel.Code is not null && !registerModel.Code.IsNullOrEmpty())
        {
            accessConfig = await GetAccessConfigAsync(xppSns, registerModel.Code);
        }

        MemberModel member = _mapper.Map<MemberModel>(dbMember);
        return await RefreshAccessChainAsync(accessConfig, xppSns, member, _user);
    }

    #endregion

    #region RefreshAccessChainAsync

    /// <summary>
    /// RefreshAccessChainAsync
    /// </summary>
    /// <param name="accessConfig"></param>
    /// <param name="xppSns"></param>
    /// <param name="member"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<IAccessToken> RefreshAccessChainAsync(AccessConfig? accessConfig, XppSns xppSns, IMember member, IUser? user)
    {
        var token = MakeAccessToken();

        //生成AccessSession
        IAccessSession accessSession = new AccessSession
        {
            XppSnsId = xppSns.Id,
            AccountId = accessConfig?.AccountId,
            AppId = accessConfig?.AppId,
            Avatar = member.Avatar,
            Name = member.Name,
            SessionKey = accessConfig?.SessionKey,
            OpenId = accessConfig?.OpenId,
            UnionId = accessConfig?.UnionId,
            TenantId = accessConfig?.TenantId,
            Token = token.Token,
            Expires = token.Expires,
            XppSource = xppSns.Xpp.Source.GetHashCode(),
            AccountSource = xppSns.Source.GetHashCode(),
            MemberId = member.Id,
            IsLogin = true,
            Status = member.Status,
            IsOfficial = member.IsOfficial
        };

        //生成AccessChain
        IAccessChain accessChain = new AccessChain
        {
            AccessSession = accessSession,
            Member = member,
            User = user
        };

        await _unitOfWork.GetCustomRepository<IMemberRepository>().RefreshAccessSessionAsync(accessChain);

        return token;
    }

    #endregion

    /*
    #region RefreshAcceeChainAsync

    /// <summary>
    /// 刷新AcceeChain缓存
    /// </summary>
    /// <param name="oldToken"></param>
    /// <param name="accessChain"></param>
    /// <returns></returns>
    public async Task RefreshAccessChainAsync(string oldToken, IAccessChain accessChain)
    {
        if (accessChain.AccessSession is not null)
        {
            await _unitOfWork.GetCustomRepository<IMemberRepository>().RefreshAccessSessionAsync(oldToken, accessChain.AccessSession, accessChain.Member, accessChain.User);
        }
        else
        {
            throw new MaxException(ResultCode.MemberNotExists);
        }
    }

    #endregion
    */

    #region AddAsync

    /// <summary>
    /// Add member
    /// </summary>
    /// <param name="model"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<IMember> AddAsync(MemberModel model, long xppId, long roleId)
    {
        DbMember dbMember = new();
        SetNewMember(model, xppId, dbMember);
        await _unitOfWork.GetCustomRepository<IMemberRepository>().AddAsync(dbMember);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<MemberModel>(dbMember);
    }

    #endregion

    #region RemoveAsync

    /// <summary>
    /// 移除成员
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    public async Task RemoveAsync(long memberId)
    {
        await _unitOfWork.GetCustomRepository<IMemberRepository>().RemoveAsync(memberId);
        await _unitOfWork.SaveChangesAsync();
    }

    #endregion

    #region GetAsync

    /// <summary>
    /// Get member
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IMember?> GetAsync(long id)
    {
        return await _unitOfWork.GetCustomRepository<IMemberRepository>().GetAsync(id);
    }

    #endregion

    #region UpdateAsync

    /// <summary>
    /// Get member
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IMember> UpdateAsync(MemberModel model)
    {
        //成员id判空
        if (model.Id == 0)
        {
            throw new MaxException(ResultCode.MemberIdCantNull);
        }

        IMemberRepository respoitory = _unitOfWork.GetCustomRepository<IMemberRepository>();
        var member = await respoitory.FindAsync(model.Id);
        if (member == null)
        {
            throw new MaxException(ResultCode.MemberNotExists);
        }

        SetMember(model, member);
        respoitory.Update(member);
        await _unitOfWork.SaveChangesAsync();

        var result = _mapper.Map<MemberModel>(member);
        await RefreshAsync(result);
        return result;
    }

    #endregion

    #region RefreshAsync

    /// <summary>
    /// 刷新member缓存
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    public async Task RefreshAsync(long memberId)
    {
        IMember? member = await GetAsync(memberId);
        if (member is not null)
        {
            await RefreshAsync(member);
        }
    }

    /// <summary>
    /// 刷新member缓存
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public async Task RefreshAsync(IMember member)
    {
        IUser user = await _userProvider.GetAsync(member.Id, member.Type);
        await _unitOfWork.GetCustomRepository<IMemberRepository>().RefreshMemberAsync(member, user);
    }

    #endregion

    #region SetMember

    /// <summary>
    /// 使用MemberModel设置DbMember
    /// </summary>
    /// <param name="model"></param>
    /// <param name="dbMember"></param>
    private static void SetMember(MemberModel model, DbMember dbMember)
    {
        dbMember.UserId = model.UserId > 0 ? model.UserId : dbMember.Id;      //如无外接UserId,使用Member.Id
        model.Name?.IfNotNull(x => dbMember.Name = x);
        model.IdNumber?.IfNotNull(x => dbMember.IdNumber = x);
        model.QuickCode?.IfNotNull(x => dbMember.QuickCode = x);
        model.Birthday?.IfNotNull(x => dbMember.Birthday = x);
        model.MaritalStatus?.IfNotNull(x => dbMember.MaritalStatus = x);
        model.Gender?.IfNotNull(x => dbMember.Gender = x);
        model.Nation?.IfNotNull(x => dbMember.Nation = x);
        model.Education?.IfNotNull(x => dbMember.Education = x);
        model.Party?.IfNotNull(x => dbMember.Party = x);
        model.UserName?.IfNotNull(x => dbMember.UserName = x, () => dbMember.UserName = dbMember.Mobile.ToString());
        model.NickName?.IfNotNull(x => dbMember.NickName = x);
        model.CountryCode?.IfNotNull(x => dbMember.CountryCode = x);
        model.Mobile?.IfNotNull(x => dbMember.Mobile = x.ToLong());
        model.Phone?.IfNotNull(x => dbMember.Phone = x);
        model.Email?.IfNotNull(x => dbMember.Email = x);
        model.Avatar?.IfNotNull(x => dbMember.Avatar = x);
        model.Country?.IfNotNull(x => dbMember.Country = x);
        model.Province?.IfNotNull(x => dbMember.Province = x);
        model.City?.IfNotNull(x => dbMember.City = x);
        model.District?.IfNotNull(x => dbMember.District = x);
        model.Street?.IfNotNull(x => dbMember.Street = x);
        model.Community?.IfNotNull(x => dbMember.Community = x);
        model.AreaCode?.IfNotNull(x => dbMember.AreaCode = x);
        model.Address?.IfNotNull(x => dbMember.Address = x);
        model.Zipcode?.IfNotNull(x => dbMember.Zipcode = x);
        model.Start?.IfNotNull(x => dbMember.Start = x);
        model.End?.IfNotNull(x => dbMember.End = x);
        dbMember.Type = model.Type;
    }

    /// <summary>
    /// 使用MemberModel设置DbMember
    /// </summary>
    /// <param name="model"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <param name="dbMember"></param>
    private static void SetNewMember(MemberModel model, long xppId, DbMember dbMember)
    {
        SetMember(model, dbMember);

        //注册or新增信息
        dbMember.TenantId = model.TenantId;

        //时间和IP
        var now = DateTime.Now;
        dbMember.JoinTime = now;
        model.JoinIp?.IfNotNull(x => dbMember.JoinIP = x);
        dbMember.LastLogin = now;
        dbMember.LastIP = dbMember.JoinIP;

        //salt
        dbMember.Salt = Guid.NewGuid().ToString().Replace("-", "");

        /*
        //角色信息(roleId为空,则指定为默认角色)
        RoleMember roleMember = new()
        {
            XppId = xppId,
            RoleId = model.RoleId ?? 0,
            Status = Status.Enable,
            TenantId = model.TenantId
        };

        dbMember.RoleMembers = new List<RoleMember>
        {
            roleMember
        };
        */
    }

    /// <summary>
    /// 设置新成员
    /// </summary>
    /// <param name="model"></param>
    /// <param name="user"></param>
    /// <param name="dbMember"></param>
    /// <param name="xppSource"></param>
    /// <param name="snsSource"></param>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    private void SetNewMember(RegisterModel model, IUser? user, DbMember dbMember, XppSource xppSource, SnsSource snsSource, long tenantId = 0, long xppId = 0)
    {
        dbMember.TenantId = user?.TenantId ?? tenantId;
        dbMember.ReferrerId = model.ReferrerId;
        dbMember.Name = ((((user?.Name ?? user?.NickName) ?? model.Name) ?? model.UserName) ?? model.NickName) ?? model.Mobile.ToString();
        dbMember.NickName = ((user?.NickName) ?? model.NickName) ?? model.Mobile.ToString();
        dbMember.Mobile = model.Mobile;
        dbMember.UserName = model.UserName ?? model.Mobile.ToString();
        dbMember.Salt = Guid.NewGuid().ToString().Replace("-", "");
        dbMember.JoinTime = DateTime.Now;
        dbMember.JoinIP = model.IP;
        dbMember.Start = DateTime.Now;
        dbMember.Gender = user?.Gender ?? model.Gender ?? Gender.Unknown;
        dbMember.Avatar = user?.Avatar ?? model.Avatar;
        dbMember.LastIP = model.IP;
        dbMember.LastLogin = DateTime.Now;
        dbMember.IsOfficial = user?.IsOfficial ?? false;
        dbMember.Status = Status.Enable;
        dbMember.Type = model.Type;
        dbMember.MemberExts = new List<MemberExt>();
        dbMember.Email = model.Email ?? string.Empty;
        dbMember.AccountSource = snsSource;
        dbMember.XppSource = xppSource;
        model.Birthday?.IfNotNull(x => dbMember.Birthday = x);
        //密码为空生产6位随机数字密码
        dbMember.Password = MakePassword(model.Password ?? MaxRandom.Next().ToString().Left(6), dbMember.Salt);

        //openId不为空, 则新增扩展成员信息
        model.OpenId?.IfNotNull(x =>
        {
            MemberExt ext = new MemberExt();
            ext.XppSnsId = model.XppSnsId;
            ext.TenantId = model.TenantId;
            ext.OpenId = model.OpenId;
            ext.UnionId = model.UnionId ?? Guid.NewGuid().ToString().Replace("-", "");
            ext.Expires = DateTime.Now.AddMinutes(_option.Identity.Expires);
            ext.Name = model.NickName;
            ext.Avatar = model.Avatar;
            ext.Status = Status.Enable;

            dbMember.MemberExts ??= new List<MemberExt>();
            dbMember.MemberExts.Add(ext);
        });
    }

    /// <summary>
    /// 设置用户角色
    /// </summary>
    /// <param name="dbMember"></param>
    /// <param name="roleIds"></param>
    /// <param name="xppId"></param>
    private static void SetRoles(DbMember dbMember, long[]? roleIds, long tenantId = 0, long xppId = 0)
    {
        //修改则先清
        if (roleIds is not null)
        {
            //新真用户情况下的角色设定
            if (dbMember.Id > 0)
            {
                dbMember.RoleMembers?.Remove(x => !roleIds.Contains(x.RoleId));
            }
            dbMember.RoleMembers = new List<RoleMember>();

            foreach (var roleId in roleIds)
            {
                dbMember.RoleMembers.Add(
                    new RoleMember
                    {
                        RoleId = roleId,
                        Status = Status.Enable,
                        TenantId = tenantId,
                        XppId = xppId
                    });
            }
        }
    }

    /// <summary>
    /// 获取真实会员信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private async Task<IUser?> GetUserAsync(long id, int type)
    {
        if (_user is not null)
        {
            return _user;
        }
        else
        {
            if (id > 0 && _userProvider is not null)
            {
                return await _userProvider.GetAsync(id, type);
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 获取真实会员信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private async Task<IUser?> GetUserAsync(string mobile, int type)
    {
        if (_user is not null)
        {
            return _user;
        }
        else
        {
            if (!mobile.IsNullOrWhiteSpace() && _userProvider is not null)
            {
                return await _userProvider.GetAsync(mobile, type);
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 注册用户
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private async Task<IUser?> RegisterUserAsync(RegisterModel registerModel)
    {
        if (_userProvider is not null)
        {
            _user = await _userProvider.RegisterAsync(registerModel);
        }

        return _user;
    }

    #endregion

    /// <summary>
    /// 检查社交账号是否已绑定过系统账号
    /// </summary>
    /// <param name="sns"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    private async Task<AccessConfig> CheckSnsAccountAsync(XppSns sns, string code)
    {
        var ac = await GetAccessConfigAsync(sns, code);

        //社交账号是否绑定过
        //bool exsits = await _unitOfWork.GetRepo<MemberExt>().ExistAsync(x => x.XappSnsId == sns.Id && x.OpenId == ac.OpenId);
        //if (exsits)
        //{
        //    throw new MaxException(ResultEnum.SnsIsBind);
        //}

        return ac;
    }

    /// <summary>
    /// 获取访问配置
    /// </summary>
    /// <param name="sns"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    private async Task<AccessConfig> GetAccessConfigAsync(XppSns sns, string code)
    {
        SnsAuth snsAuth = new SnsAuth
        {
            AppId = sns.AppId,
            AppSecret = sns.AppSecret,
            Code = code
        };

        AccessConfig accessConfig = await _snsFactory.GetService(sns.Source).GetAccessConfigAsync(snsAuth);
        accessConfig.AccountId = sns.AccountId;
        accessConfig.SnsSource = (SnsSource)sns.Source;
        accessConfig.Status = sns.Status;

        //var token = MakeAccessToken();
        //accessConfig.Token = token.Token;
        //accessConfig.Expires = token.Expires;

        return accessConfig;
    }

    /// <summary>
    /// 生成访问令牌
    /// </summary>
    /// <returns></returns>
    private IAccessToken MakeAccessToken()
    {
        return new AccessToken
        {
            Token = Guid.NewGuid().ToString().Replace("-", ""),
            Expires = DateTime.Now.AddMinutes(_option.Identity.Expires),
        };
    }

    /// <summary>
    /// 生成密码
    /// </summary>
    /// <param name="source"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    private static string MakePassword(string source, string salt)
    {
        return MD5.Hash($"{salt}-{source}@imaxsys@{salt}-{source}");
    }

    /// <summary>
    /// 检验密码
    /// </summary>
    /// <param name="password"></param>
    /// <param name="source"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    private bool CheckPassword(string password, string source, string salt)
    {
        return MakePassword(source, salt) == password;
    }
}