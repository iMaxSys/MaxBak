//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MaxExtensions.cs
//摘要: MaxExtensions
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-10-20
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Max;

/// <summary>
/// MaxExtensions
/// </summary>
public static class MaxExtensions
{
    const string FXN = "Max";

    public static void AddMax(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MaxOption>(configuration.GetSection(FXN));
        //entity from body
        //services.Configure<MvcOptions>(options =>
        //{
        //    options.Conventions.Add(new ActionModelConventionOnlyFromBody());
        //});
        //
        services.AddControllers().AddJsonOptions(options =>
        {
            //options.JsonSerializerOptions.Converters.Add(new Json.Converters.IntConverter());
            //options.JsonSerializerOptions.Converters.Add(new Json.Converters.IntNullableConverter());
            //options.JsonSerializerOptions.Converters.Add(new Json.Converters.LongConverter());
            //options.JsonSerializerOptions.Converters.Add(new Json.Converters.LongNullableConverter());
            options.JsonSerializerOptions.Converters.Add(new Json.Converters.DateTimeConverter());
            options.JsonSerializerOptions.Converters.Add(new Json.Converters.DateTimeNullableConverter());
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(allowedRanges: UnicodeRanges.All);
        });
        //var assemblies = DependencyContext.Default.CompileLibraries.Where(c => c.Name.StartsWith(appName, StringComparison.CurrentCultureIgnoreCase) || c.Name.StartsWith(fxName, StringComparison.CurrentCultureIgnoreCase)).Select(x => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(x.Name)));
        services.AddHttpClient();
        services.AddHttpContextAccessor();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddDependencyInjection();
        services.AddOptions();
        services.AddEndpointsApiExplorer();
    }

    public static IApplicationBuilder UseMax(this IApplicationBuilder builder)
    {
        //IdWorker初始
        MaxOption option = builder.ApplicationServices.GetService<IOptions<MaxOption>>()!.Value;
        IdWorker.Init(option.Network.ServerId, option.Network.DataCenterId);
        //异常&身份中间件
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}