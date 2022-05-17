//----------------------------------------------------------------
//Copyright (C) 2016-2025 Co.,Ltd.
//All rights reserved.
//
//文件: MaxDbContext.cs
//摘要: MaxDbContext 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

namespace iMaxSys.Data.EFCore;

public class EfDbContext : DbContext
{
    public string? ConnectionString { get; }

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
        var cls = DependencyContext.Default.CompileLibraries.Where(c => c.Name.Contains(AppDomain.CurrentDomain.FriendlyName[..AppDomain.CurrentDomain.FriendlyName.IndexOf('.')]));

        foreach (var item in cls)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(item.Name));
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}