//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ICoreService.cs
//摘要: 核心服务接口 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-29
//----------------------------------------------------------------

using iMaxSys.Max;
using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Core.Services;

/// <summary>
/// 核心服务接口
/// </summary>
public interface ICoreService : IDependency
{
    /// <summary>
    /// 获取xpp
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Xpp> GetXppAsync(long id);

    /// <summary>
    /// 获取xppSns
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<XppSns> GetXppSnsAsync(long id);

    /// <summary>
    /// 刷新缓存
    /// </summary>
    /// <returns></returns>
    Task RefreshAsync();
}

