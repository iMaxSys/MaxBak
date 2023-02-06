//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: program.cs
//摘要: 系统入口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-10-20
//----------------------------------------------------------------

using Microsoft.Extensions.Configuration;

using iMaxSys.Max;
using iMaxSys.Data;
using iMaxSys.Core;
using iMaxSys.Identity;

using Kylin.Data.EFCore;
using Kylin.Framework.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
Configure(app, builder.Environment);

app.Run();

//register services
static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
{
    services.Configure<KylinOption>(configuration.GetSection("Kylin"));
    services.AddMax(configuration);
    services.AddMaxCore(configuration);
    services.AddMaxIdentity(configuration);

    services.AddSwaggerGen(option =>
    {
        // Add security definitions
        option.AddSecurityDefinition("Token", new OpenApiSecurityScheme()
        {
            Description = "Please enter 'Token' in the text box.",
            Name = "Token",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
             { new OpenApiSecurityScheme
                 {
                  Reference = new OpenApiReference()
                  {
                      Id = "Token",
                      Type = ReferenceType.SecurityScheme
                  }
             }, Array.Empty<string>() }
         });
    });

    services.AddKylinDataAccess(configuration);
}

//use middleware
static void Configure(WebApplication app, IWebHostEnvironment env)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        //app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseMax();
    app.UseMaxIdentity();
    app.MapControllers();
}
