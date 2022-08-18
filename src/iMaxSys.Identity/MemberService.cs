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
using iMaxSys.Max.Identity;
using iMaxSys.Max.Identity.Domain;
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

    private IUser _user;

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
        if (member != null)
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



    #region RegisterAsync

    public async Task RegisterAsync(RegisterModel model)
    {
        string mobile = model.Mobile;
        bool needCheckCode = true;

        //账号平台来源
        SnsSource source = SnsSource.Max;

        //获取sns
        var xppSns = await _unitOfWork.GetRepository<XppSns>().FirstOrDefaultAsync(x => x.Id == model.XppSnsId, null, x => x.Include(y => y.Xpp));

        if (xppSns is null)
        {
            throw new MaxException(ResultCode.XppSnsIdNotExists);
        }

        AccessConfig accessConfig = await CheckSnsAccountAsync(xppSns, model.Code);
        source = xppSns.Source;

        var repository = _unitOfWork.GetRepository<DbMember>();
        var dbMember = await repository.FirstOrDefaultAsync(x => x.Id == model.MemberId || x.Mobile == mobile || x.UserName == model.UserName, null, x => x.Include(y => y.MemberExts));

        //用户名密码注册,需要判断是否用该手机号注册过,其他方式注册存在多社交账户绑定同一member,就不判断
        if (dbMember != null && (xppSns.Source == SnsSource.Max || dbMember.IsOfficial))
        {
            throw new MaxException(ResultCode.UserExists);
        }

        //BizConfig bizConfig = new BizConfig
        //{
        //    XappId = sns.XappId,
        //    XappName = sns.Xapp.Name,
        //    BizId = BizSource.BindCheckCode.GetHashCode(),
        //    BizName = BizSource.BindCheckCode.GetDescription(),
        //};

        switch (source)
        {
            case SnsSource.Max:
                if (model.UserName.IsNullOrWhiteSpace())
                {
                    throw new MaxException(ResultCode.UserNameCantNull);
                }
                if (!model.Password.IsStrong())
                {
                    throw new MaxException(ResultCode.PasswordIsWeak);
                }
                if (xppSns.Xpp.NeedMobile && !model.Mobile.IsMobile())
                {
                    throw new MaxException(ResultCode.MobileIsInvalid);
                }
                break;

            case SnsSource.WeChat:
                //有code,就不需要openId,但二者不可同时为空
                if (model.Code.IsNullOrWhiteSpace() && model.OpenId.IsNullOrWhiteSpace())
                {
                    throw new MaxException(ResultCode.CodeOpenIdCantNull);
                }

                //是否需要手机号码
                if (xppSns.Xpp.NeedMobile && model.Mobile.IsNullOrWhiteSpace() && model.EncryptedData.IsNullOrWhiteSpace())
                {
                    throw new MaxException(ResultCode.MobileIsInvalid);
                }

                //社交平台获取电话号码,则不需要验证码
                if (!model.EncryptedData.IsNullOrWhiteSpace())
                {
                    MemberSession? memberSession = await _unitOfWork.GetRepository<MemberSession>().FirstOrDefaultAsync(x => x.XppSnsId == model.XppSnsId && x.Token == model.Token);
                    if (memberSession is null)
                    {
                        throw new MaxException(ResultCode.AccessSessionIsNull);
                    }

                    SnsPhoneNumber? phoneNumber  = _snsFactory.GetService(SnsSource.WeChat).GetPhoneNumber(model.EncryptedData, memberSession.SessionKey, model.IV);
                    if (phoneNumber is null)
                    {
                        throw new MaxException(ResultCode.GetMobileFail);
                    }
                    mobile = phoneNumber.PurePhoneNumber;
                    needCheckCode = false;
                }
                break;
            default:
                break;
        }

        if (needCheckCode)
        {
            //检查验证码
            await _checkCodeService.CheckAsync(model.XppSnsId, BizSource.BindCheckCode.GetHashCode(), model.MemberId, mobile, model.CheckCode);
        }

        IUser user = await ActiveUserAsync(mobile, model.Avatar, model.Type);

        //新会员
        if (dbMember == null)
        {
            dbMember = new DbMember
            {
                TenantId = user?.TenantId > 0 ? user.TenantId : 0,
                Name = (((user?.Name ?? user?.NickName) ?? model.Name) ?? model.NickName) ?? mobile,
                NickName = ((user?.NickName) ?? model.NickName) ?? mobile,
                Mobile = mobile,
                LoginName = model.UserName ?? mobile,
                Salt = Guid.NewGuid().ToString().Replace("-", ""),
                AccountSource = source,
                JoinTime = DateTime.Now,
                JoinIp = model.IP,
                Start = DateTime.Now,
                Gender = user?.Gender ?? model.Gender,
                Avatar = user?.Avatar ?? model.Avatar,
                LastIp = model.IP,
                LastLogin = DateTime.Now,
                IsOfficial = user?.IsOfficial ?? false,
                Status = Status.Enable,
                Type = model.Type,
                MemberExts = new List<MemberExt>()
            };

            dbMember.Password = MakePassword(PASSWORD, dbMember.Salt);

            MemberExt ext1 = new MemberExt
            {
                TenantId = user?.TenantId > 0 ? user.TenantId : 0,
                OpenId = model.OpenId,
                Expires = DateTime.Now.AddMinutes(_option.Identity.Expires),
                NickName = model.NickName,
                Mobile = model.Mobile,
                Avatar = model.Avatar,
                Country = model.Country,
                Province = model.Province,
                City = model.City,
                Gender = model.Gender,
                XappSnsId = sns.Id,
                Name = model.NickName,
                Status = Status.Enable
            };
            dbMember.MemberExts.Add(ext1);
            await repo.AddAsync(dbMember);
        }
        else
        {
            dbMember.UserId = user?.Id ?? 0;
            dbMember.TenantId = user?.TenantId > 0 ? user.TenantId : 0;
            dbMember.Name = (((user?.Name ?? user?.NickName) ?? model.Name) ?? model.NickName) ?? mobile;
            dbMember.IsOfficial = user?.IsOfficial ?? false;
            dbMember.Mobile = user?.Mobile;

            //是否存在用户同一应用同一账号来源的绑定信息
            //ISpecification<MemberExt> spec3 = new Specification<MemberExt>(x => x.XappSnsId == model.XappSnsId && x.MemberId == dbMember.Id);
            //spec3.ApplyTracking();
            //MemberExt memberExt = await _unitOfWork.GetRepo<MemberExt>().FirstOrDefaultAsync(spec3);

            MemberExt memberExt = dbMember.MemberExts.FirstOrDefault(x => x.XappSnsId == model.XappSnsId && x.MemberId == dbMember.Id);
            if (memberExt == null)
            {
                MemberExt ext2 = new MemberExt
                {
                    TenantId = dbMember.TenantId,
                    MemberId = dbMember.Id,
                    OpenId = model.OpenId,
                    NickName = model.NickName,
                    Avatar = model.Avatar,
                    Mobile = model.Mobile,
                    Name = model.NickName,
                    Country = model.Country,
                    Province = model.Province,
                    City = model.City,
                    Gender = model.Gender,
                    Expires = DateTime.Now.AddMinutes(_option.Identity.Expires),
                    Status = Status.Enable
                };
                dbMember.MemberExts.Add(ext2);
            }
            else
            {
                memberExt.TenantId = dbMember.TenantId;
                memberExt.XappSnsId = model.XappSnsId;
                memberExt.OpenId = model.OpenId;
                memberExt.NickName = model.NickName;
                memberExt.Avatar = model.Avatar;
                memberExt.Mobile = model.Mobile;
                memberExt.Name = model.NickName;
                memberExt.Country = model.Country;
                memberExt.Province = model.Province;
                memberExt.City = model.City;
                memberExt.Gender = model.Gender;
                memberExt.Expires = DateTime.Now.AddMinutes(_option.Identity.Expires);
                memberExt.Status = Status.Enable;

                //await _unitOfWork.GetRepo<MemberExt>().UpdateAsync(memberExt);
            }

            if (string.IsNullOrWhiteSpace(dbMember.Name))
            {
                dbMember.Name = model.Name ?? user.Name;
            }

            if (string.IsNullOrWhiteSpace(dbMember.LoginName))
            {
                dbMember.LoginName = model.UserName ?? (model.Mobile ?? user.Mobile);
            }

            if (string.IsNullOrWhiteSpace(dbMember.Password))
            {
                dbMember.Password = MakePassword(PASSWORD, dbMember.Salt);
            }

            if (string.IsNullOrWhiteSpace(dbMember.NickName))
            {
                dbMember.NickName = model.NickName ?? user.NickName;
            }

            if (string.IsNullOrWhiteSpace(dbMember.Avatar))
            {
                dbMember.Avatar = model.Avatar ?? user.Avatar;
            }

            await repo.UpdateAsync(dbMember);
        }

        await _unitOfWork.SaveChangesAsync();

        await UpdateSession(model.Token, sns.Id, model.OpenId);
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
        dbMember.UserId = model.UserId ??= dbMember.Id;      //如无外接UserId,使用Member.Id
        model.Name?.IfNotNull(x => dbMember.Name = x);
        model.IdNumber?.IfNotNull(x => dbMember.IdNumber = x);
        model.QuickCode?.IfNotNull(x => dbMember.QuickCode = x);
        model.Birthday?.IfNotNull(x => dbMember.Birthday = x);
        model.MaritalStatus?.IfNotNull(x => dbMember.MaritalStatus = x);
        model.Gender?.IfNotNull(x => dbMember.Gender = x);
        model.Nation?.IfNotNull(x => dbMember.Nation = x);
        model.Education?.IfNotNull(x => dbMember.Education = x);
        model.Party?.IfNotNull(x => dbMember.Party = x);
        model.UserName?.IfNotNull(x => dbMember.UserName = x, () => dbMember.UserName = dbMember.Mobile);
        model.NickName?.IfNotNull(x => dbMember.NickName = x);
        model.CountryCode?.IfNotNull(x => dbMember.CountryCode = x);
        model.Mobile?.IfNotNull(x => dbMember.Mobile = x);
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
        model.Type?.IfNotNull(x => dbMember.Type = x);
        model.DepartmentId?.IfNotNull(x => dbMember.DepartmentId = x);
        model.Start?.IfNotNull(x => dbMember.Start = x);
        model.End?.IfNotNull(x => dbMember.End = x);
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
        model.IP?.IfNotNull(x => dbMember.JoinIP = x);
        dbMember.LastLogin = now;
        dbMember.LastIP = dbMember.JoinIP;

        //salt
        dbMember.Salt = Guid.NewGuid().ToString().Replace("-", "");

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
            if (id > 0 && _userProvider != null)
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
            if (!mobile.IsNullOrWhiteSpace() && _userProvider != null)
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
    /// 激活
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private async Task<IUser?> ActiveUserAsync(string mobile, string avatar, int type)
    {
        if (_user is null && !mobile.IsNullOrWhiteSpace() && _userProvider is not null)
        {
            _user = await _userProvider.ActivateAsync(mobile, avatar, type);
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

        var token = MakeAccessToken();
        accessConfig.Token = token.Token;
        accessConfig.Expires = token.Expires;

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
    /// 检查密码强度, 待扩展
    /// </summary>
    /// <param name="source"></param>
    private void CheckPassword(string source)
    {
        if (source.Length < 6 || source.Length > 24)
        {
            throw new MaxException(ResultCode.PasswordIsWeak);
        }
    }
}