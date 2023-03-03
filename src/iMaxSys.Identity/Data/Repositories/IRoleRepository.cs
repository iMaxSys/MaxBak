//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IRoleRepository.cs
//摘要: 角色仓储接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-01-07
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.Repositories;
using iMaxSys.Identity.Data.Entities;
using DbRole = iMaxSys.Identity.Data.Entities.Role;
using iMaxSys.Identity.Models;

namespace iMaxSys.Identity.Data.Repositories;

/// <summary>
/// 角色仓储接口
/// </summary>
public interface IRoleRepository : IRepository<DbRole>
{
    /// <summary>
    /// get
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<RoleResult> GetAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// find
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<DbRole> FindAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// RemoveAsync
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task RemoveAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// RefreshAsync
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<RoleResult> RefreshAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// RefreshAsync
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="dbRole"></param>
    /// <returns></returns>
    Task<RoleResult> RefreshAsync(long tenantId, long xppId, DbRole dbRole);
}