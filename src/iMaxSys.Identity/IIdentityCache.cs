﻿//----------------------------------------------------------------
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

using iMaxSys.Max.DependencyInjection;
using iMaxSys.Max.Identity.Domain;

namespace iMaxSys.Identity;

/// <summary>
/// 身份缓存接口
/// </summary>
public interface IIdentityCache : ISingleton
{
    /// <summary>
    /// 是否存在member
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="memberId"></param>
    /// <returns></returns>
    Task<bool> HasMemberAsync(long tenantId, long memberId);

    /// <summary>
    /// 获取member
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="memberId"></param>
    /// <returns></returns>
    Task<IMember> GetMemberAsync(long tenantId, long memberId);

    /// <summary>
    /// 设置member
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="member"></param>
    /// <returns></returns>
    Task SetMemberAsync(long tenantId, IMember member);

    /// <summary>
    /// 租户应用菜单是否存在
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    Task<bool> HasTenantMenuAsync(long tenantId, long xppId);

    /// <summary>
    /// 获取租户应用菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    Task<IMenu> GetTenantMenuAsync(long tenantId, long xppId);

    /// <summary>
    /// 设置租户应用菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="menu"></param>
    /// <returns></returns>
    Task SetTenantMenuAsync(long tenantId, long xppId, IMenu menu);

    /// <summary>
    /// 租户应用角色菜单是否存在
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<bool> HasRoleMenuAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// 获取租户应用角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<IMenu> GetRoleMenuAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// 设置租户应用角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <param name="menu"></param>
    /// <returns></returns>
    Task SetRoleMenuAsync(long tenantId, long xppId, long roleId, IMenu menu);

    /// <summary>
    /// 角色是否存在
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<bool> HasRoleAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// 获取角色
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<IRole> GetRoleAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// 设置角色
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    Task SetRoleAsync(long tenantId, long xppId, IRole role);
}