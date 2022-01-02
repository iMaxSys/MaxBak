//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MemberService.cs
//摘要: 成员服务
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------


using iMaxSys.Data;
using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Extentions;
using iMaxSys.Max.Identity;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Common.Enums;

using iMaxSys.Sns;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Repositories;
using iMaxSys.Max.Security.Cryptography;

using DbMember = iMaxSys.Identity.Data.Entities.Member;

namespace iMaxSys.Identity;

/// <summary>
/// 成员服务
/// </summary>
public class IdentityService : Service, IIdentityService
{
    const string PASSWORD = "wow#babylon";

    private readonly ISns _sns;
    private readonly IRoleService _roleService;
    private readonly IUserProvider _userProvider;
    private readonly ICheckCodeService _checkCodeService;

    //是否加载过正式用户
    bool _isGetRealMember = false;
    User _user;

    public IdentityService(IMapper mapper, IOptions<MaxOption> option, IIdentityCache cache, IUnitOfWork<MaxIdentityContext> unitOfWork, ISns sns, IRoleService roleService, IUserProvider userProvider, ICheckCodeService checkCodeService) : base(mapper, option, cache, unitOfWork)
    {
        _sns = sns;
        _roleService = roleService;
        _userProvider = userProvider;
        _checkCodeService = checkCodeService;
    }

