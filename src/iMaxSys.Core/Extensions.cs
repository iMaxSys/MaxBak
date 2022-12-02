//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Extensions.cs
//摘要: Extensions
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Data;
using iMaxSys.Core.Data.EFCore;

namespace iMaxSys.Core;

public static class Extensions
{
    public static void AddMaxCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddUnitOfWork<CoreContext, CoreReadOnlyContext>();
    }
}