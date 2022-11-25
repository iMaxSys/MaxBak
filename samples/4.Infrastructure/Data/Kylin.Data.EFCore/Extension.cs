//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Extension.cs
//摘要: Extension
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using iMaxSys.Data;
using Kylin.Data.EFCore.Contexts;

namespace Kylin.Data.EFCore;

/// <summary>
/// Extension
/// </summary>
public static class Extension
{
    public static void AddKylinDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddUnitOfWork<KylinContext>();
        services.AddUnitOfWork<KylinContext, KylinContext>();
    }
}
