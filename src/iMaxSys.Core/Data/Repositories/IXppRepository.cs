//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IXppRepository.cs
//摘要: 应用仓储接口
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

namespace iMaxSys.Core.Data.Repositories;

/// <summary>
/// 应用仓储接口
/// </summary>
public interface IXppRepository : ICoreReadOnlyRepository<DbXpp>
{
    /// <summary>
    /// 获取应用信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Xpp> GetXppAsync(long id);

    /// <summary>
    /// 获取社交信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<XppSns> GetSnsAsync(long id);

    /// <summary>
    /// 刷新全部应用信息
    /// </summary>
    /// <returns></returns>
    Task RefreshAsync();

    /// <summary>
    /// 刷新应用信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Xpp> RefreshXppAsync(long id);

    /// <summary>
    /// 刷新应用信息
    /// </summary>
    /// <param name="xpp"></param>
    /// <returns></returns>
    Task<Xpp> RefreshXppAsync(DbXpp? xpp);

    /// <summary>
    /// 刷新应用社交信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<XppSns> RefreshSnsAsync(long id);

    /// <summary>
    /// 刷新应用社交信息
    /// </summary>
    /// <param name="xppSnses"></param>
    /// <returns></returns>
    Task<XppSns> RefreshSnsAsync(DbXppSns? xppSns);

    /// <summary>
    /// 刷新应用社交信息
    /// </summary>
    /// <param name="xppSnses"></param>
    /// <returns></returns>
    Task RefreshSnsesAsync(ICollection<DbXppSns>? xppSnses);
}
