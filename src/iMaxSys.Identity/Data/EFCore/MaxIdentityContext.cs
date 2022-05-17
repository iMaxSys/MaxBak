//----------------------------------------------------------------
//Copyright (C) 2016-2026 Care Co.,Ltd.
//All rights reserved.
//
//文件: MaxIdentityContext.cs
//摘要: 身份系统上下文
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Data;
using iMaxSys.Identity.Common;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Options;

namespace iMaxSys.Identity.Data.EFCore;

/// <summary>
/// MaxContext
/// </summary>
public class MaxIdentityContext : DbContext
{
    private readonly MaxOption _maxOption;

    //public static readonly LoggerFactory LoggerFactory =
    //new LoggerFactory(new[] { new DebugLoggerProvider() });

    public MaxIdentityContext(IOptions<MaxOption> options)
    {
        _maxOption = options.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        List<DatabaseOption>? databases = _maxOption.Identity.Databases ?? _maxOption.Core.Databases;
        if (databases != null && databases.Count > 0)
        {
            optionsBuilder.UseDatabase(databases);
        }
        else
        {
            throw new MaxException(ResultCode.ConnectionIsNull);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyConfigurations(modelBuilder);
    }

    /// <summary>
    /// 注册映射配置
    /// </summary>
    /// <param name="modelBuilder">ModelBuilder</param>
    private void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        //映射Data模块配置
        var c = DependencyContext.Default.CompileLibraries.Where(c => c.Name.Contains("iMaxSys.Data")).FirstOrDefault();
        if (c != null)
        {
            var a = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(c.Name));
            modelBuilder.ApplyConfigurationsFromAssembly(a);
        }
        //映射本模块配置
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}