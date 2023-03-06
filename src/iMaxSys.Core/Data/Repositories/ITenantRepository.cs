//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ITenantRepository.cs
//摘要: 租户仓储接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max;
using iMaxSys.Max.Exceptions;
using iMaxSys.Core.Models;
using iMaxSys.Core.Data.Repositories;
using DbXpp = iMaxSys.Core.Data.Entities.Xpp;
using DbXppSns = iMaxSys.Core.Data.Entities.XppSns;
using DbTenant = iMaxSys.Core.Data.Entities.Tenant;

namespace iMaxSys.Core.Data.Repositories;

/// <summary>
/// 应用仓储接口
/// </summary>
public interface ITenantRepository : ICoreReadOnlyRepository<DbTenant>
{
    /// <summary>
    /// 获取租户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Tenant> GetAsync(long id);

    /// <summary>
    /// 刷新全部租户信息
    /// </summary>
    /// <returns></returns>
    Task RefreshAsync();

    /// <summary>
    /// 刷新租户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Tenant> RefreshAsync(long id);
}
