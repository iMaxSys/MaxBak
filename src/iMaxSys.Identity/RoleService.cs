//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IRoleService.cs
//摘要: 角色服务
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Repositories;

using DbRole = iMaxSys.Identity.Data.Entities.Role;
using DbMember = iMaxSys.Identity.Data.Entities.Member;
using iMaxSys.Identity.Common;
using iMaxSys.Max.Extentions;
using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Identity;

/// <summary>
/// 角色服务
/// </summary>
public class RoleService : ServiceBase, IRoleService
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="option"></param>
    /// <param name="unitOfWork"></param>
    public RoleService(IMapper mapper, IOptions<MaxOption> option, IUnitOfWork<MaxIdentityContext> unitOfWork) : base(mapper, option, unitOfWork)
    {
    }

    #region GetAsync

    /// <summary>
    /// Get role
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IRole?> GetAsync(long id, long tenantId = 0)
    {
        var role = await UnitOfWork.GetCustomRepository<IRoleRepository>().FirstOrDefaultAsync(x => x.Id == id && x.TenantId == tenantId);
        if (role != null)
        {
            return Mapper.Map<IRole>(role);
        }
        else
        {
            throw new MaxException(IdentityResultEnum.RoleNotExists);
        }
    }

    #endregion

    #region AddAsync

    /// <summary>
    /// 新增角色
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<IRole> AddAsync(RoleModel model)
    {
        DbRole dbRole = new();
        SetRole(model, dbRole);
        dbRole.TenantId = model.TenantId;
        await UnitOfWork.GetCustomRepository<IRoleRepository>().AddAsync(dbRole);
        await UnitOfWork.SaveChangesAsync();
        IRole role = Mapper.Map<IRole>(dbRole);
        await RefreshAsync(role);
        return role;
    }

    #endregion

    #region UpdateAsync

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public async Task<IRole> UpdateAsync(RoleModel model)
    {
        //成员id判空
        if (!model.Id.HasValue)
        {
            throw new MaxException(IdentityResultEnum.RoleIdCantNull);
        }

        IRoleRepository respoitory = UnitOfWork.GetCustomRepository<IRoleRepository>();
        var role = await respoitory.FindAsync(model.Id);
        if (role == null)
        {
            throw new MaxException(IdentityResultEnum.RoleNotExists);
        }

        SetRole(model, role);
        respoitory.Update(role);
        await UnitOfWork.SaveChangesAsync();

        var result = Mapper.Map<IRole>(role);
        await RefreshAsync(result);

        return result;
    }

    #endregion

    #region RemoveAsync

    /// <summary>
    /// 移除角色
    /// </summary>
    /// <param name="id">RoleId</param>
    /// <returns></returns>
    public async Task RemoveAsync(long id)
    {
        await UnitOfWork.GetCustomRepository<IRoleRepository>().RemoveAsync(id);
        await UnitOfWork.SaveChangesAsync();
    }

    #endregion

    #region GetMenuAsync

    /// <summary>
    /// 获取角色菜单
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IMenu?> GetMenuAsync(long roleId, long tenantId = 0)
    {
        if (roleId == 0)
        {
            throw new MaxException(IdentityResultEnum.RoleIdCantNull);
        }

        DbRole? dbRole = await UnitOfWork.GetCustomRepository<IRoleRepository>().FirstOrDefaultAsync(x => x.Id == roleId && x.TenantId == tenantId);

        if (dbRole == null)
        {
            throw new MaxException(IdentityResultEnum.RoleNotExists);
        }

        IRole role = Mapper.Map<IRole>(dbRole);

        return await GetMenuAsync(role);
    }

    #endregion

    /// <summary>
    /// 获取角色菜单
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public async Task<IMenu?> GetMenuAsync(IRole role)
    {
        //获取完整菜单for tenant && xpp
        var menu = await GetFullMenuAsync(role.TenantId);
        SetMatchMenus(menu.Menus, role.MenuIds, role.OperationIds);
        return menu;
    }

    /// <summary>
    /// 获取完整菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IMenu> GetFullMenuAsync(long tenantId = 0, long xpp = 0)
    {
        return await GetTenantFullMenuAsync(tenantId);
    }

    /// <summary>
    /// 获取租户完整菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IMenu> GetTenantFullMenuAsync(long tenantId = 0)
    {
        string key = $"{TAG}{TAG_MENU}{tenantId}";
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
    /// 移除角色
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task RemoveRoleAsync(long id, long tenantId = 0)
    {
        //如角色下还存在用户,则不可移除该角色
        bool has = await _unitOfWork.GetRepo<DbMember>().ExistAsync(x => x.TenantId == tenantId && x.RoleId == id);
        if (has)
        {
            throw new MaxException(ResultCode.RoleHasMember);
        }

        ISpecification<DbRole> spec = new Specification<DbRole>(x => x.TenantId == tenantId && x.Id == id);
        var role = await _unitOfWork.GetRepo<DbRole>().FirstOrDefaultAsync(spec);
        await _unitOfWork.GetRepo<DbRole>().UpdateAsync(role);
        await _unitOfWork.SaveChangesAsync();
        await RemoveCacheAsync(GetRoleKey(tenantId, id));
    }

    

    /// <summary>
    /// 设置DbRole
    /// </summary>
    /// <param name="model"></param>
    /// <param name="dbTenant"></param>
    private static void SetRole(RoleModel model, DbRole dbRole)
    {
        dbRole.TenantId = model.TenantId;
        dbRole.Name = model.Name;
        model.Descripton?.IfNotNull(x => dbRole.Descripton = x);
        model.Icon?.IfNotNull(x => dbRole.Icon = x);
        model.Style?.IfNotNull(x => dbRole.Style = x);
        model.Menus?.IfNotNull(x => dbRole.MenuIds = x);
        model.Operations?.IfNotNull(x => dbRole.OperationIds = x);
        dbRole.Start = model.Start;
        dbRole.End = model.End;
        dbRole.Status = model.Status;
    }

    #region RefreshAsync

    /// <summary>
    /// 刷新role缓存
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task RefreshAsync(long roleId)
    {
        IRole? role = await GetAsync(roleId);
        if (role != null)
        {
            await RefreshAsync(role);
        }
    }

    /// <summary>
    /// 刷新role缓存
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public async Task RefreshAsync(IRole role)
    {
        await UnitOfWork.GetCustomRepository<IRoleRepository>().RefreshAsync(role, DateTime.Now.AddMinutes(Option.Identity.Expires));
    }

    /// <summary>
    /// 刷新租户所有role缓存
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public async Task RefreshAllAsync(long tenantId = 0)
    {
        //此处暂时采用循环刷新，日后调整为一次刷新
        var list = await UnitOfWork.GetCustomRepository<IRoleRepository>().AllAsync(x => x.TenantId == tenantId);
        DateTime now = DateTime.Now;
        foreach (var item in list)
        {
            IRole role = Mapper.Map<IRole>(item);
            await UnitOfWork.GetCustomRepository<IRoleRepository>().RefreshAsync(role, now.AddMinutes(Option.Identity.Expires));
        }
    }

    #endregion
}