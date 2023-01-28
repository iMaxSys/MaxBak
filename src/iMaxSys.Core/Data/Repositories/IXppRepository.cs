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

using Azure;
using iMaxSys.Core.Data.Entities;
using iMaxSys.Core.Data.Repositories;
using iMaxSys.Core.Models;
using iMaxSys.Max.Exceptions;

namespace iMaxSys.Core.Data.Repositories;

/// <summary>
/// 应用仓储接口
/// </summary>
public interface IXppRepository : ICoreRepository<Xpp>
{
    /// <summary>
    /// 获取应用信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    Task<XppModel> GetXppAsync(long id);

    /// <summary>
    /// 获取社交信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<XppSnsModel> GetSnsAsync(long id);

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
    Task<XppModel> RefreshXppAsync(long id);

    /// <summary>
    /// 刷新应用信息
    /// </summary>
    /// <param name="xpp"></param>
    /// <returns></returns>
    Task<XppModel> RefreshXppAsync(Xpp? xpp);

    /// <summary>
    /// 刷新应用社交信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<XppSnsModel> RefreshSnsAsync(long id);

    /// <summary>
    /// 刷新应用社交信息
    /// </summary>
    /// <param name="xppSnses"></param>
    /// <returns></returns>
    Task<XppSnsModel> RefreshSnsAsync(XppSns? xppSns);

    /// <summary>
    /// 刷新应用社交信息
    /// </summary>
    /// <param name="xppSnses"></param>
    /// <returns></returns>
    Task RefreshSnsesAsync(ICollection<XppSns>? xppSnses);
}
