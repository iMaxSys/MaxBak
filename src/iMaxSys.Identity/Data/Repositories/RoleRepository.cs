//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: RoleRepository.cs
//摘要: 角色仓储
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-01-07
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Max.Caching;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.EFCore;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Common;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Entities;
using DbRole = iMaxSys.Identity.Data.Entities.Role;

namespace iMaxSys.Identity.Data.Repositories;

/// <summary>
/// 角色仓储
/// </summary>
public class RoleRepository : IdentityRepository<DbRole>, IRoleRepository
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="context"></param>
    /// <param name="option"></param>
    /// <param name="cacheFactory"></param>
    public RoleRepository(IdentityContext context, IMapper mapper, IOptions<MaxOption> option, ICacheFactory cacheFactory) : base(context, mapper, option, cacheFactory)
    {
    }

    /// <summary>
    /// get
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<RoleResult> GetAsync(long tenantId, long xppId, long roleId)
    {
        RoleResult roleModel;

        //取缓存
        IRole? role = await Cache.GetAsync<RoleResult>(GetRoleKey(tenantId, roleId), _global);

        //为空则刷新
        if (role is null)
        {
            roleModel = await RefreshAsync(tenantId, xppId, roleId);
        }
        else
        {
            roleModel = (RoleResult)role;
        }

        return roleModel;
    }

    #region FindAsync

    /// <summary>
    /// find role from database
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<DbRole> FindAsync(long tenantId, long xppId, long roleId)
    {
        var dbRole = await FirstOrDefaultAsync(x => x.TenantId == tenantId && x.XppId == xppId && x.Id == roleId);

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

    #region RemoveAsync

    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task RemoveAsync(long tenantId, long xppId, long roleId)
    {
        //软删除
        Remove(x => x.TenantId == tenantId && x.XppId == xppId && x.Id == roleId);
        await Cache.DeleteAsync(GetRoleKey(tenantId, roleId), _global);
        await Cache.DeleteAsync(GetRoleRoutersKey(tenantId, roleId), _global);
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
        var dbRole = await FindAsync(tenantId, xppId, roleId);
        return await RefreshAsync(tenantId, xppId, dbRole);
    }

    /// <summary>
    /// RefreshAsync
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="dbRole"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public async Task<RoleResult> RefreshAsync(long tenantId, long xppId, DbRole dbRole)
    {
        RoleResult roleModel = Mapper.Map<RoleResult>(dbRole);
        await Cache.SetAsync(GetRoleKey(tenantId, dbRole.Id), roleModel, new TimeSpan(0, Option.Identity.Expires, 0), _global);
        return roleModel;
    }

    #endregion  

    #region GetRoleKey

    /// <summary>
    /// 获取角色Basekey
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    private string GetRoleBaseKey(long tenantId, long roleId) => $"{_tagRole}{tenantId}{Cache.Separator}{roleId}";

    /// <summary>
    /// 获取角色key
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    private string GetRoleKey(long tenantId, long roleId) => $"{GetRoleBaseKey(tenantId, roleId)}{Cache.Separator}i";

    /// <summary>
    /// 获取角色路由key
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    private string GetRoleRoutersKey(long tenantId, long roleId) => $"{GetRoleBaseKey(tenantId, roleId)}{Cache.Separator}r";

    #endregion
}