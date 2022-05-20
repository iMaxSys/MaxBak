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
using iMaxSys.Identity.Data.EFCore;

namespace iMaxSys.Identity;

public static class Extensions
{
    public static void AddMaxIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddUnitOfWork<IdentityContext, IdentityReadOnlyContext>();
    }

    public static IApplicationBuilder UseMaxIdentity(this IApplicationBuilder builder)
    {
        //鉴权中间件
        return builder.UseMiddleware<IdentityMiddleware>();
    }
}