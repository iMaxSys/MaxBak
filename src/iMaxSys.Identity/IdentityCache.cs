//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IIdentityCache.cs
//摘要: 身份缓存接口 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-29
//----------------------------------------------------------------

using iMaxSys.Max.Caching;
using iMaxSys.Max.Options;
using iMaxSys.Max.Identity.Domain;

namespace iMaxSys.Identity;

/*
/// <summary>
/// 身份缓存
/// </summary>
public class IdentityCache : IIdentityCache
{
    //全局缓存标志
    const bool global = true;

    const string TAG = "id:";
    const string TAG_ACCESS = "a";
    const string TAG_MEMBER = "u";
    const string TAG_TENANT = "t";
    const string TAG_ROLE = "r";
    const string TAG_MENU = "m";

    readonly string tagId = string.Empty;
    readonly string tagAccess = string.Empty;
    readonly string tagMember = string.Empty;
    readonly string tagRole = string.Empty;
    readonly string tagMenu = string.Empty;

    private readonly ICache _cache;
    private readonly MaxOption _option;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="cacheFactory"></param>
    public IdentityCache(IOptions<MaxOption> option, ICacheFactory cacheFactory)
    {
        _option = option.Value;
        _cache = cacheFactory.GetService();

        tagId = $"{TAG}{_cache.Separator}";
        tagAccess = $"{tagId}{TAG_ACCESS}{_cache.Separator}";
        tagMember = $"{tagId}{TAG_MEMBER}{_cache.Separator}";
        tagRole = $"{tagId}{TAG_ROLE}{_cache.Separator}";
        tagMenu = $"{tagId}{TAG_MENU}{_cache.Separator}";
    }

    /// <summary>
    /// HasMember
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="memberId"></param>
    /// <returns></returns>
    public async Task<bool> HasMember(long tenantId, long memberId) => await _cache.KeyExistsAsync($"{tagMember}{tenantId}{_cache.Separator}{memberId}", global);

    /// <summary>
    /// SetMember
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="member"></param>
    /// <returns></returns>
    public async Task SetMember(long tenantId, IMember member)
    {
        await _cache.SetAsync($"{tagMember}{tenantId}{_cache.Separator}{member.Id}", member, new TimeSpan(0, _option.Identity.Expires, 0));
    }

    /// <summary>
    /// HasTenantMenu
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    public async Task<bool> HasTenantMenu(long tenantId, long xppId) => await _cache.KeyExistsAsync($"{tagMenu}{xppId}{_cache.Separator}{tenantId}");

    /// <summary>
    /// HasRoleMenu
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<bool> HasRoleMenu(long tenantId, long xppId, long roleId) => await _cache.KeyExistsAsync($"{tagMenu}{xppId}{_cache.Separator}{tenantId}{_cache.Separator}{roleId}");

    /// <summary>
    /// HasRole
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<bool> HasRole(long tenantId, long xppId, long roleId) => await _cache.KeyExistsAsync($"{tagMenu}{xppId}{_cache.Separator}{tenantId}{_cache.Separator}{roleId}"); 
}
*/