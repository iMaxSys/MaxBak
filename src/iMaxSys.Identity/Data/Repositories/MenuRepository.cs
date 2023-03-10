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
using iMaxSys.Core.Data.Entities;
using System.Collections.Generic;
using System;
using System.Collections;

namespace iMaxSys.Identity.Data.Repositories;

/// <summary>
/// 菜单仓储
/// </summary>
public class TenantMenuRepository : IdentityRepository<TenantMenu>, ITenantMenuRepository
{
    protected const string TAG_IDS = "i";
    protected const string TAG_ROUTER = "r";
    protected const string TAG_XPPMENU = "m";
    protected const string TAG_ROLEMENU = "m";

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="context"></param>
    public TenantMenuRepository(IdentityContext context, IMapper mapper, IOptions<MaxOption> option, ICacheFactory cacheFactory) : base(context, mapper, option, cacheFactory)
    {
    }

    #region FindAsync

    /// <summary>
    /// 读取租户应用完整菜单
    /// </summary>
    /// <param name="xppId"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<MenuResult?> GetAsync(long xppId, long tenantId)
    {
        //取缓存
        MenuResult? menu = await Cache.GetAsync<MenuResult>(GetXppMenuKey(tenantId, xppId), _global);

        //为空则刷新
        if (menu is null)
        {
            menu = await RefreshAsync(tenantId, xppId);
        }

        return menu;
    }

    public async Task<MenuResult?> GetAsync(long tenantId, long xppId, IRole role)
    {
        //取缓存
        MenuResult? menu = await Cache.GetAsync<MenuResult>(GetRoleMenuKey(tenantId, xppId, role.Id), _global);

        //为空则刷新
        if (menu == null)
        {
            menu = await RefreshAsync(tenantId, xppId, role);
        }

        return menu;
    }

    public async Task<MenuIdsResult?> GetIdsAsync(long tenantId, long xppId)
    {
        //取缓存
        MenuIdsResult? ids = await Cache.GetAsync<MenuIdsResult>(GetXppMenuIdsKey(tenantId, xppId), _global);

        //为空则刷新
        if (ids == null)
        {
            ids = await RefreshIdsAsync(tenantId, xppId);
        }

        return ids;
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
        var tenantMenu = await FirstOrDefaultAsync(x => x.TenantId == tenantId && x.XppId == xppId && x.Id == id, null, x => x.Include(y => y.Menu));

        if (tenantMenu is null)
        {
            throw new MaxException(ResultCode.MenuNotExits);
        }
        else
        {
            return tenantMenu.Menu;
        }
    }

    #endregion

    /// <summary>
    /// 刷新应用菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    public async Task<MenuResult?> RefreshAsync(long tenantId, long xppId)
    {
        var list = await AllAsync(x => x.TenantId == tenantId && x.XppId == xppId, null, x => x.Include(y => y.Menu).ThenInclude(x => x.Operations));

        if (list is null)
        {
            return null;
        }

        var menu = MakeMenu(list.Select(x => x.Menu).ToList());
        await Cache.SetAsync(GetXppMenuKey(tenantId, xppId), menu, new TimeSpan(0, Option.Identity.Expires, 0), _global);

        await RefreshIdsAsync(tenantId, xppId, list.Select(x => x.Menu));

        return menu;
    }

    /// <summary>
    /// RefreshIdsAsync
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    public async Task<MenuIdsResult?> RefreshIdsAsync(long tenantId, long xppId)
    {
        var list = await AllAsync(x => x.TenantId == tenantId && x.XppId == xppId, null, x => x.Include(y => y.Menu).ThenInclude(x => x.Operations));

        if (list is null)
        {
            return null;
        }

        return await RefreshIdsAsync(tenantId, xppId, list.Select(x => x.Menu));
    }

    /// <summary>
    /// RefreshIdsAsync
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="menus"></param>
    /// <returns></returns>
    private async Task<MenuIdsResult?> RefreshIdsAsync(long tenantId, long xppId, IEnumerable<DbMenu> menus)
    {
        List<long> ids = new();
        foreach (var menu in menus)
        {
            var os = menu.Operations?.Select(x => x.Id);
            if (os is not null && os.Any())
            {
                ids.AddRange(os.ToArray());
            }
        }

        MenuIdsResult menuIdsResult = new MenuIdsResult();
        menuIdsResult.MenuIds = menus.Select(x => x.Id).ToArray();
        menuIdsResult.OperationIds = ids.ToArray();

        await Cache.SetAsync(GetXppMenuIdsKey(tenantId, xppId), menuIdsResult, new TimeSpan(0, Option.Identity.Expires, 0), _global);

        return menuIdsResult;
    }

