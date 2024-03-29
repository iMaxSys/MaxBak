﻿// ----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IAuthService.cs
//摘要: 权限接口
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2022-10-15
//----------------------------------------------------------------

using iMaxSys.Max.DependencyInjection;
using iMaxSys.Identity.Models;
using iMaxSys.Max.Identity.Domain;

namespace Kylin.Services.Auth;

/// <summary>
/// 权限接口
/// </summary>
public interface IAuthService : IDependency
{
    /// <summary>
    /// 获取成员
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    Task<IMember> GetMemberAsync(long memberId);

    /// <summary>
    /// 代码登录
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<LoginResult> LoginAsync(CodeLoginRequest request);

    /// <summary>
    /// 密码登录
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<LoginResult> LoginAsync(PasswordLoginRequest request);

    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task LogoutAsync(string token);

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="oldPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    Task ChangePasswordAsync(long memberId, string oldPassword, string newPassword);

    /// <summary>
    /// 获取角色
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<RoleResult> GetRoleAsync(long tenantId, long xppId, long roleId);

    /// <summary>
    /// 获取角色
    /// </summary>
    /// <param name="accessChain"></param>
    /// <returns></returns>
    Task<RoleResult> GetRoleAsync(IAccessChain accessChain);

    /// <summary>
    /// 获取角色菜单
    /// </summary>
    /// <param name="accessChain"></param>
    /// <returns></returns>
    Task<MenuResult?> GetRoleMenuAsync(IAccessChain accessChain);
}