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
using iMaxSys.Max.Collection.Trees;
using iMaxSys.Data;
using iMaxSys.Data.Services;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Common;
using iMaxSys.Identity.Data.Repositories;

using DbMenu = iMaxSys.Identity.Data.Entities.Menu;
using DbRole = iMaxSys.Identity.Data.Entities.Role;
using iMaxSys.Max.Identity.Domain;

namespace iMaxSys.Identity;

/// <summary>
/// 菜单服务
/// </summary>
public class MenuService : TreeService<DbMenu, MenuModel>, IMenuService
{
    //身份缓存
    private readonly IIdentityCache _identityCache;
    private readonly IRoleService _roleService;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="identityCache"></param>
    public MenuService(IMapper mapper, IUnitOfWork unitOfWork, IIdentityCache identityCache, IRoleService roleService) : base(mapper, unitOfWork)
    {
        _identityCache = identityCache;
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
        var menu = await _identityCache.GetXppMenuAsync(tenantId, xppId);

        //为空则刷新
        if (menu == null)
        {
            menu = await RefreshXppMenuAsync(tenantId, xppId);
        }

        return menu is null ? null : (MenuModel)menu;
    }

    /// <summary>
    /// 获取角色菜单 cx  
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<MenuModel?> GetRoleMenuAsync(long tenantId, long xppId, long roleId)
    {
        RoleModel role = await _roleService.GetAsync(tenantId, xppId, roleId);
        var menu = await _identityCache.GetRoleMenuAsync(tenantId, xppId, roleId);
        //为空则刷新
        if (menu == null)
        {
            menu = await RefreshRoleMenuAsync(tenantId, xppId, roleId);
        }

        return menu is null ? null : (MenuModel)menu;
    }

    /// <summary>
    /// 刷新应用菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    public async Task<MenuModel?> RefreshXppMenuAsync(long tenantId, long xppId)
    {
        var list = await _unitOfWork.GetRepository<DbMenu>().AllAsync(x => x.TenantId == tenantId && x.XppId == xppId);
        var menu = MakeMenu(list);
        await _identityCache.SetXppMenuAsync(tenantId, xppId, menu);
        return menu;
    }

    /// <summary>
    /// 刷新应用角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<MenuModel?> RefreshRoleMenuAsync(long tenantId, long xppId, long roleId)
    {
        RoleModel role = await _roleService.GetAsync(tenantId, xppId, roleId);
        var list = await _unitOfWork.GetRepository<DbMenu>().AllAsync(x => x.TenantId == tenantId && x.XppId == xppId && role.MenuIds.Contains(x.Id));
        var menu = MakeMenu(list, role);
        await _identityCache.SetRoleMenuAsync(tenantId, xppId, role.Id, menu);
        return menu;
    }

    /// <summary>
    /// 生成树
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private MenuModel MakeMenu(IList<DbMenu> list)
    {
        var tree = list.ToTree((parent, child) => child.ParentId == parent.Id);
        var menu = _mapper.Map<MenuModel>(tree);
        return menu;
    }

    private MenuModel MakeMenu(IList<DbMenu> list, RoleModel role)
    {
        foreach (var item in list)
        {
            if (role.MenuIds.Contains(item.Id))
            {
                //清除无权限操作
                item.Operations?.ForEach(x =>
                {
                    if (role.OperationIds.Contains(x.Id))
                    {
                        item.Operations?.Remove(x);
                    }
                });
            }
            else
            {
                //清除无权限菜单
                list.Remove(item);
            }
        }

        var tree = list.ToTree((parent, child) => child.ParentId == parent.Id);
        var menu = _mapper.Map<MenuModel>(tree);
        return menu;
    }

    /// <summary>
    /// 生成菜单
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    //private static MenuModel? MakeMenu(IMenu menu, RoleModel role)
    //{
    //    if (role.MenuIds.Contains(menu.Id))
    //    {
    //        if (menu.Children is not null)
    //        {
    //            foreach (IMenu item in menu.Children)
    //            {
    //                if (!role.MenuIds.Contains(item.Id))
    //                {
    //                    menu.Children.Remove(item);
    //                }

    //                item.Operations?.ForEach(x =>
    //                {
    //                    if (!role.OperationIds.Contains(x.Id))
    //                    {
    //                        item.Operations?.Remove(x);
    //                    }
    //                });
    //            }
    //        }
    //        return (MenuModel)menu;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}

    /// <summary>
    /// 刷新菜单缓存
    /// </summary>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    //private async Task<MenuModel> RefreshXppMenuAsync(long tenantId, long xppId)
    //{
    //    var menu = await _identityCache.GetXppMenuAsync(tenantId, xppId);
    //    if (menu is null)
    //    {

    //    }
    //    throw new MaxException(ResultCode.HasMember);
    //}
}