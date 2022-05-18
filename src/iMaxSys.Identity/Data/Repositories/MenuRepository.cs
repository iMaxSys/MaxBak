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
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.EFCore.Repositories;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Entities;
using DbMenu = iMaxSys.Identity.Data.Entities.Menu;

namespace iMaxSys.Identity.Data.Repositories;

/*
/// <summary>
/// 菜单仓储
/// </summary>
public class MenuRepository : EfRepository<DbMenu>, IMenuRepository
{
    const string TAG = "id:";
    const string TAG_MENU = "m:";
    const string TAG_TENANT = "t:";
    const string TAG_TENANT_MENU = $"{TAG}{TAG_MENU}";

    private readonly IMapper _mapper;
    private readonly MaxOption _option;
    private readonly IIdentityCache _identityCache;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityCache"></param>
    public MenuRepository(IMapper mapper, IOptions<MaxOption> option, MaxIdentityContext context, IIdentityCache identityCache) : base(context)
    {
        _mapper = mapper;
        _option = option.Value;
        _identityCache = identityCache;
    }

    public async Task<IMenu?> ReadAsync(long xppId, long tenantId, IRole role)
    {
        var menu = await ReadFullAsync(xppId, tenantId);
        SetMatchMenus(menu, role.MenuIds, role.OperationIds);
        return menu;
    }

    /// <summary>
    /// 读取租户应用完整菜单
    /// </summary>
    /// <param name="xppId"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IMenu?> ReadFullAsync(long xppId, long tenantId)
    {
        //读取租户全菜单缓存, 如无则取库刷新缓存
        string key = $"{TAG_TENANT_MENU}{xppId}{tenantId}";

        IMenu? menu = await _identityCache.GetAsync<Max.Identity.Domain.Menu>(key, true);

        if (menu != null)
        {
            return menu;
        }
        else
        {
            return await RefreshAsync(xppId, tenantId);
        }
    }

    /// <summary>
    /// 刷新租户应用完整菜单缓存
    /// </summary>
    /// <param name="xppId"></param>
    /// <param name="tenantId"></param>
    /// <param name="expires"></param>
    /// <returns></returns>
    public async Task<IMenu?> RefreshAsync(long xppId, long tenantId, DateTime? expires = null)
    {
        IMenu? menu = null;
        var menus = await AllAsync(x => x.XppId == xppId && x.TenantId == tenantId);
        if (menus?.Count > 0)
        {
            menu = GetMenu(menus);
            //设置租户全菜单缓存
            await _identityCache.SetAsync($"{TAG_TENANT_MENU}{xppId}{tenantId}", expires ?? DateTime.Now.AddMinutes(_option.Identity.Refresh), true);
        }
        return menu;
    }

    #region 菜单操作

    //==============================菜单操作==============================

    /// <summary>
    /// 设置匹配菜单s
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="ms"></param>
    /// <param name="os"></param>
    protected void SetMatchMenus(IMenu? menu, long[]? ms, long[]? os)
    {
        if (menu != null && menu.Menus != null && menu.Menus.Any())
        {
            menu.Menus.RemoveAll(x => ms != null && !ms.Contains(0) && !ms.Contains(x.Id));
            menu.Menus.ForEach(x =>
            {
                x.Operations?.RemoveAll(x => os != null && !os.Contains(0) && !os.Contains(x.Id));
                SetMatchMenus(x, ms, os);
            });
        }
    }

    private IMenu? GetMenu(IList<DbMenu> stores)
    {
        IMenu? menu = null;
        var store = stores.FirstOrDefault(x => x.Lv == 0);
        if (store != null)
        {
            menu = new Max.Identity.Domain.Menu
            {
                Id = store.Id,
                Code = store.Code,
                Name = store.Name,
                Description = store.Description,
                Icon = store.Icon,
                Style = store.Style,
                Router = store.Router,
                Status = store.Status
            };
            SetNodes(stores, menu);
        }

        return menu;
    }

    private void SetNodes(IList<DbMenu> stores, IMenu menu)
    {
        //获取当前节点的下一层子节点
        DbMenu? current = stores.FirstOrDefault(x => x.Id == menu.Id);
        if (current != null)
        {
            var list = stores.Where(x => x.Deep == current.Deep + 1 && x.Lv > current.Lv && x.Rv < current.Rv).OrderBy(x => x.Lv);

            if (list?.Count() > 0)
            {
                menu.Menus = new List<IMenu>();
                foreach (var item in list)
                {
                    IMenu n = new Max.Identity.Domain.Menu
                    {
                        Id = item.Id,
                        Code = item.Code,
                        Name = item.Name,
                        Description = item.Description,
                        Icon = item.Icon,
                        Style = item.Style,
                        Router = item.Router,
                        Status = item.Status,
                        Operations = _mapper.Map<List<IOperation>>(item.Operations)
                    };
                    SetNodes(stores, n);
                    menu.Menus.Add(n);
                }
            }
        }
    }

    #endregion
}
*/