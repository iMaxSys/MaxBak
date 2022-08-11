//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MenuRepository.cs
//摘要: 菜单仓储
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Max.Caching;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Collection.Trees;
using iMaxSys.Data.EFCore.Repositories;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Common;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Entities;
using DbMenu = iMaxSys.Identity.Data.Entities.Menu;
using StackExchange.Redis;

namespace iMaxSys.Identity.Data.Repositories;

/// <summary>
/// 菜单仓储
/// </summary>
public class MenuRepository : IdentityRepository<DbMenu>, IMenuRepository
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="context"></param>
    public MenuRepository(IdentityContext context, IMapper mapper, IOptions<MaxOption> option, ICacheFactory cacheFactory) : base(context, mapper, option, cacheFactory)
    {
    }

    #region FindAsync

    /// <summary>
    /// 读取租户应用完整菜单
    /// </summary>
    /// <param name="xppId"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<MenuModel?> GetAsync(long xppId, long tenantId)
    {
        //取缓存
        MenuModel? menu = await Cache.GetAsync<MenuModel>(GetXppMenuKey(tenantId, xppId), GLOBAL);

        //为空则刷新
        if (menu is null)
        {
            menu = await RefreshAsync(tenantId, xppId);
        }

        return menu;
    }

    public async Task<MenuModel?> GetAsync(long xppId, long tenantId, IRole role)
    {
        //取缓存
        MenuModel? menu = await Cache.GetAsync<MenuModel>(GetRoleMenuKey(tenantId, xppId, role.Id), GLOBAL);

        //为空则刷新
        if (menu == null)
        {
            menu = await RefreshAsync(tenantId, xppId, role);
        }

        return menu;
    }

    #endregion

    #region FindAsync

    /// <summary>
    /// find role from database
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<DbMenu> FindAsync(long tenantId, long xppId, long id)
    {
        var dbMenu = await FirstOrDefaultAsync(x => x.TenantId == tenantId && x.XppId == xppId && x.Id == id);

        if (dbMenu is null)
        {
            throw new MaxException(ResultCode.MenuNotExits);
        }
        else
        {
            return dbMenu;
        }
    }

    #endregion

    /// <summary>
    /// 刷新应用菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    public async Task<MenuModel?> RefreshAsync(long tenantId, long xppId)
    {
        var list = await AllAsync(x => x.TenantId == tenantId && x.XppId == xppId);
        var menu = MakeMenu(list);
        await Cache.SetAsync(GetXppMenuKey(tenantId, xppId), menu, new TimeSpan(0, Option.Identity.Expires, 0), GLOBAL);
        return menu;
    }

    /// <summary>
    /// 刷新应用角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<MenuModel?> RefreshAsync(long tenantId, long xppId, IRole role)
    {
        var list = await AllAsync(x => x.TenantId == tenantId && x.XppId == xppId && role.MenuIds.Contains(x.Id));
        var menu = MakeMenu(list, role.MenuIds, role.OperationIds);
        await Cache.SetAsync(GetRoleMenuKey(tenantId, xppId, role.Id), menu, new TimeSpan(0, Option.Identity.Expires, 0), GLOBAL);
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
        var menu = Mapper.Map<MenuModel>(tree);
        return menu;
    }

    private MenuModel MakeMenu(IList<DbMenu> list, long[] ids, long[] operationIds)
    {
        foreach (var item in list)
        {
            if (ids.Contains(item.Id))
            {
                //清除无权限操作
                item.Operations?.ForEach(x =>
                {
                    if (operationIds.Contains(x.Id))
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
        var menu = Mapper.Map<MenuModel>(tree);
        return menu;
    }

    /// <summary>
    /// 获取应用菜单key
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    private string GetXppMenuKey(long tenantId, long xppId) => $"{tagMenu}{xppId}{Cache.Separator}{tenantId}";

    /// <summary>
    /// 获取角色菜单key
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    private string GetRoleMenuKey(long tenantId, long xppId, long roleId) => $"{tagMenu}{xppId}{Cache.Separator}{tenantId}{Cache.Separator}{roleId}";
}