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
    Task<bool> HasMember(long tenantId, long memberId);

    /// <summary>
    /// 设置member
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="member"></param>
    /// <returns></returns>
    Task SetMember(long tenantId, IMember member);

    /// <summary>
    /// 租户应用菜单是否存在
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    Task<bool> HasTenantMenu(long tenantId, long xppId);

    /// <summary>
    /// 租户应用角色菜单是否存在
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<bool> HasRoleMenu(long tenantId, long xppId, long roleId);

    /// <summary>
    /// 角色是否存在
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<bool> HasRole(long tenantId, long xppId, long roleId);
}

