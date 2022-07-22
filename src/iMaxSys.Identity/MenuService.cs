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
using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Collection.Trees;
using iMaxSys.Data;
using iMaxSys.Data.Services;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Common;
using iMaxSys.Identity.Data.Entities;

using DbMenu = iMaxSys.Identity.Data.Entities.Menu;
using DbRole = iMaxSys.Identity.Data.Entities.Role;

namespace iMaxSys.Identity;

/// <summary>
/// 菜单服务
/// </summary>
public class MenuService : TreeService<DbMenu, MenuModel>, IMenuService
{
    //身份缓存
    //private readonly IdentityCache _identityCache;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="unitOfWork"></param>
    public MenuService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        //_identityCache = identityCache;
    }
    /// <summary>
    /// 获取应用色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    public async Task<MenuModel> GetXppMenuAsync(long tenantId, long xppId)
    {
        var list = await _unitOfWork.GetRepository<DbMenu>().AllAsync(x => x.TenantId == tenantId && x.XppId == xppId);
        return MakeMenu(list);
    }

    /// <summary>
    /// 获取角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<MenuModel> GetRoleMenuAsync(long tenantId, long xppId, long roleId)
    {
        IList<DbMenu> list;
        var repository = _unitOfWork.GetRepository<DbRole>();
        var role = await repository.FirstOrDefaultAsync(x => x.TenantId == tenantId && x.XppId == xppId && x.Id == roleId);

        //角色不存在
        if (role is null)
        {
            throw new MaxException(ResultCode.RoleNotExists);
        }

        long[] menuIds = role.MenuIds?.ToLongArray() ?? new long[] { -1 };
        long[] operationIds = role.OperationIds?.ToLongArray() ?? new long[] { -1 };

        //0为全部权限
        if (menuIds.Contains(0) == true)
        {
            list = await _unitOfWork.GetRepository<DbMenu>().AllAsync(x => x.TenantId == tenantId && x.XppId == xppId, include: source => source.Include(y => y.Operations));
        }
        else
        {
            list = await _unitOfWork.GetRepository<DbMenu>().AllAsync(x => x.TenantId == tenantId && x.XppId == xppId && menuIds.Contains(x.Id), include: source => source.Include(e => e.Operations!.Where(p => operationIds.Contains(p.Id))));
        }

        return MakeMenu(list);
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
        //return new MenuModel();
    }
}

