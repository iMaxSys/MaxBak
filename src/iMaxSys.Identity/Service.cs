//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Service.cs
//摘要: 服务基础类
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Data;
using iMaxSys.Max.Options;
using iMaxSys.Max.Caching;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Algorithm.Collection;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Repositories;

using DbMenu = iMaxSys.Identity.Data.Entities.Menu;
using DbRole = iMaxSys.Identity.Data.Entities.Role;
using DbMember = iMaxSys.Identity.Data.Entities.Member;
using DbTenant = iMaxSys.Data.Entities.App.Tenant;

namespace iMaxSys.Identity;

/// <summary>
/// 服务基类
/// </summary>
public abstract class Service
{
    protected const string TAG = "id:";
    protected const string TAG_ACCESS = "a:";
    protected const string TAG_MEMBER = "u:";
    protected const string TAG_TENANT = "t:";
    protected const string TAG_ROLE = "r:";
    protected const string TAG_MENU = "m:";

    protected const string TAG_TENANT_MENU = $"{TAG}{TAG_MENU}";
    protected const string TAG_TENANT_ROLE = $"{TAG}{TAG_ROLE}";

    protected readonly IMapper _mapper;
    protected readonly MaxOption _option;
    protected readonly ICache _cache;
    protected readonly IUnitOfWork _unitOfWork;

