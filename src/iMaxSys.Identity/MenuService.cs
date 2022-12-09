//----------------------------------------------------------------
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
public class MenuService : TreeService<DbMenu, MenuModel>, IMenuService
{
    //身份缓存
    private readonly IRoleService _roleService;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="identityCache"></param>
    public MenuService(IMapper mapper, IUnitOfWork unitOfWork, IIdentityCache identityCache, IRoleService roleService) : base(mapper, unitOfWork)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// 获取应用色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    public async Task<MenuModel?> GetXppMenuAsync(long tenantId, long xppId)
    {
        return await _unitOfWork.GetCustomRepository<IMenuRepository>().GetAsync(tenantId, xppId);
    }

    /// <summary>
    /// 获取角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<MenuModel?> GetRoleMenuAsync(long tenantId, long xppId, long roleId)
    {
        RoleModel role = await _roleService.GetAsync(tenantId, xppId, roleId);
        return await _unitOfWork.GetCustomRepository<IMenuRepository>().GetAsync(tenantId, xppId, role);
    }

    /// <summary>
    /// 刷新应用菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    public async Task<MenuModel?> RefreshAsync(long tenantId, long xppId)
    {
        return await _unitOfWork.GetCustomRepository<IMenuRepository>().RefreshAsync(tenantId, xppId);
    }

    /// <summary>
    /// 刷新应用角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<MenuModel?> RefreshAsync(long tenantId, long xppId, long roleId)
    {
        RoleModel role = await _roleService.GetAsync(tenantId, xppId, roleId);
        return await _unitOfWork.GetCustomRepository<IMenuRepository>().RefreshAsync(tenantId, xppId, role);
    }
}