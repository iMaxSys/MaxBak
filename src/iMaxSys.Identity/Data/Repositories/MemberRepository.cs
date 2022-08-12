//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MemberRepository.cs
//摘要: 成员仓储
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Max.Caching;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.EFCore.Repositories;
using iMaxSys.Identity.Data.EFCore;
using DbMember = iMaxSys.Identity.Data.Entities.Member;
using iMaxSys.Identity.Models;

namespace iMaxSys.Identity.Data.Repositories;

/// <summary>
/// 成员仓储
/// </summary>
public class MemberRepository : IdentityRepository<DbMember>, IMemberRepository
{

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    /// <param name="option"></param>
    /// <param name="cacheFactory"></param>
    public MemberRepository(IdentityContext context, IMapper mapper, IOptions<MaxOption> option, ICacheFactory cacheFactory) : base(context, mapper, option, cacheFactory)
    {
    }

    /// <summary>
    /// 获取访问Session
    /// </summary>
    /// <param name="token">令牌</param>
    /// <returns></returns>
    public async Task<IAccessSession?> GetAccessSessionAsync(string token)
    {
        return await Cache.GetAsync<AccessSession>(GetAccessKey(token), _global);
    }

    /// <summary>
    /// 获取成员
    /// </summary>
    /// <param name="memberId">成员id</param>
    /// <returns></returns>
    public async Task<IMember?> GetAsync(long memberId)
    {
        return await Cache.GetAsync<Member>(GetMemberKey(memberId), true);
    }

    /// <summary>
    /// RemoveAsync
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    public async Task RemoveAsync(long memberId)
    {
        //软删除
        this.Remove(memberId);

        //清除缓存
        await Cache.DeleteAsync(GetMemberKey(memberId), true);
    }

    /// <summary>
    /// 刷新member缓存
    /// </summary>
    /// <param name="member"></param>
    /// <param name="expires"></param>
    /// <returns></returns>
    public async Task RefreshAsync(long memberId)
    {
        var member = await FindAsync(memberId);
        if (member is not null)
        {
            var model = Mapper.Map<MemberModel>(member);
            await Cache.SetAsync(GetMemberKey(model.Id), model, DateTime.Now.AddMinutes(Option.Identity.Expires), true);
        }
    }

    /// <summary>
    /// 刷新member缓存
    /// </summary>
    /// <param name="member"></param>
    /// <param name="expires"></param>
    /// <returns></returns>
    public async Task RefreshAsync(IMember member)
    {
        if (member.Id > 0)
        {
            await Cache.SetAsync(GetMemberKey(member.Id), member, DateTime.Now.AddMinutes(Option.Identity.Expires), true);
        }
    }

    //实体获取甚至缓存

    public async Task RefreshAccessSessionAsync(string oldToken, IAccessSession accessSession, IMember? member = null, IUser? user = null)
    {
        if (!string.IsNullOrWhiteSpace(oldToken))
        {
            await Cache.DeleteAsync(GetAccessKey(oldToken), true);
        }

        //设置新缓存
        if (!string.IsNullOrWhiteSpace(accessSession.Token))
        {
            await Cache.SetAsync(GetAccessKey(accessSession.Token), accessSession, DateTime.Now.AddMinutes(Option.Identity.Expires), true);

            if (accessSession.MemberId > 0 && member is not null)
            {
                await RefreshMemberAsync(member, user);
            }
        }
    }

    public async Task RefreshMemberAsync(IMember member, IUser? user = null)
    {
        await Cache.DeleteAsync(GetMemberKey(member.Id), true);
        await Cache.SetAsync(GetMemberKey(member.Id), member, DateTime.Now.AddMinutes(Option.Identity.Expires), true);
        if (member.UserId > 0 && user is not null)
        {
            await RefreshUserAsync(user);
        }
    }

    public async Task RefreshUserAsync(IUser user)
    {
        await Cache.DeleteAsync(GetUserKey(user.Id), true);
        await Cache.SetAsync(GetUserKey(user.Id), user, DateTime.Now.AddMinutes(Option.Identity.Expires), true);
    }

    /// <summary>
    /// GetAccessKey
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private string GetAccessKey(string token) => $"{_tagAccess}{token}";

    /// <summary>
    /// GetMemberKey
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    private string GetMemberKey(long memberId) => $"{_tagMember}{memberId}";

    /// <summary>
    /// GetUserKey
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    private string GetUserKey(long userId) => $"{_tagUser}{userId}";
}