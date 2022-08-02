//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IRoleService.cs
//摘要: 角色服务接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.DependencyInjection;
using iMaxSys.Identity.Models;
using DbRole = iMaxSys.Identity.Data.Entities.Role;

namespace iMaxSys.Identity;

/// <summary>
/// 角色服务接口
/// </summary>
public interface IRoleService : IDependency
{
    /// <summary>
    /// get
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<RoleModel> GetAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// refresh
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<RoleModel> RefreshAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// refresh
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="dbRole"></param>
    /// <returns></returns>
    Task<RoleModel> RefreshAsync(long tenantId, long xppId, DbRole dbRole);

    /// <summary>
    /// add
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RoleModel> AddAsync(long tenantId, long xppId, RoleModel model);

    /// <summary>
    /// update
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RoleModel> UpdateAsync(long tenantId, long xppId, RoleModel model);

    /// <summary>
    /// remove
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveAsync(long id);
}