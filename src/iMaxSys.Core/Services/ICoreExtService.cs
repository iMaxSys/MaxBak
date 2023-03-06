//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ICoreExtService.cs
//摘要: 核心服务扩展接口 
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
public interface ICoreExtService : IDependency
{
    /// <summary>
    /// 获取租户扩展
    /// </summary>
    /// <param name="id">user id</param>
    /// <returns></returns>
    Task<object> GetTenantExtAsync(long id);
}

