// ----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: CompanyService.cs
//摘要: 公司服务
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2022-10-15
//----------------------------------------------------------------

using iMaxSys.Max.DependencyInjection;
using iMaxSys.Core.Services;
using iMaxSys.Identity;

namespace Kylin.Services.Common.Services;

/// <summary>
/// 用户服务接口
/// </summary>
public class CompanyService : ICompanyService
{
    /// <summary>
    /// 获取租户扩展
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<object> GetTenantExtAsync(long id)
    {
        throw new NotImplementedException();
    }
}