/*
/// <summary>
/// 菜单服务
/// </summary>
public class MenuService : ServiceBase, IMenuService
{
    public MenuService(IMapper mapper, IOptions<MaxOption> option, IUnitOfWork<MaxIdentityContext> unitOfWork, IGenericCache genericCache) : base(mapper, option, unitOfWork)
    {
        _mapper = mapper;
        _option = option;
        _genericCache = genericCache;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// 读取菜单
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
        public async Task<IMenu?> ReadAsync(long roleId)
    {

    }

    /// <summary>
    /// 获取完整菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IMenu?> GetFullAsync(long tenantId = 0, long xppId)
    {
        var role = await UnitOfWork.GetCustomRepository<IMenuRepository>().FirstOrDefaultAsync(x => x.TenantId == tenantId && );
        if (role != null)
        {
            return Mapper.Map<IRole>(role);
        }
        else
        {
            throw new MaxException(IdentityResultEnum.RoleNotExists);
        }
    }

    /// <summary>
    /// 新增菜单项目
    /// </summary>
    /// <param name="menuModel"></param>
    /// <param name="targetId"></param>
    /// <param name="isSub"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IMenu> AddMenuItemAsync(MenuModel menuModel, long targetId, bool isSub, long tenantId = 0)
    {
        DbMenu dbMenu = new DbMenu();
        SetDbMenu(menuModel, dbMenu);
        dbMenu.TenantId = targetId;
        await _unitOfWork.GetRepo<DbMenu>().AddAsync(dbMenu);
        await _unitOfWork.SaveChangesAsync();
        await RefreshAuthorityAsync(dbMenu.TenantId);
        return _mapper.Map<IMenu>(dbMenu);
    }

    /// <summary>
    /// 获取完整菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IMenu> GetFullMenuAsync(long tenantId = 0)
    {
        return await GetTenantFullMenuAsync(tenantId);
    }

    /// <summary>
    /// 获取菜单项
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IMenu> GetMenuItemAsync(long id, long tenantId = 0)
    {
        ISpecification<DbMenu> spec = new Specification<DbMenu>(x => x.TenantId == tenantId && x.Id == id);
        DbMenu dbMenu = await _unitOfWork.GetRepo<DbMenu>().FirstOrDefaultAsync(spec);
        return _mapper.Map<IMenu>(dbMenu);
    }

    /// <summary>
    /// 移动菜单项(待完成)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="targetId"></param>
    /// <param name="isSub"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public Task MoveMenuItemAsync(long id, long targetId, bool isSub, long tenantId = 0)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// 移除菜单项(补位未完成)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task RemoveMenuItemAsync(long id, long tenantId = 0)
    {
        ISpecification<DbMenu> spec = new Specification<DbMenu>(x => x.TenantId == tenantId && x.Id == id);
        DbMenu dbMenu = await _unitOfWork.GetRepo<DbMenu>().FirstOrDefaultAsync(spec);

        //如存在子菜单则不可删除
        bool hasSub = await _unitOfWork.GetRepo<DbMenu>().ExistAsync(x => x.TenantId == tenantId && x.Lv > dbMenu.Lv && x.Rv < dbMenu.Rv);
        if (hasSub)
        {
            throw new MaxException(ResultEnum.MenuHasSub);
        }

        //待补充删除补位逻辑
        var menu = await _unitOfWork.GetRepo<DbMenu>().GetAsync(id);
        await _unitOfWork.GetRepo<DbMenu>().DeleteAsync(menu);
        await _unitOfWork.SaveChangesAsync();
        await RefreshAuthorityAsync(dbMenu.TenantId);
    }

    /// <summary>
    /// 更新菜单项
    /// </summary>
    /// <param name="menuModel"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IMenu> UpdateMenuItemAsync(MenuModel menuModel, long tenantId = 0)
    {
        ISpecification<DbMenu> spec = new Specification<DbMenu>(x => x.TenantId == tenantId && x.Id == menuModel.Id);
        DbMenu dbMenu = await _unitOfWork.GetRepo<DbMenu>().FirstOrDefaultAsync(spec);
        SetDbMenu(menuModel, dbMenu);
        await _unitOfWork.GetRepo<DbMenu>().UpdateAsync(dbMenu);
        await _unitOfWork.SaveChangesAsync();
        await RefreshAuthorityAsync(dbMenu.TenantId);
        return _mapper.Map<IMenu>(dbMenu);
    }

    /// <summary>
    /// 设置DbMenu
    /// </summary>
    /// <param name="model"></param>
    /// <param name="dbRole"></param>
    private void SetDbMenu(MenuModel model, DbMenu dbMenu)
    {
        dbMenu.Name = model.Name ?? dbMenu.Name;
        dbMenu.Description = model.Description ?? dbMenu.Description;
        dbMenu.Icon = model.Icon ?? dbMenu.Icon;
        dbMenu.Style = model.Style ?? dbMenu.Style;
        dbMenu.Router = model.Router ?? dbMenu.Router;
        dbMenu.Code = model.Code ?? dbMenu.Code;
        dbMenu.Status = model.Status ?? dbMenu.Status;
    }

    /// <summary>
    /// 拷贝Menu
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    private void CopyMenu(IMenu source, IMenu target)
    {
        target.Code = source.Code;
        target.Description = source.Description;
        target.Icon = source.Icon;
        target.Id = source.Id;
        target.Name = source.Name;
        target.Router = source.Router;
        target.Status = source.Status;
        target.Style = source.Router;
        target.Router = source.Style;
        target.Operations = source.Operations;
        target.Menus = source.Menus;
    }
}
*/