    public Service(IMapper mapper, IOptions<MaxOption> option, IIdentityCache cache, IUnitOfWork<MaxIdentityContext> unitOfWork)
    {
        _mapper = mapper;
        _cache = cache;
        _option = option.Value;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// 是否存在指定key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected async Task<bool> KeyExistsAsync(string key, bool global = false) => await _cache.KeyExistsAsync(key, global);

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    protected async Task<T?> GetCacheAsync<T>(string key, bool global = false) => await _cache.GetAsync<T>(key, global);

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    protected async Task SetCacheAsync(string key, object value, bool global = false)
    {
        DateTime time = DateTime.Now.AddMinutes(_option.Identity.Expires);
        await _cache.SetAsync(key, value, time, global);
    }

    /// <summary>
    /// 移除缓存
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected async Task RemoveCacheAsync(string key, bool global = false) => await _cache.DeleteAsync(key, global);

    /// <summary>
    /// 刷新权限基础信息(菜单&角色权限)
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IAuthority> RefreshAuthorityAsync(long tenantId = 0)
    {
        IAuthority? authority = null;

        
        string tenantMenuKey = $"{TAG_TENANT_MENU}{tenantId}";      //xppid:id:xppid:m:tenantId
        string tenantRoleKey = $"{TAG_TENANT_ROLE}{tenantId}";      //xppid:id:xppid:r:tenantId

        //删除原数据: 完整菜单&角色信息
        await RemoveCacheAsync(tenantMenuKey);
        await RemoveCacheAsync(tenantRoleKey);

        //获取菜单，无指定租户菜单则取默认菜单
        var menus = await _unitOfWork.GetRepository<DbMenu>().AllAsync(x => x.TenantId == tenantId, null, source => source.Include(y => y.Operations));

        var tmenus = menus.Where(x => x.TenantId == tenantId).ToList();
        if (tmenus.Any())
        {
            menus = tmenus;
        }
        var ms = GetMenu(menus);
        await SetCacheAsync(tenantMenuKey, ms);


        //获取租户的所有角色
        var roles = _unitOfWork.GetRepository<DbRole>().AllAsync(x => x.TenantId == tenantId);
        List<IRole> rs = _mapper.Map<List<IRole>>(roles);
        foreach (var role in rs)
        {
            await SetCacheAsync($"{tenantRoleKey}:{role.Id}", role);
        }

        authority = new Authority
        {
            //数据转换
            Menu = ms,
            Roles = rs,
        };

        return authority;
    }

    /// <summary>
    /// 获取租户完整菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IMenu> GetTenantFullMenuAsync(long tenantId = 0)
    {
        string key = $"{TAG_TENANT_ROLE}{tenantId}";
        bool exists = await KeyExistsAsync(key);

        IMenu menu;
        if (exists)
        {
            //menu = await GetCacheAsync<IMenu>(key);
            var ms = await GetCacheAsync<MenuShadow>(key);
            menu = _mapper.Map<IMenu>(ms);
        }
        else
        {
            IAuthority authority = await RefreshAuthorityAsync(tenantId);
            menu = authority.Menu;
        }

        return menu;
    }

    /// <summary>
    /// 设置匹配菜单s
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="ms"></param>
    /// <param name="os"></param>
    protected void SetMatchMenus(List<IMenu>? menus, long[] ms, long[] os)
    {
        if (ms.Length == 1 && ms[0] == 0)
        {
            return;
        }
        if (menus != null)
        {
            menus.RemoveAll(x => !ms.Contains(x.Id));
            menus.ForEach(x =>
            {
                x.Operations?.RemoveAll(x => !os.Contains(x.Id));
                SetMatchMenus(x.Menus, ms, os);
            });
        }
    }

    /// <summary>
    /// 设置匹配菜单s
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="ms"></param>
    /// <param name="os"></param>
    private void SetMatchMenus(List<IMenu> menus, string ms, string os)
    {
        menus.RemoveAll(x => !ms.Contains(x.Id.ToString()));
        foreach (var menu in menus)
        {
            menu.Operations.RemoveAll(x => !os.Contains(x.Id.ToString()));
            SetMatchMenus(menu.Menus, ms, os);
        }
    }

    /// <summary>
    /// 获取角色key
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    protected string GetRoleKey(long tenantId, long id)
    {
        return $"{TAG}{TAG_ROLE}{tenantId}:{id}";
    }

    /// <summary>
    /// 获取租户key
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    protected string GetTenantKey(long id)
    {
        return $"{TAG_TENANT}{id}";
    }

    /// <summary>
    /// 验证路由是否在权限菜单内
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="router"></param>
    /// <returns></returns>
    protected bool ExistsRouter(IMenu menu, string router)
    {
        if (menu == null)
        {
            return true;
        }
        else
        {
            //本级匹配
            if (menu.Router == router || menu.Operations.Select(x => x.Router).Any(x => x == router))
            {
                return true;
            }

            //子级匹配
            return ExistsRouter(menu.Menus, router);
        }
    }

    /// <summary>
    /// 验证路由是否在权限菜单内
    /// </summary>
    /// <param name="menus"></param>
    /// <param name="router"></param>
    /// <returns></returns>
    private bool ExistsRouter(List<IMenu> menus, string router)
    {
        if (menus?.Count > 0)
        {
            //本级匹配
            if (menus.Any(x => x.Router == router) || menus.Where(x => x.Operations != null).SelectMany(x => x.Operations).Any(x => x.Router == router))
            {
                return true;
            }
            else//子级匹配
            {
                return ExistsRouter(menus.Where(x => x.Menus != null).SelectMany(x => x.Menus)?.ToList(), router);
            }
        }
        else
        {
            return false;
        }
    }

    //==============================菜单操作==============================
    private IMenu GetMenu(IList<DbMenu> stores)
    {
        var store = stores.FirstOrDefault(x => x.Lv == 0);
        IMenu menu = new Menu
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
        return menu;
    }

    private void SetNodes(List<DbMenu> stores, IMenu menu)
    {
        //获取当前节点的下一层子节点
        DbMenu current = stores.FirstOrDefault(x => x.Id == menu.Id);
        var list = stores.Where(x => x.Deep == current.Deep + 1 && x.Lv > current.Lv && x.Rv < current.Rv).OrderBy(x => x.Lv);

        if (list.Count() > 0)
        {
            menu.Menus = new List<IMenu>();
            foreach (var item in list)
            {
                IMenu n = new Menu
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