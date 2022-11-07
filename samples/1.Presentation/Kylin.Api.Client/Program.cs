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
using iMaxSys.Identity;

using Kylin.Framework.Options;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
Configure(app, builder.Environment);

app.Run();

//register services
static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
{
    services.AddMax(configuration);
    services.AddMaxIdentity(configuration);
    services.AddSwaggerGen();
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
