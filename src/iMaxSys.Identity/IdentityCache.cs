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
using iMaxSys.Identity.Models;

namespace iMaxSys.Identity;

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
    //const string TAG_TENANT = "t";
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
    public async Task<bool> HasMemberAsync(long tenantId, long memberId) => await _cache.KeyExistsAsync(GetMemberKey(tenantId, memberId), global);

    /// <summary>
    /// 获取member
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="memberId"></param>
    /// <returns></returns>
    public async Task<IMember?> GetMemberAsync(long tenantId, long memberId) => await _cache.GetAsync<MemberModel>(GetMemberKey(tenantId, memberId), global);

    /// <summary>
    /// SetMemberAsync
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="member"></param>
    /// <returns></returns>
    public async Task SetMemberAsync(long tenantId, IMember member) => await _cache.SetAsync(GetMemberKey(tenantId, member.Id), member, new TimeSpan(0, _option.Identity.Expires, 0), global);

    /// <summary>
    /// HasTenantMenu
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    public async Task<bool> HasXppMenuAsync(long tenantId, long xppId) => await _cache.KeyExistsAsync(GetXppMenuKey(tenantId, xppId), global);

    /// <summary>
    /// 获取租户应用菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    public async Task<MenuModel?> GetXppMenuAsync(long tenantId, long xppId) => await _cache.GetAsync<MenuModel>(GetXppMenuKey(tenantId, xppId), global);

    /// <summary>
    /// 设置租户应用菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="menu"></param>
    /// <returns></returns>
    public async Task SetXppMenuAsync(long tenantId, long xppId, IMenu menu) => await _cache.SetAsync(GetXppMenuKey(tenantId, xppId), menu, new TimeSpan(0, _option.Identity.Expires, 0), global);

    /// <summary>
    /// HasRoleMenu
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<bool> HasRoleMenuAsync(long tenantId, long xppId, long roleId) => await _cache.KeyExistsAsync(GetRoleMenuKey(tenantId, xppId, roleId), global);

    /// <summary>
    /// HasRoleMenu
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<MenuModel?> GetRoleMenuAsync(long tenantId, long xppId, long roleId) => await _cache.GetAsync<MenuModel>(GetRoleMenuKey(tenantId, xppId, roleId), global);

    /// <summary>
    /// 设置租户角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <param name="menu"></param>
    /// <returns></returns>
    public async Task SetRoleMenuAsync(long tenantId, long xppId, long roleId, IMenu menu) => await _cache.SetAsync(GetRoleMenuKey(tenantId, xppId, roleId), menu, new TimeSpan(0, _option.Identity.Expires, 0), global);

    /// <summary>
    /// HasRole
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<bool> HasRoleAsync(long tenantId, long xppId, long roleId) => await _cache.KeyExistsAsync(GetRoleKey(tenantId, xppId, roleId), global);

    /// <summary>
    /// 获取角色
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<IRole?> GetRoleAsync(long tenantId, long xppId, long roleId) => await _cache.GetAsync<RoleModel>(GetRoleKey(tenantId, xppId, roleId), global);

    /// <summary>
    /// 设置角色
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public async Task SetRoleAsync(long tenantId, long xppId, IRole role) => await _cache.SetAsync(GetRoleKey(tenantId, xppId, role.Id), role, new TimeSpan(0, _option.Identity.Expires, 0), global);

    /// <summary>
    /// 移除角色
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task RemoveRoleAsync(long tenantId, long xppId, long roleId)
    {
        await _cache.DeleteAsync(GetRoleKey(tenantId, xppId, roleId), global);
    }

    /// <summary>
    /// 获取成员key
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="memberId"></param>
    /// <returns></returns>
    private string GetMemberKey(long tenantId, long memberId) => $"{tagMember}{tenantId}{_cache.Separator}{memberId}";

    /// <summary>
    /// 获取应用菜单key
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    private string GetXppMenuKey(long tenantId, long xppId) => $"{tagMenu}{xppId}{_cache.Separator}{tenantId}";

    /// <summary>
    /// 获取角色菜单key
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    private string GetRoleMenuKey(long tenantId, long xppId, long roleId) => $"{tagMenu}{xppId}{_cache.Separator}{tenantId}{_cache.Separator}{roleId}";

    /// <summary>
    /// 获取角色key
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    private string GetRoleKey(long tenantId, long xppId, long roleId) => $"{tagMenu}{xppId}{_cache.Separator}{tenantId}{_cache.Separator}{roleId}";
}