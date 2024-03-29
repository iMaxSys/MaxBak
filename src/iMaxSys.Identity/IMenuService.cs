﻿//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IMenuService.cs
//摘要: 菜单服务接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Collection.Trees;
using iMaxSys.Max.DependencyInjection;
using iMaxSys.Core.Services;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Data.Entities;

using DbMenu = iMaxSys.Identity.Data.Entities.Menu;
using DbRole = iMaxSys.Identity.Data.Entities.Role;

namespace iMaxSys.Identity;

/// <summary>
/// 菜单服务接口
/// </summary>
public interface IMenuService : ITreeService<DbMenu, MenuResult>, IDependency
{
    /// <summary>
    /// 获取应用角色菜单Ids
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    Task<MenuIdsResult?> GetXppMenuIdsAsync(long tenantId, long xppId);

    /// <summary>
    /// 获取应用色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    Task<MenuResult?> GetXppMenuAsync(long tenantId, long xppId);

    /// <summary>
    /// 获取角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<MenuResult?> GetRoleMenuAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// 获取角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<MenuResult?> GetRoleMenuAsync(IAccessChain accessChain);


    /// <summary>
    /// 刷新应用菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    Task<MenuResult?> RefreshAsync(long tenantId, long xppId);

    /// <summary>
    /// 刷新应用角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<MenuResult?> RefreshAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// 刷新应用角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    Task<MenuResult?> RefreshAsync(long tenantId, long xppId, IRole role);

    /// <summary>
    /// 是否允许访问路由
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <param name="router"></param>
    /// <returns></returns>
    Task<bool> AllowAccessAsync(long tenantId, long xppId, long roleId, string router);
}