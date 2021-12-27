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

using DbRole = iMaxSys.Identity.Data.Entities.Role;
using DbMember = iMaxSys.Identity.Data.Entities.Member;

namespace iMaxSys.Identity;

/// <summary>
/// 角色服务
/// </summary>
public class RoleService : Service, IRoleService
{
    public RoleService(IMapper mapper, IOptions<MaxOption> option, IIdentityCache cache, IUnitOfWork<MaxIdentityContext> unitOfWork) : base(mapper, option, cache, unitOfWork)
    {
    }

    /// <summary>
    /// 新增角色
    /// </summary>
    /// <param name="model"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IRole> AddRoleAsync(RoleModel model, long tenantId = 0)
    {
        DbRole dbRole = new();
        SetDbRole(model, dbRole);
        dbRole.TenantId = tenantId;
        //await _unitOfWork.GetRepository<DbRole>().InsertAsync(dbRole);
        await _unitOfWork.GetRepository<DbRole>().AddAsync(dbRole);
        await _unitOfWork.SaveChangesAsync();
        IRole role = _mapper.Map<IRole>(dbRole);
        await SetCacheAsync(GetRoleKey(role.TenantId, role.Id), role);
        return role;
    }

    /// <summary>
    /// 获取角色
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IRole?> GetRoleAsync(long id, long tenantId = 0)
    {
        if (id == 0)
        {
            return null;
        }

        string key = GetRoleKey(tenantId, id);
        IRole? role = await GetCacheAsync<IRole>(key);

        if (role == null)
        {
            DbRole? dbRole = await _unitOfWork.GetRepository<DbRole>().FirstOrDefaultAsync(x => x.TenantId == tenantId && x.Id == id);
            role = _mapper.Map<IRole>(dbRole);
            await SetCacheAsync(key, role);
        }

        //role.Menu = await GetRoleMenuAsync(id);
        return role;
    }

    /// <summary>
    /// 获取角色菜单
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IMenu> GetRoleMenuAsync(long id, long tenantId = 0)
    {
        if (id == 0)
        {
            return null;
        }

        //DbRole dbRole = await _unitOfWork.GetRepository<DbRole>().GetFirstOrDefaultAsync(predicate: x => x.TenantId == tenantId && x.Id == id);
        ISpecification<DbRole> spec = new Specification<DbRole>(x => x.TenantId == tenantId && x.Id == id);
        DbRole dbRole = await _unitOfWork.GetRepo<DbRole>().FirstOrDefaultAsync(spec);
        if (dbRole == null)
        {
            return null;
        }
        IRole role = _mapper.Map<IRole>(dbRole);
        return await GetRoleMenuAsync(role);
    }

    /// <summary>
    /// 获取角色菜单
    /// </summary>
    /// <param name="role"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IMenu> GetRoleMenuAsync(IRole role, long tenantId = 0)
    {
        if (role == null)
        {
            return null;
        }
        var menu = await GetTenantFullMenuAsync(role.TenantId);
        SetMatchMenus(menu.Menus, role.MenuIds, role.OperationIds);
        return menu;
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
            throw new MaxException(ResultEnum.RoleHasMember);
        }

        ISpecification<DbRole> spec = new Specification<DbRole>(x => x.TenantId == tenantId && x.Id == id);
        var role = await _unitOfWork.GetRepo<DbRole>().FirstOrDefaultAsync(spec);
        await _unitOfWork.GetRepo<DbRole>().UpdateAsync(role);
        await _unitOfWork.SaveChangesAsync();
        await RemoveCacheAsync(GetRoleKey(tenantId, id));
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="model"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IRole> UpdateRoleAsync(RoleModel model, long tenantId = 0)
    {
        ISpecification<DbRole> spec = new Specification<DbRole>(x => x.TenantId == tenantId && x.Id == model.Id);
        DbRole dbRole = await _unitOfWork.GetRepo<DbRole>().FirstOrDefaultAsync(spec);
        SetDbRole(model, dbRole);
        //_unitOfWork.GetRepository<DbRole>().Update(dbRole);
        await _unitOfWork.SaveChangesAsync();
        IRole role = _mapper.Map<IRole>(dbRole);
        await SetCacheAsync(GetRoleKey(role.TenantId, role.Id), role);
        return role;
    }

    /// <summary>
    /// 设置DbRole
    /// </summary>
    /// <param name="model"></param>
    /// <param name="dbTenant"></param>
    private void SetDbRole(RoleModel model, DbRole dbRole)
    {
        dbRole.TenantId = model.TenantId ?? dbRole.TenantId;
        dbRole.Name = model.Name ?? dbRole.Name;
        dbRole.Descripton = model.Descripton ?? dbRole.Descripton;
        dbRole.Icon = model.Icon ?? dbRole.Icon;
        dbRole.Style = model.Style ?? dbRole.Style;
        dbRole.MenuIds = model.Menus ?? dbRole.MenuIds;
        dbRole.OperationIds = model.Operations ?? dbRole.OperationIds;
        dbRole.Start = model.Start ?? dbRole.Start;
        dbRole.End = model.End ?? dbRole.End;
        dbRole.Status = model.Status ?? dbRole.Status;
    }