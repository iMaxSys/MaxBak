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
using iMaxSys.Max.Collection;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Extentions;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Common;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Entities;
using iMaxSys.Identity.Data.Repositories;
using DbRole = iMaxSys.Identity.Data.Entities.Role;

namespace iMaxSys.Identity;

/// <summary>
/// 角色服务
/// </summary>
public class RoleService : IRoleService
{
    private readonly IMapper _mapper;
    private readonly MaxOption _option;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="option"></param>
    /// <param name="unitOfWork"></param>
    public RoleService(IMapper mapper, IOptions<MaxOption> option, IUnitOfWork<IdentityContext> unitOfWork)
    {
        _mapper = mapper;
        _option = option.Value;
        _unitOfWork = unitOfWork;
    }

    #region GetAsync

    /// <summary>
    /// get
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<RoleResult> GetAsync(long tenantId, long xppId, long roleId)
    {
        return await _unitOfWork.GetCustomRepository<IRoleRepository>().GetAsync(tenantId, xppId, roleId);
    }

    /// <summary>
    /// get
    /// </summary>
    /// <param name="accessChain"></param>
    /// <returns></returns>
    public async Task<RoleResult> GetAsync(IAccessChain accessChain)
    {
        if (accessChain.Member is null)
        {
            throw new MaxException(ResultCode.UnLogin);
        }

        return await GetAsync(accessChain.Member.TenantId, accessChain.AccessSession.XppId, accessChain.Member.GetCurrentRole(accessChain.AccessSession.XppId).Id);
    }


    #endregion

    #region RefreshAsync

    /// <summary>
    /// RefreshAsync
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<RoleResult> RefreshAsync(long tenantId, long xppId, long roleId)
    {
        return await _unitOfWork.GetCustomRepository<IRoleRepository>().RefreshAsync(tenantId, xppId, roleId);
    }

    #endregion

    #region AddAsync

    /// <summary>
    /// add
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<RoleResult> AddAsync(AddRoleRequest request)
    {
        //重名判断
        await CheckNameAsync(request.TenantId, request.XppId, request);
        var dbRole = SetRole(request.TenantId, request.XppId, request);
        await _unitOfWork.GetCustomRepository<IRoleRepository>().AddAsync(dbRole);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<RoleResult>(dbRole);
    }

    #endregion

    #region UpdateAsync

    /// <summary>
    /// update
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public async Task<RoleResult> UpdateAsync(UpdateRoleRequest request)
    {
        //获取
        var role = await FindAsync(request.TenantId, request.XppId, request.Id);

        //重名判断
        await CheckNameAsync(request.TenantId, request.XppId, request, request.Id);

        //更新
        role = SetRole(request.TenantId, request.XppId, request, role);
        var reporitory = _unitOfWork.GetCustomRepository<IRoleRepository>();
        reporitory.Update(role);
        await _unitOfWork.SaveChangesAsync();

        return await reporitory.RefreshAsync(request.TenantId, request.XppId, role);
    }

    #endregion

    #region RemoveAsync

    /// <summary>
    /// 移除角色
    /// </summary>
    /// <param name="id">RoleId</param>
    /// <returns></returns>
    public async Task RemoveAsync(long tenantId, long xppId, long roleId)
    {
        var hasMember = await _unitOfWork.GetRepository<RoleMember>().AnyAsync(x => x.RoleId == roleId);
        //判断角色之下是否还有成员
        if (hasMember)
        {
            throw new MaxException(ResultCode.RoleNotExists);
        }
        var reporitory = _unitOfWork.GetCustomRepository<IRoleRepository>();
        await reporitory.RemoveAsync(tenantId, xppId, roleId);
        await _unitOfWork.SaveChangesAsync();
    }

    #endregion

    #region SetRole

    /// <summary>
    /// SetRole
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="model"></param>
    /// <param name="dbRole"></param>
    private DbRole SetRole(long tenantId, long xppId, RoleModelRequest model)
    {
        var dbRole = _mapper.Map<DbRole>(model);
        dbRole.TenantId = tenantId;
        dbRole.XppId = xppId;
        return dbRole;
    }

    /// <summary>
    /// SetRole
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="model"></param>
    /// <param name="dbRole"></param>
    private DbRole SetRole(long tenantId, long xppId, RoleModelRequest model, DbRole dbRole)
    {
        _mapper.Map(model, dbRole);
        dbRole.TenantId = tenantId;
        dbRole.XppId = xppId;
        return dbRole;
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
    private async Task<DbRole> FindAsync(long tenantId, long xppId, long roleId)
    {
        var dbRole = await _unitOfWork.GetRepository<DbRole>().FirstOrDefaultAsync(x => x.TenantId == tenantId && x.XppId == xppId && x.Id == roleId);

        if (dbRole is null)
        {
            throw new MaxException(ResultCode.RoleNotExists);
        }
        else
        {
            return dbRole;
        }
    }

    #endregion

    #region CheckName

    /// <summary>
    /// 重名判断
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    private async Task CheckNameAsync(long tenantId, long xppId, RoleModelRequest model, long id = 0)
    {
        Expression<Func<DbRole, bool>> predicate = id == 0 ? x => x.TenantId == tenantId && x.XppId == xppId && x.Name == model.Name : x => x.TenantId == tenantId && x.XppId == xppId && x.Id != id && x.Name == model.Name;

        var exists = await _unitOfWork.GetRepository<DbRole>().AnyAsync(predicate);
        if (exists)
        {
            throw new MaxException(ResultCode.RoleIsExists);
        }
    }

    /// <summary>
    /// 重名判断
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    private async Task CheckNameAsync(long tenantId, long xppId, Expression<Func<DbRole, bool>> predicate)
    {
        var exists = await _unitOfWork.GetRepository<DbRole>().AnyAsync(predicate);
        if (exists)
        {
            throw new MaxException(ResultCode.RoleIsExists);
        }
    }

    /// <summary>
    /// all
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<PagedList<RoleResult>> AllAsync(long tenantId, string? name = null)
    {
        Expression<Func<DbRole, bool>> expression = x => x.TenantId == tenantId && !x.IsDeleted;
        expression = expression.And(x => x.Name.Contains(name!), !name.IsNullOrWhiteSpace());
        var roles = await _unitOfWork.GetRepository<DbRole>().GetPagedListAsync(predicate: expression, orderBy: o => o.OrderBy(x => x.Name));
        return _mapper.Map<PagedList<RoleResult>>(roles);
    }

    #endregion

    /// <summary>
    /// 获取roles
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<PagedList<RoleResult>> GetListAsync(RolesRequest request)
    {
        Expression<Func<DbRole, bool>> expression = x => x.TenantId == request.TenantId && x.XppId == request.XppId && !x.IsDeleted;
        expression = expression.And(x => x.Name.Contains(request.Key!), !request.Key.IsNullOrWhiteSpace());
        var roles = await _unitOfWork.GetRepository<DbRole>().GetPagedListAsync(predicate: expression, orderBy: o => o.OrderBy(x => x.Name));
        return _mapper.Map<PagedList<RoleResult>>(roles);
    }

    /// <summary>
    /// get
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<RoleResult> GetAsync(RoleRequest request)
    {
        return await _unitOfWork.GetCustomRepository<IRoleRepository>().GetAsync(request.TenantId, request.XppId, request.Id);
    }
}
