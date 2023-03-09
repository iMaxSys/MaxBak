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

using iMaxSys.Max.Collection;
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
    Task<RoleResult> GetAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// get
    /// </summary>
    /// <param name="accessChain"></param>
    /// <returns></returns>
    Task<RoleResult> GetAsync(IAccessChain accessChain);

    /// <summary>
    /// refresh
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<RoleResult> RefreshAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// add
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RoleResult> AddAsync(AddRoleRequest request);

    /// <summary>
    /// update
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<RoleResult> UpdateAsync(UpdateRoleRequest request);

    /// <summary>
    /// remove
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task RemoveAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// 获取roles
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<PagedList<RoleResult>> AllAsync(long tenantId, string name);

    /// <summary>
    /// 获取roles
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<PagedList<RoleResult>> GetListAsync(RolesRequest request);

    /// <summary>
    /// 获取role
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<RoleResult> GetAsync(RoleRequest request);
}