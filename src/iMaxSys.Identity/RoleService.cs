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
using System;

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
    public RoleService(IMapper mapper, IOptions<MaxOption> option, IUnitOfWork unitOfWork)
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
    public async Task<RoleModel> GetAsync(long tenantId, long xppId, long roleId)
    {
        return await _unitOfWork.GetCustomRepository<IRoleRepository>().GetAsync(tenantId, xppId, roleId);
    }

    /// <summary>
    /// get
    /// </summary>
    /// <param name="accessChain"></param>
    /// <returns></returns>
    public async Task<RoleModel> GetAsync(IAccessChain accessChain)
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
    public async Task<RoleModel> RefreshAsync(long tenantId, long xppId, long roleId)
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
    public async Task<RoleModel> AddAsync(long tenantId, long xppId, RoleModel model)
    {
        //重名判断
        await CheckNameAsync(tenantId, xppId, model);
        var dbRole = SetRole(tenantId, xppId, model);
        await _unitOfWork.GetCustomRepository<IRoleRepository>().AddAsync(dbRole);
        await _unitOfWork.SaveChangesAsync();
        model.Id = dbRole.Id;
        return model;
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
    public async Task<RoleModel> UpdateAsync(long tenantId, long xppId, RoleModel model)
    {
        //获取
        var role = await FindAsync(tenantId, xppId, model.Id);

        //重名判断
        await CheckNameAsync(tenantId, xppId, model);

        //更新
        role = SetRole(tenantId, xppId, model, role);
        var reporitory = _unitOfWork.GetCustomRepository<IRoleRepository>();
        reporitory.Update(role);
        await _unitOfWork.SaveChangesAsync();

        return await reporitory.RefreshAsync(tenantId, xppId, role);
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
    private DbRole SetRole(long tenantId, long xppId, RoleModel model)
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
    private DbRole SetRole(long tenantId, long xppId, RoleModel model, DbRole dbRole)
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
    private async Task CheckNameAsync(long tenantId, long xppId, RoleModel model)
    {
        Expression<Func<DbRole, bool>> predicate = model.Id == 0 ? x => x.TenantId == tenantId && x.XppId == xppId && x.Name == model.Name : x => x.TenantId == tenantId && x.XppId == xppId && x.Id != model.Id && x.Name == model.Name;

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
    /// <returns></returns>
    public async Task<List<RoleModel>> All(long tenantId)
    {
        var roles = await _unitOfWork.GetRepository<DbRole>().AllAsync(x=>x.TenantId == tenantId);
        return _mapper.Map<List<RoleModel>>(roles);
    }

    #endregion
}
