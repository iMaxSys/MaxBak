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
using iMaxSys.Max.Extentions;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.EFCore.Repositories;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Common;
using iMaxSys.Identity.Data.EFCore;
using DbMember = iMaxSys.Identity.Data.Entities.Member;

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
    /// find
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public async Task<DbMember> FindAsync(long memberId)
    {
        var dbMenu = await FirstOrDefaultAsync(x => x.Id == memberId);

        if (dbMenu is null)
        {
            throw new MaxException(ResultCode.MemberNotExists);
        }
        else
        {
            return dbMenu;
        }
    }

    /// <summary>
    /// 获取成员
    /// </summary>
    /// <param name="memberId">成员id</param>
    /// <returns></returns>
    public async Task<IMember?> GetAsync(long memberId)
    {
        var member = await Cache.GetAsync<MemberModel>(GetMemberKey(memberId), true);
        return member ?? await RefreshMemberAsync(memberId);
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
    /// 移除访问session
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task RemoveAccessSessionAsync(string token)
    {
        await Cache.DeleteAsync(GetAccessKey(token), _global);
    }

    /// <summary>
    /// 获取访问Chain
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IAccessChain?> GetAccessChainAsync(string token)
    {
        throw new NotImplementedException();
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
    /// 刷新访问Chain
    /// </summary>
    /// <param name="accessChain"></param>
    /// <param name="oldToken"></param>
    /// <returns></returns>
    public async Task RefreshAccessSessionAsync(IAccessChain accessChain, string? oldToken = null)
    {
        if (!string.IsNullOrWhiteSpace(oldToken))
        {
            await Cache.DeleteAsync(GetAccessKey(oldToken), true);
        }

        //设置新缓存
        if (accessChain.AccessSession is not null && !string.IsNullOrWhiteSpace(accessChain.AccessSession?.Token))
        {
            await Cache.SetAsync(GetAccessKey(accessChain.AccessSession.Token), accessChain.AccessSession, DateTime.Now.AddMinutes(Option.Identity.Expires), true);

            if (accessChain.AccessSession.MemberId > 0 && accessChain.Member is not null)
            {
                await RefreshMemberAsync(accessChain.Member, accessChain.User);
            }
        }
    }

    public async Task<IMember> RefreshMemberAsync(long memberId, IUser? user = null)
    {
        var member = await FindAsync(memberId);
        var model = Mapper.Map<MemberModel>(member);
        await RefreshMemberAsync(model, user);
        return model;
    }

    public async Task<IMember> RefreshMemberAsync(IMember member, IUser? user = null)
    {
        await Cache.DeleteAsync(GetMemberKey(member.Id), true);
        await Cache.SetAsync(GetMemberKey(member.Id), member, DateTime.Now.AddMinutes(Option.Identity.Expires), true);
        if (member.UserId > 0 && user is not null)
        {
            await RefreshUserAsync(user);
        }
        return member;
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