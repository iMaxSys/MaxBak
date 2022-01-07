//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IRoleRepository.cs
//摘要: 角色仓储接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-01-07
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.Repositories;
using iMaxSys.Identity.Data.Entities;
using DbRole = iMaxSys.Identity.Data.Entities.Role;

namespace iMaxSys.Identity.Data.Repositories;

/// <summary>
/// 角色仓储接口
/// </summary>
public interface IRoleRepository : IRepository<DbRole>
{
    /// <summary>
    /// 读取角色
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<IRole?> ReadAsync(long roleId);

    /// <summary>
    /// RemoveAsync
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    Task RemoveAsync(long memberId);

    /// <summary>
    /// 刷新role缓存
    /// </summary>
    /// <param name="role"></param>
    /// <param name="expires"></param>
    /// <returns></returns>
    Task RefreshAsync(IRole role, DateTime expires);
}