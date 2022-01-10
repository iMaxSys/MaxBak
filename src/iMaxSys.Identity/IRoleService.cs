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

namespace iMaxSys.Identity;

/// <summary>
/// 角色服务接口
/// </summary>
public interface IRoleService : IDependency
{
    /// <summary>
    /// 获取角色
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IRole?> GetAsync(long id);

    /// <summary>
    /// 新增角色
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IRole> AddAsync(RoleModel model);

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IRole> UpdateAsync(RoleModel model);

    /// <summary>
    /// 移除角色
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveAsync(long id);

    /// <summary>
    /// 获取角色菜单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IMenu> GetMenuAsync(long id);

    /// <summary>
    /// 获取角色菜单
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    Task<IMenu> GetMenuAsync(IRole role);

    /// <summary>
    /// 获取完整菜单
    /// </summary>
    /// <param name="tenantId">租户Id</param>
    /// <returns></returns>
    Task<IMenu> GetFullMenuAsync(long tenantId = 0);
}