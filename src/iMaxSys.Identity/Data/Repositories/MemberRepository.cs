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

namespace iMaxSys.Identity.Data.Repositories;

/// <summary>
/// 成员仓储
/// </summary>
public class MemberRepository : EfRepository<DbMember>, IMemberRepository
{
    const string TAG = "id:";
    const string TAG_ACCESS = "a:";
    const string TAG_MEMBER = "m:";
    const string TAG_ACCESS_SECTION = $"{TAG}{TAG_ACCESS}";
    const string TAG_MEMBER_SECTION = $"{TAG}{TAG_MEMBER}";

    private readonly ICache _cache;
    private readonly MaxOption _option;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityCache"></param>
    public MemberRepository(IOptions<MaxOption> option, IdentityContext context, ICacheFactory cacheFactory) : base(context)
    {
        _option = option.Value;
        _cache = cacheFactory.GetService();
    }

    /// <summary>
    /// 获取访问Session
    /// </summary>
    /// <param name="token">令牌</param>
    /// <returns></returns>
    public async Task<IAccessSession?> GetAccessSessionAsync(string token)
    {
        return await _cache.GetAsync<AccessSession>($"{TAG_MEMBER_SECTION}{token}", true);
    }

    /// <summary>
    /// 获取成员
    /// </summary>
    /// <param name="memberId">成员id</param>
    /// <returns></returns>
    public async Task<IMember?> ReadAsync(long memberId)
    {
        return await _cache.GetAsync<Member>($"{TAG_MEMBER_SECTION}{memberId}", true);
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
        await _cache.DeleteAsync($"{TAG_MEMBER_SECTION}{memberId}", true);
    }

    /// <summary>
    /// 刷新member缓存
    /// </summary>
    /// <param name="member"></param>
    /// <param name="expires"></param>
    /// <returns></returns>
    public async Task RefreshAsync(IMember member, DateTime? expires = null)
    {
        if (member.Id > 0)
        {
            await _cache.SetAsync($"{TAG_MEMBER_SECTION}{member.Id}", member, expires ?? DateTime.Now.AddMinutes(_option.Identity.Expires), true);
        }
    }

    /// <summary>
    /// 刷新AccessChain
    /// </summary>
    /// <param name="oldToken"></param>
    /// <param name="accessChain"></param>
    /// <param name="expires"></param>
    /// <returns></returns>
    public async Task RefreshAccessChainAsync(string oldToken, IAccessChain accessChain, DateTime? expires = null)
    {
        //清除旧AccessSession和User
        if (!string.IsNullOrWhiteSpace(oldToken))
        {
            await _cache.DeleteAsync($"{TAG_ACCESS_SECTION}{oldToken}", true);
        }

        //设置新缓存
        await _cache.SetAsync($"{TAG_ACCESS_SECTION}{accessChain!.AccessSession!.Token}", accessChain.AccessSession, expires ?? DateTime.Now.AddMinutes(_option.Identity.Expires), true);
        if (accessChain.AccessSession.MemberId > 0)
        {
            await RefreshAsync(accessChain.Member!, expires);
        }
    }
}