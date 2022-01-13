//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IMenuRepository.cs
//摘要: 菜单仓储接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-01-07
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.Repositories;
using iMaxSys.Identity.Data.Entities;
using DbMenu = iMaxSys.Identity.Data.Entities.Menu;

namespace iMaxSys.Identity.Data.Repositories;

/// <summary>
/// 角色仓储接口
/// </summary>
public interface IMenuRepository : IRepository<DbMenu>
{
    /// <summary>
    /// 读取租户角色菜单
    /// </summary>
    /// <param name="xppId"></param>
    /// <param name="tenantId"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    Task<IMenu?> ReadAsync(long xppId, long tenantId, IRole role);

    /// <summary>
    /// 读取租户完整菜单
    /// </summary>
    /// <param name="xppId"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    Task<IMenu?> ReadFullAsync(long xppId, long tenantId);

    /// <summary>
    /// 刷新租户应用菜单缓存
    /// </summary>
    /// <param name="xppId"></param>
    /// <param name="tenantId"></param>
    /// <param name="expires"></param>
    /// <returns></returns>
    Task<IMenu?> RefreshAsync(long xppId, long tenantId, DateTime? expires = null);
}