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
using iMaxSys.Identity.Common;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Repositories;
using iMaxSys.Identity.Data.Entities;
using DbMember = iMaxSys.Identity.Data.Entities.Member;
using iMaxSys.Data.Entities.App;

namespace iMaxSys.Identity;

/// <summary>
/// MemberService
/// </summary>
public class MemberService : ServiceBase, IMemberService
{
    /// <summary>
    /// 用户提供者
    /// </summary>
    private readonly IUserProvider _userProvider;

    #region 构造

    public MemberService(IMapper mapper, IOptions<MaxOption> option, IUnitOfWork<IdentityContext> unitOfWork, IUserProvider userProvider) : base(mapper, option, unitOfWork)
    {
        _userProvider = userProvider;
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

        IMemberRepository repository = UnitOfWork.GetCustomRepository<IMemberRepository>();

        //先按Token获取uid
        IAccessSession? access = await repository.GetAccessSessionAsync(token);

        if (access == null)
        {
            return null;
        }

        //按mid获取member
        IMember? member = null;
        if (access.MemberId.HasValue)
        {
            member = await repository.ReadAsync(access.MemberId.Value);

            if (member != null)
            {
                var type = _userProvider.GetType(member.Type);
                if (type != null)
                {
                    member.User = member.GetUser(type);
                }
            }
        }

        return new AccessChain
        {
            AccessSession = access,
            Member = member
        };
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
        await UnitOfWork.GetCustomRepository<IMemberRepository>().AddAsync(dbMember);
        await UnitOfWork.SaveChangesAsync();
        return Mapper.Map<IMember>(dbMember);
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
        await UnitOfWork.GetCustomRepository<IMemberRepository>().RemoveAsync(memberId);
        await UnitOfWork.SaveChangesAsync();
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
        var member = await UnitOfWork.GetCustomRepository<IMemberRepository>().FindAsync(id);
        if (member != null)
        {
            return Mapper.Map<IMember>(member);
        }
        else
        {
            throw new MaxException(IdentityResultEnum.MemberNotExists);
        }
    }

    #endregion

    #region UpdateAsync

    /// <summary>
    /// Get member
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IMember?> UpdateAsync(MemberModel model)
    {
        //成员id判空
        if (!model.Id.HasValue)
        {
            throw new MaxException(IdentityResultEnum.MemberIdCantNull);
        }

        IMemberRepository respoitory = UnitOfWork.GetCustomRepository<IMemberRepository>();
        var member = await respoitory.FindAsync(model.Id);
        if (member == null)
        {
            throw new MaxException(IdentityResultEnum.MemberNotExists);
        }

        SetMember(model, member);
        respoitory.Update(member);
        await UnitOfWork.SaveChangesAsync();

        var result = Mapper.Map<IMember>(member);
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
        await UnitOfWork.GetCustomRepository<IMemberRepository>().RefreshAsync(member);
    }

    #endregion

    #region RefreshAcceeChainAsync

    /// <summary>
    /// 刷新AcceeChain缓存
    /// </summary>
    /// <param name="oldToken"></param>
    /// <param name="accessChain"></param>
    /// <returns></returns>
    public async Task RefreshAcceeChainAsync(string oldToken, IAccessChain accessChain)
    {
        await UnitOfWork.GetCustomRepository<IMemberRepository>().RefreshAccessChainAsync(oldToken, accessChain);
    }

    #endregion

    #region RegisterAsync

    public async Task RegisterAsync(RegisterModel model)
    {
        string mobile = model.Mobile;
        bool needCheckCode = true;

        //账号平台来源
        PlatformSource source = PlatformSource.Max;

        //获取sns
        var xppSns = await UnitOfWork.GetRepository<XppSns>().FirstOrDefaultAsync(x => x.Id == model.XppSnsId, null, x => x.Include(y => y.Xpp));

        AccessConfig accessConfig = await CheckSnsAccountAsync(sns, model.Code);
        source = sns.Source;

        //获取手机号对应的会员信息
        ISpecification<DbMember> spec2 = new Specification<DbMember>(x => x.Id == model.MemberId || x.Mobile == mobile || (!string.IsNullOrWhiteSpace(model.UserName) && x.LoginName == model.UserName)).AddInclude(x => x.MemberExts).ApplyTracking();
        var repo = _unitOfWork.GetRepo<DbMember>();

        DbMember dbMember = await repo.FirstOrDefaultAsync(spec2);

        //用户名密码注册,需要判断是否用该手机号注册过,其他方式注册存在多社交账户绑定同一member,就不判断
        if (sns.Source == PlatformSource.Max && dbMember != null || dbMember.IsOfficial)
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
            case PlatformSource.Max:
                if (string.IsNullOrWhiteSpace(model.UserName))
                {
                    throw new MaxException(ResultCode.UserNameCantNull);
                }
                if (string.IsNullOrWhiteSpace(model.Password))
                {
                    throw new MaxException(ResultCode.PasswordCantNull);
                }
                //是否需要手机号码
                if (sns.Xapp.NeedMobile && string.IsNullOrWhiteSpace(model.Mobile))
                {
                    throw new MaxException(ResultCode.NeedMobile);
                }
                break;
            case PlatformSource.WeChat:
                //有code,就不需要openId,但二者不可同时为空
                if (string.IsNullOrWhiteSpace(model.Code) && string.IsNullOrWhiteSpace(model.OpenId))
                {
                    throw new MaxException(ResultCode.CodeOpenIdCantNull);
                }
                //是否需要手机号码
                if (sns.Xapp.NeedMobile && string.IsNullOrWhiteSpace(model.Mobile) && string.IsNullOrWhiteSpace(model.EncryptedData))
                {
                    throw new MaxException(ResultCode.NeedMobile);
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
        if (id > 0 && _userProvider != null)
        {
            return await _userProvider.GetAsync(id, type);
        }
        else
        {
            return null;
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
        if (!string.IsNullOrWhiteSpace(mobile) && _userProvider != null)
        {
            return await _userProvider.GetAsync(mobile, type);
        }
        else
        {
            return null;
        }
    }

    #endregion
}