    /// <summary>
    /// 刷新应用角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<MenuResult?> RefreshAsync(long tenantId, long xppId, IRole role)
    {
        var list = await AllAsync(x => x.TenantId == tenantId && x.XppId == xppId && role.MenuIds.Contains(x.Id), null, x => x.Include(y => y.Menu).ThenInclude(z => z.Operations));

        if (list is null)
        {
            return null;
        }

        var menus = list.Select(x => x.Menu).ToList();

        MenuResult menu = MakeMenu(menus, role.MenuIds, role.OperationIds);
        await Cache.SetAsync<MenuResult>(GetRoleMenuKey(tenantId, xppId, role.Id), menu, new TimeSpan(0, Option.Identity.Expires, 0), _global);

        var actions = MakeActionArray(menus);
        //await Cache.DeleteAsync(GetRoleRoutersKey(tenantId, xppId, role.Id), _global);
        if (actions is not null && actions.Length > 0)
        {
            await Cache.SetAddAsync(GetRoleRoutersKey(tenantId, xppId, role.Id), actions, new TimeSpan(0, Option.Identity.Expires, 0), _global);
        }

        return menu;
    }

    /// <summary>
    /// 刷新应用角色s菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    public async Task RefreshAsync(long tenantId, long xppId, List<IRole> roles)
    {
        foreach (var item in roles)
        {
            await RefreshAsync(tenantId, xppId, item);
        }
    }

    /// <summary>
    /// 生成树
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private MenuResult MakeMenu(IList<DbMenu> list)
    {
        var tree = list.ToTree((parent, child) => child.ParentId == parent.Id);
        MenuResult menu = Mapper.Map<MenuResult>(tree);
        return menu;
    }

    private MenuResult MakeMenu(List<DbMenu> list, long[] ids, long[] operationIds)
    {
        list.RemoveAll(x => !ids.Contains(x.Id));
        list.ForEach(x =>
        {
            x.Operations?.RemoveAll(y => !operationIds.Contains(y.Id));
        });

        var tree = list.ToTree((parent, child) => child.ParentId == parent.Id);
        var menu = Mapper.Map<MenuResult>(tree);
        return menu;
    }

    /// <summary>
    /// 生成action数组
    /// </summary>
    /// <param name="menus"></param>
    /// <returns></returns>
    private static string[]? MakeActionArray(IList<DbMenu>? menus)
    {
        if (menus is null || menus.Count == 0)
        {
            return null;
        }

        var mRouters = menus.Where(x => !string.IsNullOrWhiteSpace(x.ServerRouter));
        var oRouters = menus.Where(x => x.Operations is not null).SelectMany(x => x.Operations!).Where(x => !string.IsNullOrWhiteSpace(x.ServerRouter));
        int count = mRouters.Count() + oRouters.Count();

        if (count == 0)
        {
            return null;
        }

        int index = 0;
        string[] actions = new string[count];

        foreach (var item in mRouters)
        {
            actions[index] = item.ServerRouter!;
            index++;
        }
        foreach (var item in oRouters)
        {
            actions[index] = item.ServerRouter!;
            index++;
        }
        return actions;
    }

    /// <summary>
    /// 是否允许访问路由
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="role"></param>
    /// <param name="router"></param>
    /// <returns></returns>
    public async Task<bool> AllowAccessAsync(long tenantId, long xppId, IRole role, string router)
    {
        string key = GetRoleRoutersKey(tenantId, xppId, role.Id);
        bool exists = await Cache.KeyExistsAsync(key, true);

        if (!exists)
        {
            await GetAsync(tenantId, xppId);
            await GetAsync(tenantId, xppId, role);
        }

        return await Cache.SetContainsAsync(key, router, true);
    }

    /// <summary>
    /// 获取应用菜单key
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    private string GetXppMenuKey(long tenantId, long xppId) => $"{_tagMenu}{xppId}{Cache.Separator}{tenantId}{Cache.Separator}{TAG_XPPMENU}";

    /// <summary>
    /// 获取应用菜单key
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    private string GetXppMenuIdsKey(long tenantId, long xppId) => $"{_tagMenu}{xppId}{Cache.Separator}{tenantId}{Cache.Separator}{TAG_IDS}";

    /// <summary>
    /// 获取角色菜单key
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    private string GetRoleMenuKey(long tenantId, long xppId, long roleId) => $"{_tagMenu}{xppId}{Cache.Separator}{tenantId}{Cache.Separator}{roleId}{Cache.Separator}{TAG_ROLEMENU}";

    /// <summary>
    /// 获取角色路由key
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    private string GetRoleRoutersKey(long tenantId, long xppId, long roleId) => $"{_tagMenu}{xppId}{Cache.Separator}{tenantId}{Cache.Separator}{roleId}{Cache.Separator}{TAG_ROUTER}";
}