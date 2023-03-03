// ----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: AuthService.cs
//摘要: 权限服务
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2022-10-15
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Identity;
using iMaxSys.Identity.Models;

namespace Kylin.Services.Auth;

/// <summary>
/// 权限服务
/// </summary>
public class AuthService : IAuthService
{
    private readonly iMaxSys.Identity.IRoleService _roleService;
    private readonly iMaxSys.Identity.IMenuService _menuService;
    private readonly iMaxSys.Identity.IMemberService _memberService;

    public AuthService(IRoleService roleService, IMenuService menuService, iMaxSys.Identity.IMemberService memberService)
    {
        _roleService = roleService;
        _menuService = menuService;
        _memberService = memberService;
    }

    /// <summary>
    /// 获取成员信息
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    public async Task<IMember> GetMemberAsync(long memberId)
    {
       return await _memberService.GetAsync(memberId);
    }

    /// <summary>
    /// 代码登录
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<LoginResult> LoginAsync(CodeLoginModel request)
    {
        IAccessChain accessChain = await _memberService.LoginAsync(request);

        return new LoginResult
        {
            Token = accessChain.AccessSession.Token,
            Expires = accessChain.AccessSession.Expires,
            Member = accessChain.Member
        };
    }

    /// <summary>
    /// 密码登录
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<LoginResult> LoginAsync(PasswordLoginModel request)
    {
        IAccessChain accessChain = await _memberService.LoginAsync(request);

        return new LoginResult
        {
            Token = accessChain.AccessSession.Token,
            Expires = accessChain.AccessSession.Expires,
            Member = accessChain.Member
        };
    }

    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task LogoutAsync(string token)
    {
        await _memberService.LogoutAsync(token);
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="oldPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    public async Task ChangePasswordAsync(long memberId, string oldPassword, string newPassword)
    {
        await _memberService.ChangePasswordAsync(memberId, oldPassword, newPassword);
    }

    /// <summary>
    /// 获取角色
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<RoleResult> GetRoleAsync(long tenantId, long xppId, long roleId)
    {
        return await _roleService.GetAsync(tenantId, xppId, roleId);
    }

    /// <summary>
    /// 获取角色
    /// </summary>
    /// <param name="accessChain"></param>
    /// <returns></returns>
    public async Task<RoleResult> GetRoleAsync(IAccessChain accessChain)
    {
        return await _roleService.GetAsync(accessChain);
    }

    /// <summary>
    /// 获取角色
    /// </summary>
    /// <param name="accessChain"></param>
    /// <returns></returns>
    public async Task<MenuModel?> GetRoleMenuAsync(IAccessChain accessChain)
    {
        return await _menuService.GetRoleMenuAsync(accessChain);
    }
}

