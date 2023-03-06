//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IMenuRepository.cs
//摘要: 菜单仓储接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-01-07
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.Repositories;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Data.Entities;
using DbMenu = iMaxSys.Identity.Data.Entities.Menu;

namespace iMaxSys.Identity.Data.Repositories;

/// <summary>
/// 角色仓储接口
/// </summary>
public interface ITenantMenuRepository : IIdentityRepository<TenantMenu>
{
    /// <summary>
    /// get
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    Task<MenuResult?> GetAsync(long tenantId, long xppId);

    /// <summary>
    /// get
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    Task<MenuResult?> GetAsync(long tenantId, long xppId, IRole role);

    /// <summary>
    /// refresh
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    Task<MenuResult?> RefreshAsync(long tenantId, long xppId);

    /// <summary>
    /// refresh
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    Task<MenuResult?> RefreshAsync(long tenantId, long xppId, IRole role);

    /// <summary>
    /// refresh
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    Task RefreshAsync(long tenantId, long xppId, List<IRole> roles);

    /// <summary>
    /// 是否允许访问路由
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="role"></param>
    /// <param name="router"></param>
    /// <returns></returns>
    Task<bool> AllowAccessAsync(long tenantId, long xppId, IRole role, string router);
}