    /// <summary>
    /// 获取访问串
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<IAccessChain> GetAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }

        //先按Token获取uid
        IAccessSession access = await _cache.GetAsync<AccessSession>($"{TAG}{TAG_ACCESS}{token}", true);
        if (access == null)
        {
            return null;
        }
        else
        {
            //按mid获取member
            IMember member = null;
            if (access.MemberId > 0)
            {
                member = await _cache.GetAsync<Max.Identity.Domain.Member>($"{TAG}{TAG_MEMBER}{access.MemberId}", true);
                var type = _userProvider.GetType(member.Type);
                if (type != null)
                {
                    member.User = member.GetUser(type);
                }
            }

            return new AccessChain
            {
                AccessSession = access,
                Member = member
            };
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
        var ac = await GetAsync(token);
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
        if (accessChain?.Member?.Status == Status.Disable)
        {
            throw new MaxException(ResultEnum.UserIsDisable);
        }

        //全开放
        if ("*" == _option.Identity.OpenRouters)
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

                if (accessChain == null)
                {
                    throw new MaxException(ResultEnum.UnLogin);
                }

                if (!accessChain.AccessSession.IsLogin)
                {
                    throw new MaxException(ResultEnum.Forbidden, router);
                }
            }
            else //严格模式
            {
                //在开放api中,则通行
                if (_option.Identity.OpenRouters.Contains(router))
                {
                    return;
                }

                if (accessChain == null)
                {
                    throw new MaxException(ResultEnum.UnLogin);
                }

                if (!accessChain.AccessSession.IsLogin)
                {
                    throw new MaxException(ResultEnum.Forbidden, router);
                }

                var role = await _roleService.GetRoleAsync(accessChain.Member.RoleId);

                if (accessChain.AccessSession.IsLogin && ExistsRouter(role.Menu, router))
                {
                    return;
                }
                else
                {
                    throw new MaxException(ResultEnum.Forbidden, router);
                }

            }
        }
    }

    /// <summary>
    /// 新增成员
    /// </summary>
    /// <param name="model"></param>
    /// <param name="ip"></param>
    /// <returns></returns>
    public async Task<IMember> AddMemberAsync(MemberModel model, string ip, long tenantId = 0)
    {
        DbMember dbMember = new DbMember();
        SetDbMember(model, dbMember);
        dbMember.TenantId = tenantId;

        var now = DateTime.Now;
        dbMember.JoinTime = now;
        dbMember.JoinIp = ip;

        //ip非空，则为自动注册
        if (!string.IsNullOrWhiteSpace(ip))
        {
            dbMember.LastLogin = now;
            dbMember.LastIp = ip;
        }
        dbMember.Salt = Guid.NewGuid().ToString().Replace("-", "");
        await _unitOfWork.GetRepo<DbMember>().AddAsync(dbMember);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<IMember>(dbMember);
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="oldPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    public async Task ChangePasswordAsync(long memberId, string oldPassword, string newPassword)
    {
        ISpecification<DbMember> spec = new Specification<DbMember>(x => x.Id == memberId).ApplyTracking();
        DbMember dbmebmer = await _unitOfWork.GetRepo<DbMember>().FirstOrDefaultAsync(spec);

        //用户是否存在
        if (dbmebmer == null)
        {
            throw new MaxException(ResultEnum.UserNotExsits);
        }

        //验证旧密码
        if (!CheckPassword(dbmebmer.Password, oldPassword, dbmebmer.Salt))
        {
            throw new MaxException(ResultEnum.PasswordError);
        }

        dbmebmer.Password = MakePassword(newPassword, dbmebmer.Salt);

        await _unitOfWork.GetRepo<DbMember>().UpdateAsync(dbmebmer);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// 获取成员
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IMember> GetMemberAsync(long id)
    {
        ISpecification<DbMember> spec = new Specification<DbMember>(x => x.Id == id);
        DbMember dbMember = await _unitOfWork.GetRepo<DbMember>().FirstOrDefaultAsync(spec);
        if (dbMember == null)
        {
            throw new MaxException(ResultEnum.UserNotExsits);
        }

        return _mapper.Map<IMember>(dbMember);
    }

    /// <summary>
    /// 获取用户关键值
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sids"></param>
    /// <returns></returns>
    public async Task<Dictionary<long, string>> GetKeysAsync(long id, long[] sids = null)
    {
        //获取会员信息
        ISpecification<DbMember> spec = new Specification<DbMember>(x => x.Id == id).AddInclude(x => x.MemberExts);
        var member = await _unitOfWork.GetRepo<DbMember>().FirstOrDefaultAsync(spec);
        return GetKeysAsync(member);
    }

    /// <summary>
    /// 获取用户关键值
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <param name="sids"></param>
    /// <returns></returns>
    public async Task<Dictionary<long, string>> GetKeysAsync(long id, int type, long[] sids = null)
    {
        //获取会员信息
        ISpecification<DbMember> spec = new Specification<DbMember>(x => x.UserId == id && x.Type == type).AddInclude(x => x.MemberExts);
        var member = await _unitOfWork.GetRepo<DbMember>().FirstOrDefaultAsync(spec);
        return GetKeysAsync(member);
    }

    /// <summary>
    /// 获取用户关键值
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    private Dictionary<long, string> GetKeysAsync(DbMember member)
    {
        Dictionary<long, string> dict = null;

        if (member != null)
        {
            dict = new Dictionary<long, string>
                {
                    { 0, member.Mobile }
                };
            foreach (var item in member.MemberExts)
            {
                dict.Add(item.XappSnsId, item.OpenId);
            }
        }

        return dict;
    }

    /// <summary>
    /// 获取完整菜单
    /// </summary>
    /// <param name="id">租户Id</param>
    /// <returns></returns>
    public async Task<IMenu> GetFullMenuAsync(long id)
    {
        return await _roleService.GetFullMenuAsync(id);
    }

    /// <summary>
    /// 获取成员菜单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IMenu> GetMemberMenuAsync(long id)
    {
        var role = await GetMemberRoleAsync(id);
        if (role == null)
        {
            return null;
        }

        return await _roleService.GetRoleMenuAsync(role);
    }

    /// <summary>
    /// 获取成员角色
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IRole> GetMemberRoleAsync(long id)
    {
        ISpecification<DbMember> spec = new Specification<DbMember>(x => x.Id == id).AddInclude(x => x.Role);
        DbMember dbMember = await _unitOfWork.GetRepo<DbMember>().FirstOrDefaultAsync(spec);

        if (dbMember.RoleId == 0)
        {
            return null;
        }

        return _mapper.Map<IRole>(dbMember.Role);
    }

    /// <summary>
    /// 获取成员角色
    /// </summary>
    /// <param name="accessChain"></param>
    /// <returns></returns>
    public async Task<IRole> GetMemberRoleAsync(IAccessChain accessChain)
    {
        long? roleId = accessChain?.Member?.RoleId;
        if (!roleId.HasValue)
        {
            return null;
        }

        long tenantId = accessChain?.Member?.TenantId ?? 0;
        string key = GetRoleKey(tenantId, roleId.Value);

        bool exists = await KeyExistsAsync(key);

        IRole role;
        if (exists)
        {
            role = await GetCacheAsync<IRole>(key);
        }
        else
        {
            IAuthority authority = await RefreshAuthorityAsync(tenantId);
            role = authority.Roles.FirstOrDefault(x => x.Id == roleId);
            //role.Menu = await GetRoleMenuAsync(role.Id);
        }

        return role;
    }

    /// <summary>
    /// 获取社交平台绑定的电话号码
    /// </summary>
    /// <param name="sid"></param>
    /// <param name="data"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public SnsPhoneNumber GetSnsPhoneNumber(long sid, string data, string key, string iv)
    {
        //XappSns sns = await _unitOfWork.GetRepo<XappSns>().GetAsync(sid);
        return _sns.GetPhoneNumber(data, key, iv);
    }

    /// <summary>
    /// 登录(用户名&密码)
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="ip"></param>
    /// <returns></returns>
    public async Task<AccessChain> LoginAsync(string userName, string password, string ip)
    {
        return await LoginAsync(null, userName, password, ip);
    }

    /// <summary>
    /// 登录(用户名&密码)
    /// </summary>
    /// <param name="types"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="ip"></param>
    /// <returns></returns>
    public async Task<AccessChain> LoginAsync(int[] types, string userName, string password, string ip)
    {
        //用户名空检查
        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new MaxException(ResultEnum.UserNameCantNull);
        }

        //密码空检查
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new MaxException(ResultEnum.PasswordCantNull);
        }

        AccessChain accessChain = new AccessChain();

        Expression<Func<DbMember, bool>> expr = x => x.LoginName == userName && !x.IsDeleted;

        if (types != null && types.Length > 0)
        {
            //此处整型的contains会被换为sql的位运算，先用大于代替
            expr = expr.And(x => x.Type >= types[0]);
        }

        ISpecification<DbMember> spec = new Specification<DbMember>(expr).AddInclude(x => x.Role);
        DbMember dbMember = await _unitOfWork.GetRepo<DbMember>().FirstOrDefaultAsync(spec);
        if (dbMember == null)
        {
            throw new MaxException(ResultEnum.UserNotExsits);
        }

        if (!(CheckPassword(dbMember.Password, password, dbMember.Salt)))
        {
            throw new MaxException(ResultEnum.PasswordError);
        }

        var token = MakeAccessToken();

        MemberSession memberSession = new MemberSession
        {
            Avatar = dbMember.Avatar,
            XappSnsId = 0,
            NickName = dbMember.NickName,
            Status = dbMember.Status.GetHashCode(),
            Token = token.Token,
            Expires = token.Expires,
            MemberId = dbMember.Id,
            TenantId = dbMember.TenantId,
            IsOfficial = dbMember.IsOfficial
        };

        await _unitOfWork.GetRepo<MemberSession>().AddAsync(memberSession);

        dbMember.LastLogin = DateTime.Now;
        dbMember.LastIp = ip;

        await _unitOfWork.GetRepo<DbMember>().UpdateAsync(dbMember);
        await _unitOfWork.SaveChangesAsync();

        accessChain.Member = _mapper.Map<IMember>(dbMember);
        var user = await GetRealMemberAsync(dbMember.UserId, dbMember.Type);
        if (user != null)
        {
            accessChain.Member.User = user;
            if (!string.IsNullOrWhiteSpace(user.Name))
            {
                accessChain.Member.Name = user.Name;
            }
            if (!string.IsNullOrWhiteSpace(user.Avatar))
            {
                accessChain.Member.Avatar = user.Avatar;
            }
            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                accessChain.Member.Email = user.Email;
            }
        }

        AccessSession accessSession = new AccessSession
        {
            Avatar = user?.Avatar ?? dbMember.Avatar,
            Name = user?.Name ?? (dbMember?.Name ?? dbMember.NickName),
            TenantId = dbMember.TenantId,
            Token = token.Token,
            Expires = token.Expires,
            AppSource = 0,
            AccountSource = 0,
            MemberId = dbMember.Id,
            IsLogin = true,
            IsOfficial = dbMember.IsOfficial,
            Status = dbMember.Status.GetHashCode()
        };

        accessChain.AccessSession = accessSession;

        //刷新accessChain
        await RefreshAsync(token.Token, accessChain);

        return accessChain;
    }

    /// <summary>
    /// 登录:code登录
    /// </summary>
    /// <param name="sid"></param>
    /// <param name="type"></param>
    /// <param name="code"></param>
    /// <param name="ip"></param>
    /// <returns></returns>
    public async Task<AccessChain> LoginAsync(long sid, int type, string code, string ip)
    {
        AccessChain accessChain = new AccessChain();
        DbMember dbMember;
        string token = string.Empty;

        ISpecification<XappSns> spec1 = new Specification<XappSns>(x => x.Id == sid).AddInclude(x => x.Xapp);
        var sns = await _unitOfWork.GetRepo<XappSns>().FirstOrDefaultAsync(spec1);

        //取访问配置
        AccessConfig accessConfig = await GetAccessConfigAsync(sns, code);

        //是否用第三方平台账号绑定过
        ISpecification<MemberExt> spec2 = new Specification<MemberExt>(x => x.XappSnsId == sid && x.OpenId == accessConfig.OpenId).AddInclude(x => x.Member).ApplyTracking();
        MemberExt memberExt = await _unitOfWork.GetRepo<MemberExt>().FirstOrDefaultAsync(spec2);
        DateTime now = DateTime.Now;

        //有表示已是会员,无进行快速注册,注册完成为非正式
        if (memberExt == null)
        {
            dbMember = new DbMember
            {
                Type = type,
                Start = now,
                AppSource = sns.Xapp.Source,
                AccountSource = sns.Source,
                Salt = Guid.NewGuid().ToString().Replace("-", ""),
                JoinTime = now,
                JoinIp = ip,
                LastLogin = now,
                LastIp = ip,
                IsOfficial = false,
                Status = Status.Enable,
                MemberExts = new List<MemberExt>()
            };

            MemberExt ext1 = new MemberExt
            {
                XappSnsId = sns.Id,
                OpenId = accessConfig.OpenId,
                Token = accessConfig.Token,
                Expires = accessConfig.Expires,
                Status = Status.Enable
            };
            dbMember.MemberExts.Add(ext1);
            await _unitOfWork.GetRepo<DbMember>().AddAsync(dbMember);
            await _unitOfWork.SaveChangesAsync();
        }
        else
        {
            dbMember = memberExt.Member;
            dbMember.LastIp = ip;
            dbMember.LastLogin = now;
            await CheckStatus(dbMember);

            //更新token
            token = memberExt.Token;
            memberExt.Token = accessConfig.Token;
            memberExt.Expires = accessConfig.Expires;
        }

        MemberSession memberSession = new MemberSession
        {
            Avatar = accessConfig.Avatar,
            XappSnsId = sid,
            Expires = accessConfig.Expires,
            OpenId = accessConfig.OpenId,
            NickName = accessConfig.NickName,
            SessionKey = accessConfig.SessionKey,
            Status = Status.Enable.GetHashCode(),
            Token = accessConfig.Token,
            MemberId = dbMember.Id,
            UnionId = accessConfig.UnionId,
            IsOfficial = true
        };
        await _unitOfWork.GetRepo<MemberSession>().AddAsync(memberSession);
        await _unitOfWork.SaveChangesAsync();

        AccessSession accessSession = new AccessSession
        {
            XappSnsId = sid,
            AccountId = accessConfig.AccountId,
            AppId = accessConfig.AppId,
            Avatar = accessConfig.Avatar,
            Name = accessConfig.NickName,
            SessionKey = accessConfig.SessionKey,
            OpenId = accessConfig.OpenId,
            UnionId = accessConfig.UnionId,
            TenantId = accessConfig.TenantId,
            Token = accessConfig.Token,
            Expires = accessConfig.Expires,
            AppSource = sns.Xapp.Source.GetHashCode(),
            AccountSource = sns.Source.GetHashCode(),
            MemberId = dbMember.Id,
            IsLogin = true,
            Status = dbMember.Status.GetHashCode(),
            IsOfficial = dbMember.IsOfficial
        };

        accessChain.AccessSession = accessSession;
        accessChain.Member = _mapper.Map<IMember>(dbMember);

        //刷新accessChain
        await RefreshAsync(token, accessChain);

        return accessChain;
    }

    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task LogoutAsync(string token)
    {
        await RemoveAsync(token);
    }

    /// <summary>
    /// 绑定社交账号
    /// </summary>
    /// <param name="token"></param>
    /// <param name="openId"></param>
    /// <returns></returns>
    public Task BindAsync(string token, string openId)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// 校验用户关键数据是否合法
    /// </summary>
    /// <param name="key"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task CheckKeyAsync(string key, int type)
    {
        await _userProvider.CheckAsync(key, type);
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task RegisterAsync(RegisterModel model)
    {
        string mobile = model.Mobile;
        bool needCheckCode = true;

        //账号平台来源
        PlatformSource source = PlatformSource.Max;

        ISpecification<XappSns> spec1 = new Specification<XappSns>(x => x.Id == model.XappSnsId).AddInclude(x => x.Xapp);
        var sns = await _unitOfWork.GetRepo<XappSns>().FirstOrDefaultAsync(spec1);

        AccessConfig accessConfig = await CheckSnsAccountAsync(sns, model.Code);
        source = sns.Source;

        //获取手机号对应的会员信息
        ISpecification<DbMember> spec2 = new Specification<DbMember>(x => x.Id == model.MemberId || x.Mobile == mobile || (!string.IsNullOrWhiteSpace(model.UserName) && x.LoginName == model.UserName)).AddInclude(x => x.MemberExts).ApplyTracking();
        var repo = _unitOfWork.GetRepo<DbMember>();

        DbMember dbMember = await repo.FirstOrDefaultAsync(spec2);

        //用户名密码注册,需要判断是否用该手机号注册过,其他方式注册存在多社交账户绑定同一member,就不判断
        if (sns.Source == PlatformSource.Max && dbMember != null || dbMember.IsOfficial)
        {
            throw new MaxException(ResultEnum.UserExists);
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
            case PlatformSource.Max:
                if (string.IsNullOrWhiteSpace(model.UserName))
                {
                    throw new MaxException(ResultEnum.UserNameCantNull);
                }
                if (string.IsNullOrWhiteSpace(model.Password))
                {
                    throw new MaxException(ResultEnum.PasswordCantNull);
                }
                //是否需要手机号码
                if (sns.Xapp.NeedMobile && string.IsNullOrWhiteSpace(model.Mobile))
                {
                    throw new MaxException(ResultEnum.NeedMobile);
                }
                break;
            case PlatformSource.WeChat:
                //有code,就不需要openId,但二者不可同时为空
                if (string.IsNullOrWhiteSpace(model.Code) && string.IsNullOrWhiteSpace(model.OpenId))
                {
                    throw new MaxException(ResultEnum.CodeOpenIdCantNull);
                }
                //是否需要手机号码
                if (sns.Xapp.NeedMobile && string.IsNullOrWhiteSpace(model.Mobile) && string.IsNullOrWhiteSpace(model.EncryptedData))
                {
                    throw new MaxException(ResultEnum.NeedMobile);
                }
                //社交平台获取电话号码,则不需要验证码
                if (!string.IsNullOrWhiteSpace(model.EncryptedData))
                {
                    ISpecification<MemberSession> spec = new Specification<MemberSession>(x => x.XappSnsId == model.XappSnsId && x.Token == model.Token);
                    MemberSession xmemberSession = await _unitOfWork.GetRepo<MemberSession>().FirstOrDefaultAsync(spec);
                    SnsPhoneNumber phoneNumber = GetSnsPhoneNumber(model.XappSnsId, model.EncryptedData, xmemberSession.SessionKey, model.IV);
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
            await _checkCodeService.CheckAsync(model.XappSnsId, BizSource.BindCheckCode.GetHashCode(), model.MemberId, model.Mobile, model.CheckCode);
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

    public Task RemoveMemberAsync(long id)
    {
        throw new System.NotImplementedException();
    }

    public Task<IMember> UpdateMemberAsync(MemberModel model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> RemoveAsync(string token)
    {
        return await _cache.DeleteAsync($"{TAG}{TAG_ACCESS}{token}", true);
    }

    /// <summary>
    /// 刷新访问串
    /// </summary>
    /// <param name="oldToken"></param>
    /// <param name="accessChain"></param>
    /// <returns></returns>
    public async Task RefreshAsync(string oldToken, IAccessChain accessChain)
    {
        //清除旧AccessSession和User
        if (!string.IsNullOrWhiteSpace(oldToken))
        {
            await RemoveAsync(oldToken);
        }

        DateTime expires = DateTime.Now.AddMinutes(_option.Identity.Expires);
        await _cache.SetAsync($"{TAG}{TAG_ACCESS}{accessChain.AccessSession.Token}", accessChain.AccessSession, expires, true);
        if (accessChain.AccessSession.MemberId > 0)
        {
            await RefreshMemberAsync(accessChain.Member, expires);
        }
    }

    /// <summary>
    /// 刷新member缓存
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public async Task RefreshMemberAsync(IMember member, DateTime? expires = null)
    {
        object user = await GetRealMemberAsync(member.UserId, member.Type);
        member.User = user as User;
        member.UserJson = JsonSerializer.Serialize(user);

        if (member?.Id > 0)
        {
            DateTime time = expires ?? DateTime.Now.AddMinutes(_option.Identity.Expires);
            await _cache.SetAsync($"{TAG}{TAG_MEMBER}{member.Id}", member, time, true);
        }
    }

    /// <summary>
    /// 使用MemberUpdateModel设置DbMember
    /// </summary>
    /// <param name="model"></param>
    /// <param name="dbMember"></param>
    private void SetDbMember(MemberModel model, DbMember dbMember)
    {
        dbMember.UserId = model.ExtId ?? dbMember.UserId;
        dbMember.Avatar = model.Avatar ?? dbMember.Avatar;
        dbMember.Birthday = model.Birthday ?? dbMember.Birthday;
        dbMember.DepartmentId = model.DepartmentId ?? dbMember.DepartmentId;
        dbMember.Email = model.Email ?? dbMember.Email;
        dbMember.End = model.End ?? dbMember.End;
        dbMember.Gender = model.Gender ?? dbMember.Gender;
        dbMember.IsOfficial = model.IsOfficial ?? dbMember.IsOfficial;
        dbMember.Mobile = model.Mobile ?? dbMember.Mobile;
        dbMember.Name = model.Name ?? dbMember.Name;
        dbMember.QuickCode = model.QuickCode ?? dbMember.QuickCode;
        dbMember.NickName = model.NickName ?? dbMember.NickName;
        dbMember.RoleId = model.RoleId ?? dbMember.RoleId;
        dbMember.Start = model.Start ?? dbMember.Start;
        dbMember.Status = model.Status ?? dbMember.Status;
        dbMember.LoginName = model.UserName ?? dbMember.LoginName;
        dbMember.Type = model.Type ?? dbMember.Type;
    }

    /// <summary>
    /// 生成密码
    /// </summary>
    /// <param name="source"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    private string MakePassword(string source, string salt)
    {
        return Md5.Hash($"{salt}-{source}-{salt}-{source}");
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
    /// 获取访问配置
    /// </summary>
    /// <param name="sns"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    private async Task<AccessConfig> GetAccessConfigAsync(XappSns sns, string code)
    {
        SnsAuth snsAuth = new SnsAuth
        {
            AppId = sns.AppId,
            AppSecret = sns.AppSecret,
            Code = code
        };

        AccessConfig accessConfig = await _sns.GetAccessConfigAsync(snsAuth);
        accessConfig.AccountId = sns.AccountId;
        accessConfig.Platform = (Platform)sns.Source;
        accessConfig.Status = sns.Status;

        var token = MakeAccessToken();
        accessConfig.Token = token.Token;
        accessConfig.Expires = token.Expires;

        return accessConfig;
    }

    /// <summary>
    /// 检查用户状态
    /// </summary>
    /// <param name="dbMember"></param>
    /// <returns></returns>
    private async Task CheckStatus(DbMember dbMember)
    {
        var realMember = await GetRealMemberAsync(dbMember.UserId, dbMember.Type);

        //禁用判断
        if (dbMember?.Status == Status.Disable || realMember?.Status == Status.Disable)
        {
            throw new MaxException(ResultEnum.UserIsDisable);
        }

        //过期判断
        if (dbMember?.End < DateTime.Now || realMember?.End < DateTime.Now)
        {
            throw new MaxException(ResultEnum.UserIsExpired);
        }
    }

    /// <summary>
    /// 获取真实会员信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private async Task<User> GetRealMemberAsync(long id, int type)
    {
        if (!_isGetRealMember && id > 0 && _userProvider != null)
        {
            _user = await _userProvider.GetAsync(id, type);
            _isGetRealMember = true;
        }

        return _user;
    }

    /// <summary>
    /// 获取真实会员信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private async Task<IUser> GetRealMemberAsync(string mobile, int type)
    {
        if (!_isGetRealMember && !string.IsNullOrWhiteSpace(mobile) && _userProvider != null)
        {
            _user = await _userProvider.GetAsync(mobile, type);
            _isGetRealMember = true;
        }

        return _user;
    }

    /// <summary>
    /// 激活
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private async Task<IUser> ActiveUserAsync(string mobile, string avatar, int type)
    {
        if (!_isGetRealMember && !string.IsNullOrWhiteSpace(mobile) && _userProvider != null)
        {
            _user = await _userProvider.ActivateAsync(mobile, avatar, type);
            _isGetRealMember = true;
        }

        return _user;
    }

    /// <summary>
    /// 检查社交账号是否已绑定过系统账号
    /// </summary>
    /// <param name="sns"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    private async Task<AccessConfig> CheckSnsAccountAsync(XappSns sns, string code)
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

    private async Task UpdateSession(string token, long xappSnsId, string openId)
    {
        //先检查是否调过login,调用过就有AccessChain
        IAccessChain accessChain = await GetAsync(token);

        //是否用第三方平台账号绑定过
        ISpecification<MemberExt> spec = new Specification<MemberExt>(x => x.XappSnsId == xappSnsId && x.OpenId == openId).AddInclude(x => x.Member);
        MemberExt memberExt = await _unitOfWork.GetRepo<MemberExt>().FirstOrDefaultAsync(spec);

        //绑定过
        if (memberExt != null)
        {
            accessChain.AccessSession.MemberId = memberExt.MemberId;
            accessChain.AccessSession.Status = memberExt.Status.GetHashCode();
            DbMember dbMember = memberExt.Member;
            //DbMember dbMember = await _unitOfWork.GetRepository<DbMember>().FirstOrDefaultAsync(predicate: x => x.Id == memberExt.MemberId);

            accessChain.Member = _mapper.Map<IMember>(dbMember);
            //accessChain.Member = new Max.Identity.Domain.Member
            //{
            //    Id = dbMember.Id,
            //    UserId = dbMember.UserId,
            //    LoginName = dbMember.LoginName,
            //    Mobile = dbMember.Mobile,
            //    Avatar = dbMember.Avatar,
            //    Email = dbMember.Email,
            //    Name = dbMember.Name ?? dbMember.NickName,
            //    NickName = dbMember.NickName,
            //    Start = dbMember.Start,
            //    End = dbMember.End,
            //    IsOfficial = dbMember.IsOfficial,
            //    Status = dbMember.Status
            //};
        }

        await RefreshAsync(token, accessChain);
    }
}