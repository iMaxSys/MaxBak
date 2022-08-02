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

namespace iMaxSys.Identity;

/// <summary>
/// 菜单服务
/// </summary>
public class MenuService : TreeService<DbMenu, MenuModel>, IMenuService
{
    //身份缓存
    private readonly IIdentityCache _identityCache;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="identityCache"></param>
    public MenuService(IMapper mapper, IUnitOfWork unitOfWork, IIdentityCache identityCache) : base(mapper, unitOfWork)
    {
        _identityCache = identityCache;
    }

    /// <summary>
    /// 获取应用色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    public async Task<MenuModel> GetXppMenuAsync(long tenantId, long xppId)
    {
        var menu = await _identityCache.GetXppMenuAsync(tenantId, xppId);

        //为空则刷新
        if (menu == null)
        {
            var list = await _unitOfWork.GetRepository<DbMenu>().AllAsync(x => x.TenantId == tenantId && x.XppId == xppId);
            menu = MakeMenu(list);
            await _identityCache.SetXppMenuAsync(tenantId, xppId, menu);
        }

        return (MenuModel)menu;
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
        var role = await _identityCache.GetRoleAsync(tenantId, xppId, roleId);

        //角色不存在
        if (role is null)
        {
            var repository = _unitOfWork.GetRepository<DbRole>();
            role = await repository.FirstOrDefaultAsync(x => x.TenantId == tenantId && x.XppId == xppId && x.Id == roleId);

            throw new MaxException(ResultCode.RoleNotExists);
        }

        var menu = await _identityCache.GetRoleMenuAsync(tenantId, xppId, roleId);

        IList<DbMenu> list;


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
    }

    /// <summary>
    /// 刷新菜单缓存
    /// </summary>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    private async Task<MenuModel> RefreshXppMenuAsync(long tenantId, long xppId)
    {
        var menu = await _identityCache.GetXppMenuAsync(tenantId, xppId);
        if (menu is null)
        {

        }
        throw new MaxException(ResultCode.HasMember);
    }
}