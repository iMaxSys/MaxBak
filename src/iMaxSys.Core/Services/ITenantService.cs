//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ITenantService.cs
//摘要: 租户服务接口 
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
/// 租户服务接口
/// </summary>
public interface ITenantService : IDependency
{
    /// <summary>
    /// 获取租户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Tenant> GetAsync(long id);
}


