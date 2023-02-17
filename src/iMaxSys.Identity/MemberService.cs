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
using iMaxSys.Data.Repositories;
using iMaxSys.Core.Models;
using iMaxSys.Core.Services;
using iMaxSys.Identity.Common;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Entities;
using iMaxSys.Identity.Data.Repositories;
using iMaxSys.Sns;
using iMaxSys.Sns.Api;
using iMaxSys.Sns.Common.Auth;
using iMaxSys.Sns.Common.Open;

using MD5 = iMaxSys.Max.Security.Cryptography.MD5;
using DbRole = iMaxSys.Identity.Data.Entities.Role;
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
    private readonly IUserService _userService;
    private readonly ISnsFactory _snsFactory;
    private readonly ICoreService _coreService;
    private readonly IMenuService _menuService;
    private readonly IRoleService _roleService;
    private readonly ICheckCodeService _checkCodeService;

    private IUser? _user;

    #region 构造

    public MemberService(IMapper mapper, IOptions<MaxOption> option, IUnitOfWork<IdentityContext> unitOfWork, IUserService userProvider, ISnsFactory snsFactory, ICoreService coreService, ICheckCodeService checkCodeService, IMenuService menuService, IRoleService roleService)
    {
        _mapper = mapper;
        _option = option.Value;
        _unitOfWork = unitOfWork;
        _userService = userProvider;
        _snsFactory = snsFactory;
        _coreService = coreService;
        _checkCodeService = checkCodeService;
        _menuService = menuService;
        _roleService = roleService;
    }

    #endregion

    #region LoginAsync

    /// <summary>
    /// login by code
    /// </summary>
    /// <param name="sid"></param>
    /// <param name="type"></param>
    /// <param name="code"></param>
    /// <param name="ip"></param>
    /// <returns></returns>
    public async Task<IAccessChain> LoginAsync(CodeLoginModel codeLoginModel)
    {
        DbMember dbMember;

        //获取xppSns
        var xppSns = await _coreService.GetXppSnsAsync(codeLoginModel.Id);

        if (xppSns is null)
        {
            throw new MaxException(ResultCode.XppSnsIdNotExists);
        }

        DateTime now = DateTime.Now;
        DateTime end = now.AddYears(100);

        bool isOfficial = true;
        //IAccessToken token = MakeAccessToken();

        //取访问配置
        AccessConfig accessConfig = await GetAccessConfigAsync(xppSns, codeLoginModel.Code);
        var memberExt = await _unitOfWork.GetRepository<MemberExt>().FirstOrDefaultAsync(x => x.XppSnsId == codeLoginModel.Id && x.OpenId == accessConfig.OpenId, null, x => x.Include(y => y.Member));

        //有表示已是会员,无进行快速注册,注册完成为非正式
        if (memberExt == null)
        {
            string salt = Guid.NewGuid().ToString().Replace("-", "");
            dbMember = new DbMember
            {
                Type = codeLoginModel.Type,
                Start = now,
                End = end,
                XppSource = xppSns.Xpp.Source,
                AccountSource = xppSns.Source,
                Salt = salt,
                Password = MakePassword(MaxRandom.Next().ToString().Right(6), salt),
                JoinTime = now,
                JoinIP = codeLoginModel.IP,
                LastLogin = now,
                LastIP = codeLoginModel.IP,
                IsOfficial = isOfficial,
                Status = Status.Enable,
                MemberExts = new List<MemberExt>()
            };

            //快速登录，自动注册扩展信息
            dbMember.MemberExts.Add(new MemberExt
            {
                XppSnsId = codeLoginModel.Id,
                OpenId = accessConfig.OpenId,
                UnionId = accessConfig.UnionId.IfNullOrEmpty(accessConfig.OpenId),
                Expires = end,
                Status = Status.Enable
            });

            await _unitOfWork.GetRepository<DbMember>().AddAsync(dbMember);
            await _unitOfWork.SaveChangesAsync();
        }
        else
        {
            dbMember = memberExt.Member;
            dbMember.LastIP = codeLoginModel.IP;
            dbMember.LastLogin = now;
            await _unitOfWork.SaveChangesAsync();
            await CheckStatus(dbMember);
        }

        _user = await LoginUserAsync(dbMember.Id, dbMember.UserId, dbMember.Type);

        var member = _mapper.Map<MemberModel>(dbMember);
        return await RefreshAccessChainAsync(xppSns, member, accessConfig);
    }

    /// <summary>
    /// login
    /// </summary>
    /// <param name="xppSnsId"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="ip"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public async Task<IAccessChain> LoginAsync(PasswordLoginModel model)
    {
        //用户名空检查
        if (string.IsNullOrWhiteSpace(model.UserName))
        {
            throw new MaxException(ResultCode.UserNameCantNull);
        }

        //密码空检查
        if (string.IsNullOrWhiteSpace(model.Password))
        {
            throw new MaxException(ResultCode.PasswordCantNull);
        }

        DbMember? dbMember = await _unitOfWork.GetCustomRepository<IMemberRepository>().FirstOrDefaultAsync(x => x.UserName == model.UserName, null, x => x.Include(y => y.RoleMembers).Include(y => y.Department), false);
        if (dbMember is null)
        {
            throw new MaxException(ResultCode.MemberNotExists);
        }

        if (!(CheckPassword(dbMember.Password, model.Password, dbMember.Salt)))
        {
            throw new MaxException(ResultCode.PasswordError);
        }

        //获取xppSns
        var xppSns = await _coreService.GetXppSnsAsync(model.XppSnsId);

        if (xppSns is null)
        {
            throw new MaxException(ResultCode.XppSnsIdNotExists);
        }

        _user = await LoginUserAsync(dbMember.Id, dbMember.UserId, dbMember.Type);

        var member = _mapper.Map<MemberModel>(dbMember);

        //获取角色
        if (dbMember.RoleMembers?.Count > 0)
        {
            member.Roles = new List<iMaxSys.Max.Identity.Domain.Role>();
            var role = await _roleService.GetAsync(dbMember.TenantId, model.XppSnsId, dbMember.RoleMembers.First().RoleId);
            if (role is not null)
            {
                member.Roles.Add(role);
            }
        }
        return await RefreshAccessChainAsync(xppSns, member);
    }

    #endregion

    #region LoginUserAsync

    /// <summary>
    /// login user
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="userId"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task<IUser?> LoginUserAsync(long memberId, long userId, int type)
    {
        if (_userService is not null && userId > 0 && memberId != userId)
        {
            _user = await _userService.LoginAsync(userId, type);
        }

        return _user;
    }

    #endregion

    #region RegisterAsync

    /// <summary>
    /// register
    /// </summary>
    /// <param name="registerModel"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public async Task<IAccessChain> RegisterAsync(RegisterModel registerModel)
    {
        bool needCheckCode = true;

        //存在检查
        var repository = _unitOfWork.GetCustomRepository<IMemberRepository>();
        var dbMember = await repository.FirstOrDefaultAsync(x => x.Mobile == registerModel.Mobile || x.Email == registerModel.Email || x.UserName == registerModel.UserName, null, x => x.Include(y => y.MemberExts));
        if (dbMember is not null)
        {
            throw new MaxException(ResultCode.UserExists);
        }

        //获取xppSns
        var xppSns = await _coreService.GetXppSnsAsync(registerModel.XppSnsId);

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
        SetRegisterMember(registerModel, _user, dbMember, xppSns.Xpp.Source, xppSns.Source, registerModel.TenantId, xppSns.XppId);

        //持久化
        await repository.AddAsync(dbMember);
        await _unitOfWork.SaveChangesAsync();

        //刷新缓存
        AccessConfig? accessConfig = null;

        if (registerModel.Code is not null && !registerModel.Code.IsNullOrEmpty())
        {
            accessConfig = await GetAccessConfigAsync(xppSns, registerModel.Code);
        }

        var member = _mapper.Map<MemberModel>(dbMember);
        return await RefreshAccessChainAsync(xppSns, member, accessConfig);
    }

    #endregion

    #region RegisterUserAsync

    /// <summary>
    /// 注册用户
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task<IUser?> RegisterUserAsync(RegisterModel registerModel)
    {
        if (_userService is not null)
        {
            _user = await _userService.RegisterAsync(registerModel);
        }

        return _user;
    }

    #endregion

    #region LogoutAsync

    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task LogoutAsync(string token)
    {
        //to-do: 记录登出
        await _unitOfWork.GetCustomRepository<IMemberRepository>().RemoveAccessSessionAsync(token);
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
    public async Task<IMember> AddAsync(MemberModel model, long xppId, long[]? roleIds)
    {
        DbMember dbMember = new();
        SetNewMember(model, dbMember, xppId, roleIds);
        SetRoles(dbMember, roleIds, model.TenantId, xppId);
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

    /// <summary>
    /// Get member
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    public async Task<IMember?> GetAsync(string mobile)
    {
        return await _unitOfWork.GetCustomRepository<IMemberRepository>().GetAsync(mobile);
    }

    #endregion

    #region UpdateAsync

    /// <summary>
    /// update
    /// </summary>
    /// <param name="model"></param>
    /// <param name="xppId"></param>
    /// <param name="roleIds"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public async Task<IMember> UpdateAsync(MemberModel model, long xppId, long[]? roleIds)
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

        SetMember(model, member, xppId, roleIds);
        respoitory.Update(member);
        await _unitOfWork.SaveChangesAsync();

        var result = _mapper.Map<MemberModel>(member);
        await RefreshAsync(result);
        return result;
    }

    #endregion

    #region GetAccessChainAsync

    /// <summary>
    /// GetAccessChainAsync
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<IAccessChain> GetAccessChainAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new MaxException(ResultCode.TokenCantNull);
        }

        IMemberRepository repository = _unitOfWork.GetCustomRepository<IMemberRepository>();

        //先按Token获取uid
        IAccessSession? access = await repository.GetAccessSessionAsync(token);

        if (access is null)
        {
            throw new MaxException(ResultCode.AccessSessionIsNull);
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
                user = await _userService.GetAsync(access.MemberId.Value, access.Type);
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

    #region RefreshAsync

    /// <summary>
    /// 刷新member缓存
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public async Task RefreshAsync(long memberId)
    {
        IUser? user = await GetUserAsync(memberId);
        await _unitOfWork.GetCustomRepository<IMemberRepository>().RefreshMemberAsync(memberId, user);
    }

    /// <summary>
    /// 刷新member缓存
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public async Task RefreshAsync(IMember member)
    {
        IUser? user = await _userService.GetAsync(member.Id, member.Type);
        await _unitOfWork.GetCustomRepository<IMemberRepository>().RefreshMemberAsync(member, user);
    }

    #endregion

    #region RefreshAccessChainAsync

    /// <summary>
    /// 刷新访问链
    /// </summary>
    /// <param name="xppSns"></param>
    /// <param name="member"></param>
    /// <param name="accessConfig"></param>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    public async Task<IAccessChain> RefreshAccessChainAsync(XppSns xppSns, long memberId, AccessConfig? accessConfig = null, IAccessToken? accessToken = null)
    {
        DbMember? member = await _unitOfWork.GetCustomRepository<IMemberRepository>().FirstOrDefaultAsync(x => x.Id == memberId);

        if (member == null)
        {
            throw new MaxException(ResultCode.MemberNotExists);
        }

        return await RefreshAccessChainAsync(xppSns, memberId, accessConfig, accessToken);
    }

    /// <summary>
    /// 刷新访问链
    /// </summary>
    /// <param name="xppSns"></param>
    /// <param name="member"></param>
    /// <param name="accessConfig"></param>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    public async Task<IAccessChain> RefreshAccessChainAsync(XppSns xppSns, MemberModel member, AccessConfig? accessConfig = null, IAccessToken? accessToken = null)
    {
        var token = accessToken ?? MakeAccessToken();

        _user = await GetUserAsync(member.Id);

        MemberSession memberSession = new MemberSession
        {
            XppSnsId = xppSns.Id,
            Token = token.Token,
            Expires = token.Expires,
            Status = _user?.Status ?? member.Status,
            MemberId = member.Id,
            IsOfficial = _user?.IsOfficial ?? member.IsOfficial
        };

        accessConfig?.OpenId.IfNotNull(x => memberSession.OpenId = x);
        accessConfig?.UnionId.IfNotNull(x => memberSession.UnionId = x);
        accessConfig?.NickName.IfNotNull(x => memberSession.NickName = x);
        accessConfig?.Avatar.IfNotNull(x => memberSession.Avatar = x);

        await _unitOfWork.GetRepository<MemberSession>().AddAsync(memberSession);
        await _unitOfWork.SaveChangesAsync();

        AccessSession accessSession = new AccessSession
        {
            XppSnsId = xppSns.Id,
            AccountId = accessConfig?.AccountId,
            AppId = accessConfig?.AppId,
            Avatar = accessConfig?.Avatar,
            Name = accessConfig?.NickName,
            SessionKey = accessConfig?.SessionKey,
            OpenId = accessConfig?.OpenId,
            UnionId = accessConfig?.UnionId,
            TenantId = accessConfig?.TenantId ?? 0,
            Token = token.Token,
            Expires = token.Expires,
            XppSource = xppSns.Xpp.Source,
            AccountSource = xppSns.Source,
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
            User = _user
        };

        await _unitOfWork.GetCustomRepository<IMemberRepository>().RefreshAccessSessionAsync(accessChain);
        return accessChain;
    }

    /*
    /// <summary>
    /// RefreshAccessChainAsync
    /// </summary>
    /// <param name="accessConfig"></param>
    /// <param name="accessToken"></param>
    /// <param name="xppSns"></param>
    /// <param name="member"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<IAccessChain> RefreshAccessChainAsync(AccessConfig? accessConfig, IAccessToken? accessToken, XppSns xppSns, IMember member, IUser? user)
    {
        var token = accessToken ?? MakeAccessToken();

        MemberSession memberSession = new MemberSession
        {
            XppSnsId = xppSns.Id,
            Token = token.Token,
            Expires = token.Expires,
            Status = user?.Status ?? member.Status,
            MemberId = member.Id,
            IsOfficial = user?.IsOfficial ?? member.IsOfficial
        };

        accessConfig?.OpenId.IfNotNull(x => memberSession.OpenId = x);
        accessConfig?.UnionId.IfNotNull(x => memberSession.UnionId = x);
        accessConfig?.NickName.IfNotNull(x => memberSession.NickName = x);
        accessConfig?.Avatar.IfNotNull(x => memberSession.Avatar = x);

        await _unitOfWork.GetRepository<MemberSession>().AddAsync(memberSession);
        await _unitOfWork.SaveChangesAsync();

        AccessSession accessSession = new AccessSession
        {
            XppSnsId = xppSns.Id,
            AccountId = accessConfig?.AccountId,
            AppId = accessConfig?.AppId,
            Avatar = accessConfig?.Avatar,
            Name = accessConfig?.NickName,
            SessionKey = accessConfig?.SessionKey,
            OpenId = accessConfig?.OpenId,
            UnionId = accessConfig?.UnionId,
            TenantId = accessConfig?.TenantId ?? 0,
            Token = token.Token,
            Expires = token.Expires,
            XppSource = xppSns.Xpp.Source,
            AccountSource = xppSns.Source,
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
            User = _user
        };

        await _unitOfWork.GetCustomRepository<IMemberRepository>().RefreshAccessSessionAsync(accessChain);
        return accessChain;
    }
    */

    #endregion

    #region RefreshAcceeChainAsync

    /*
    /// <summary>
    /// 刷新AcceeChain缓存
    /// </summary>
    /// <param name="oldToken"></param>
    /// <param name="accessChain"></param>
    /// <returns></returns>
    public async Task RefreshAccessChainAsync(IAccessChain accessChain, string oldToken)
    {
        if (accessChain.AccessSession is not null)
        {
            await _unitOfWork.GetCustomRepository<IMemberRepository>().RefreshAccessSessionAsync(accessChain, oldToken);
        }
        else
        {
            throw new MaxException(ResultCode.MemberNotExists);
        }
    }
    */

    #endregion

    #region ChangePasswordAsync

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="oldPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    public async Task ChangePasswordAsync(long memberId, string oldPassword, string newPassword)
    {
        IMemberRepository respoitory = _unitOfWork.GetCustomRepository<IMemberRepository>();
        var member = await respoitory.FindAsync(memberId);
        if (member == null)
        {
            throw new MaxException(ResultCode.MemberNotExists);
        }
        //验证旧密码
        if (!CheckPassword(member.Password, oldPassword, member.Salt))
        {
            throw new MaxException(ResultCode.PasswordError);
        }

        member.Password = MakePassword(newPassword, member.Salt);

        respoitory.Update(member);
        await _unitOfWork.SaveChangesAsync();
    }

    #endregion

    #region GetSnsPhoneNumber

    /// <summary>
    /// 获取社交平台绑定的电话号码
    /// </summary>
    /// <param name="sid"></param>
    /// <param name="data"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public async Task<SnsPhoneNumber> GetSnsPhoneNumber(long xppSnsId, string data, string key, string iv)
    {
        var sns = await _coreService.GetXppSnsAsync(xppSnsId);

        if (sns is null)
        {
            throw new MaxException(ResultCode.XppSnsIdNotExists);
        }

        return _snsFactory.GetService(sns.Source).GetPhoneNumber(data, key, iv);
    }

    #endregion

    #region SetMember

    /// <summary>
    /// 使用MemberModel设置DbMember
    /// </summary>
    /// <param name="model"></param>
    /// <param name="dbMember"></param>
    /// <param name="xppId"></param>
    /// <param name="roleIds"></param>
    private static string SetNewMember(MemberModel model, DbMember dbMember, long xppId, long[]? roleIds)
    {
        SetMember(model, dbMember, xppId, roleIds);

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
        string password = MaxRandom.Next().ToString().Right(6);
        dbMember.Password = MakePassword(password, dbMember.Salt);
        return password;
    }

    /// <summary>
    /// 使用MemberModel设置DbMember
    /// </summary>
    /// <param name="model"></param>
    /// <param name="dbMember"></param>
    /// <param name="xppId"></param>
    /// <param name="roleIds"></param>
    private static void SetMember(MemberModel model, DbMember dbMember, long xppId, long[]? roleIds)
    {
        dbMember.UserId = model.UserId > 0 ? model.UserId : dbMember.Id;      //如无外接UserId,使用Member.Id
        dbMember.Mobile = model.Mobile;
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

        SetRoles(dbMember, roleIds, model.TenantId, xppId);
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
    private static string SetRegisterMember(RegisterModel model, IUser? user, DbMember dbMember, XppSource xppSource, SnsSource snsSource, long tenantId = 0, long xppId = 0)
    {
        string password = model.Password ?? MaxRandom.Next().ToString().Right(6);
        DateTime end = DateTime.Now.AddYears(100);
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
        dbMember.Password = MakePassword(password, dbMember.Salt);
        dbMember.End = end;

        //openId不为空, 则新增扩展成员信息
        model.OpenId?.IfNotNull(x =>
        {
            MemberExt ext = new MemberExt();
            ext.XppSnsId = model.XppSnsId;
            ext.TenantId = model.TenantId;
            ext.OpenId = model.OpenId;
            ext.UnionId = model.UnionId ?? Guid.NewGuid().ToString().Replace("-", "");
            ext.Name = model.NickName;
            ext.Avatar = model.Avatar;
            ext.Status = Status.Enable;
            ext.Expires = end;

            dbMember.MemberExts ??= new List<MemberExt>();
            dbMember.MemberExts.Add(ext);
        });

        return password;
    }

    /// <summary>
    /// 设置用户角色
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="roleIds"></param>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    public async void SetRoles(long memberId, long[]? roleIds, long tenantId = 0, long xppId = 0)
    {
        var member = await _unitOfWork.GetCustomRepository<IMemberRepository>().FindAsync(memberId);

        if (member is null)
        {
            throw new MaxException(ResultCode.MemberNotExists);
        }

        SetRoles(member, roleIds, tenantId, xppId);
    }

    /// <summary>
    /// 设置用户角色
    /// </summary>
    /// <param name="dbMember"></param>
    /// <param name="roleIds"></param>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    public static void SetRoles(DbMember dbMember, long[]? roleIds, long tenantId = 0, long xppId = 0)
    {
        if (dbMember.Id > 0)
        {
            dbMember.RoleMembers?.Clear();
        }
        else
        {
            dbMember.RoleMembers = new List<RoleMember>();
        }

        if (roleIds is not null && roleIds.Length > 0)
        {
            foreach (var roleId in roleIds)
            {
                dbMember.RoleMembers?.Add(
                    new RoleMember
                    {
                        RoleId = roleId,
                        TenantId = tenantId,
                        XppId = xppId
                    });
            }
        }
    }

    /// <summary>
    /// 获取真实会员信息
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    public async Task<IUser?> GetUserAsync(long memberId)
    {
        return await _unitOfWork.GetCustomRepository<IMemberRepository>().GetUserAsync(memberId);
    }

    /// <summary>
    /// 获取真实会员信息
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    public async Task<IUser?> GetUserAsync(string mobile)
    {
        return await _unitOfWork.GetCustomRepository<IMemberRepository>().GetUserAsync(mobile);
    }

    /// <summary>
    /// 获取真实会员信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task<IUser?> GetUserAsync(long id, int type)
    {
        return await _unitOfWork.GetCustomRepository<IMemberRepository>().GetUserAsync(id, type);
    }

    /// <summary>
    /// 获取真实会员信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task<IUser?> GetUserAsync(string mobile, int type)
    {
        return await _unitOfWork.GetCustomRepository<IMemberRepository>().GetUserAsync(mobile, type);
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
            AppSecret = sns.AppKey,
            Code = code
        };
        //to-do: max的赋值
        AccessConfig accessConfig = await _snsFactory.GetService(sns.Source).LoginAsync(snsAuth);
        accessConfig.AccountId = sns.AccountId;
        accessConfig.SnsSource = (SnsSource)sns.Source;
        accessConfig.Status = sns.Status;

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
    private static bool CheckPassword(string password, string source, string salt)
    {
        return MakePassword(source, salt) == password;
    }

    /// <summary>
    /// 检查用户状态
    /// </summary>
    /// <param name="dbMember"></param>
    /// <returns></returns>
    private async Task CheckStatus(DbMember dbMember)
    {
        IUser? user = await GetUserAsync(dbMember.UserId, dbMember.Type);

        //禁用判断
        if (dbMember?.Status == Status.Disable || user?.Status == Status.Disable)
        {
            throw new MaxException(ResultCode.UserIsDisable);
        }

        //过期判断
        if (dbMember?.End < DateTime.Now || user?.End < DateTime.Now)
        {
            throw new MaxException(ResultCode.UserIsExpired);
        }
    }

    /// <summary>
    /// API权限检查
    /// </summary>
    /// <param name="token"></param>
    /// <param name="router"></param>
    /// <returns></returns>
    public async Task<IAccessChain> CheckAsync(string token, string router)
    {
        var ac = await GetAccessChainAsync(token);
        await CheckAsync(ac, router);
        return ac;
    }

    /// <summary>
    /// API权限检查
    /// </summary>
    /// <param name="accessChain"></param>
    /// <param name="router"></param>
    /// <returns></returns>
    private async Task CheckAsync(IAccessChain accessChain, string router)
    {

        //停用检查
        if (accessChain.Member?.Status == Status.Disable)
        {
            throw new MaxException(ResultCode.UserIsDisable);
        }

        //全开放
        if (_option.Identity.OpenRouters.Contains("*"))
        {
            return;
        }
        else
        {
            //简单模式
            if (0 == _option.Identity.CheckMode)
            {
                //已登录或者在放行api,则通行
                if (_option.Identity.OpenRouters.Contains(router))
                {
                    return;
                }

                if (accessChain is null || !accessChain.AccessSession.IsLogin)
                {
                    throw new MaxException(ResultCode.UnLogin, router);
                }
            }
            else //严格模式
            {
                //在开放api中,则通行
                if (_option.Identity.OpenRouters.Contains(router))
                {
                    return;
                }

                if (!accessChain.AccessSession.IsLogin)
                {
                    throw new MaxException(ResultCode.UnLogin, router);
                }

                long roleId = accessChain.Member?.Roles?.FirstOrDefault(x => x.XppId == _option.XppId)?.Id ?? 0;
                bool allow = await _menuService.AllowAccessAsync(accessChain.Member?.TenantId ?? 0, _option.XppId, roleId, router);

                if (allow)
                {
                    return;
                }
                else
                {
                    throw new MaxException(ResultCode.Forbidden, router);
                }

            }
        }
    }

    /// <summary>
    /// 验证路由是否在权限菜单内
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="router"></param>
    /// <returns></returns>
    //protected static bool ExistsRouter(IMenu? menu, string router)
    //{
    //    if (menu is null)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        //本级匹配
    //        if (menu.Router == router || menu.Operations.Select(x => x.Router).Any(x => x == router))
    //        {
    //            return true;
    //        }

    //        //子级匹配
    //        return ExistsRouter(menu.Menus, router);
    //    }
    //}

    /// <summary>
    /// 验证路由是否在权限菜单内
    /// </summary>
    /// <param name="menus"></param>
    /// <param name="router"></param>
    /// <returns></returns>
    //private bool ExistsRouter(List<IMenu>? menus, string router)
    //{
    //    if (menus?.Count > 0)
    //    {
    //        //本级匹配
    //        if (menus.Any(x => x.Router == router) || menus.Where(x => x.Operations != null).SelectMany(x => x.Operations).Any(x => x.Router == router))
    //        {
    //            return true;
    //        }
    //        else//子级匹配
    //        {
    //            return ExistsRouter(menus.Where(x => x.Menus != null).SelectMany(x => x.Menus)?.ToList(), router);
    //        }
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
}