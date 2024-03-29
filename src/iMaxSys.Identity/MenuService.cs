﻿//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MenuService.cs
//摘要: 菜单服务
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Extentions;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Collection.Trees;

using iMaxSys.Data;
using iMaxSys.Core.Services;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Common;
using iMaxSys.Identity.Data.Repositories;

using DbMenu = iMaxSys.Identity.Data.Entities.Menu;

namespace iMaxSys.Identity;

/// <summary>
/// 菜单服务
/// </summary>
public class MenuService : TreeService<DbMenu, MenuResult>, IMenuService
{
    //身份缓存
    private readonly IRoleService _roleService;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="unitOfWork"></param>

    public MenuService(IMapper mapper, IUnitOfWork unitOfWork, IIdentityCache identityCache, IRoleService roleService) : base(mapper, unitOfWork)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// 获取应用角色菜单Ids
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    public async Task<MenuIdsResult?> GetXppMenuIdsAsync(long tenantId, long xppId)
    {
        return await _unitOfWork.GetCustomRepository<ITenantMenuRepository>().GetIdsAsync(tenantId, xppId);
    }

    /// <summary>
    /// 获取应用色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    public async Task<MenuResult?> GetXppMenuAsync(long tenantId, long xppId)
    {
        return await _unitOfWork.GetCustomRepository<ITenantMenuRepository>().GetAsync(tenantId, xppId);
    }

    /// <summary>
    /// 获取角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<MenuResult?> GetRoleMenuAsync(long tenantId, long xppId, long roleId)
    {
        RoleResult role = await _roleService.GetAsync(tenantId, xppId, roleId);
        return await _unitOfWork.GetCustomRepository<ITenantMenuRepository>().GetAsync(tenantId, xppId, role);
    }

    /// <summary>
    /// 获取角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<MenuResult?> GetRoleMenuAsync(IAccessChain accessChain)
    {
        if (accessChain.Member is null)
        {
            throw new MaxException(ResultCode.UnLogin);
        }

        return await GetRoleMenuAsync(accessChain.Member.TenantId, accessChain.AccessSession.XppId, accessChain.Member.GetCurrentRole(accessChain.AccessSession.XppId).Id);
    }

    /// <summary>
    /// 刷新应用菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    public async Task<MenuResult?> RefreshAsync(long tenantId, long xppId)
    {
        return await _unitOfWork.GetCustomRepository<ITenantMenuRepository>().RefreshAsync(tenantId, xppId);
    }

    /// <summary>
    /// 刷新应用角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<MenuResult?> RefreshAsync(long tenantId, long xppId, long roleId)
    {
        RoleResult role = await _roleService.GetAsync(tenantId, xppId, roleId);
        return await RefreshAsync(tenantId, xppId, role);
    }

    /// <summary>
    /// 刷新应用角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public async Task<MenuResult?> RefreshAsync(long tenantId, long xppId, IRole role)
    {
        return await _unitOfWork.GetCustomRepository<ITenantMenuRepository>().RefreshAsync(tenantId, xppId, role);
    }

    /// <summary>
    /// 是否允许访问路由 
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <param name="router"></param>
    /// <returns></returns>
    public async Task<bool> AllowAccessAsync(long tenantId, long xppId, long roleId, string router)
    {
        var role = await _roleService.GetAsync(tenantId, xppId, roleId);
        return await _unitOfWork.GetCustomRepository<ITenantMenuRepository>().AllowAccessAsync(tenantId, xppId, role, router);
    }
}