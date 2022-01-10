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
    /// 读取菜单
    /// </summary>
    /// <param name="xpp"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<IMenu?> ReadAsync(long xpp, long roleId);

    /// <summary>
    /// 获取菜单
    /// </summary>
    /// <param name="xpp"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<IMenu?> GetAsync(long xpp, long roleId);

    /// <summary>
    /// 刷新菜单缓存
    /// </summary>
    /// <param name="role"></param>
    /// <param name="expires"></param>
    /// <returns></returns>
    Task RefreshAsync(IRole role, DateTime expires);
}