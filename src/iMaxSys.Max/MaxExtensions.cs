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

using iMaxSys.Max.Json;
using iMaxSys.Max.Common;
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
    const string CORS = "cors";

    public static void AddMax(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MaxOption>(configuration.GetSection(FXN));

        //entity from body
        //services.Configure<MvcOptions>(options =>
        //{
        //    options.Conventions.Add(new ActionModelConventionOnlyFromBody());
        //});
        //

        services.AddControllers();

        services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
        {
            MaxJsonOptions.Configure(options.JsonSerializerOptions);
        });

        //var assemblies = DependencyContext.Default.CompileLibraries.Where(c => c.Name.StartsWith(appName, StringComparison.CurrentCultureIgnoreCase) || c.Name.StartsWith(fxName, StringComparison.CurrentCultureIgnoreCase)).Select(x => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(x.Name)));
        services.AddHttpClient();
        services.AddHttpContextAccessor();
        //var ass = AppDomain.CurrentDomain.GetAssemblies();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddDependencyInjection();
        services.AddOptions();
        services.AddEndpointsApiExplorer();

        services.AddCors(options =>
        {
            options.AddPolicy(CORS, policy =>
            {
                policy.AllowAnyOrigin().WithOrigins("http://localhost:5173", "http://localhost:9527").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });
        });

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                //获取验证失败的模型字段 
                var errors = actionContext.ModelState
                    .Where(s => s.Value != null && s.Value.ValidationState == ModelValidationState.Invalid)
                    .SelectMany(s => s.Value!.Errors.ToList())
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // 统一返回格式
                var result = new Result()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "数据验证不通过",
                    Detail = string.Join(", ", errors)
                };

                return new BadRequestObjectResult(result);
            };
        });


        //services.AddCors(options =>
        //{
        //    options.AddPolicy(name: "cors", builder =>
        //    {
        //        //for when you're running on localhost
        //        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "*.iynss.com")
        //        .AllowAnyHeader().AllowAnyMethod();


        //        //builder.WithOrigins("url from where you're trying to do the requests")
        //    });
        //});

    }

    public static IApplicationBuilder UseMax(this IApplicationBuilder builder, Action<CoreOption>? coreAction = null, Action<ExceptionHandlingOptions>? exAction = null)
    {
        MaxOption option = builder.ApplicationServices.GetService<IOptions<MaxOption>>()!.Value;

        //IdWorker初始
        IdWorker.Init(option.Network.ServerId, option.Network.DataCenterId);

        //core相关中间件
        CoreOption coreOption;

        if (coreAction is not null)
        {
            coreOption = new CoreOption();
            coreAction(coreOption);
        }
        else
        {
            coreOption = option.Core;
        }

        if (coreOption.UseHttpsRedirection)
        {
            builder.UseHttpsRedirection();
        }

        if (coreOption.UseStaticFiles)
        {
            builder.UseStaticFiles();
        }

        if (coreOption.UseRouting)
        {
            builder.UseRouting();
        }

        builder.UseCors(CORS);

        if (coreOption.UseAuthorization)
        {
            builder.UseAuthorization();
        }

        //异常中间件
        ExceptionHandlingOptions exOptions = new ExceptionHandlingOptions();

        if (exAction is not null)
        {
            exAction(exOptions);
        }


        builder.UseMiddleware<ExceptionHandlingMiddleware>(exOptions);

        return builder;
    }
}