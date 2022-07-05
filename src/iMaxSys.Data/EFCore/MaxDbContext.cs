//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ReadOnlyDbContext.cs
//摘要: 只读仓储
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-05-07
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Data.DbContexts;

namespace iMaxSys.Data.EFCore;

/// <summary>
/// MaxDbContext
/// </summary>
public abstract class MaxDbContext : DbContext, IDbContextBase
{
    /// <summary>
    /// 数据库配置列表
    /// </summary>
    protected readonly List<DatabaseOption> _databases;

    public MaxDbContext(List<DatabaseOption> databases)
    {
        _databases = databases;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyConfigurations(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        UseDatabase(optionsBuilder, _databases);
    }

    /// <summary>
    /// 注册映射配置
    /// </summary>
    /// <param name="modelBuilder">ModelBuilder</param>
    private static void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        //throw new Exception(Assembly.GetExecutingAssembly().GetName().Name);
        //var cls = DependencyContext.Default.CompileLibraries.Where(c => c.Name.Contains(AppDomain.CurrentDomain.FriendlyName[..AppDomain.CurrentDomain.FriendlyName.IndexOf('.')]));

        var cls = DependencyContext.Default.CompileLibraries.Where(c => c.Name.Contains("iMaxSys.Identity"));

        foreach (var item in cls)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(item.Name));
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        //映射Core模块配置
        //var c = DependencyContext.Default.CompileLibraries.Where(c => c.Name.Contains("iMaxSys.Identity")).FirstOrDefault();

        //var a = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(c.Name));
        //modelBuilder.ApplyConfigurationsFromAssembly(a);

        ////映射本模块配置
        //modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }

    /// <summary>
    /// 选取数据库
    /// </summary>
    /// <param name="databases"></param>
    protected abstract DatabaseOption SelectDatabase(List<DatabaseOption> databases);


    /// <summary>
    /// UseDatabase
    /// </summary>
    /// <param name="optionsBuilder"></param>
    /// <param name="databases"></param>
    protected virtual void UseDatabase(DbContextOptionsBuilder optionsBuilder, List<DatabaseOption> databases)
    {
        DatabaseOption database = SelectDatabase(databases);

        switch (database.Type)
        {
            case 0:
                optionsBuilder.UseMySql(database.Connection, MariaDbServerVersion.LatestSupportedServerVersion);
                break;
            case 1:
                optionsBuilder.UseSqlServer(database.Connection);
                break;
            default:
                optionsBuilder.UseMySql(database.Connection, MariaDbServerVersion.LatestSupportedServerVersion);
                break;
        }

        //switch (database.Type)
        //{
        //    case 0:
        //        optionsBuilder.UseMySql(database.Connection, MariaDbServerVersion.AutoDetect(database.Connection));
        //        break;
        //    case 1:
        //        optionsBuilder.UseSqlServer(database.Connection);
        //        break;
        //    default:
        //        optionsBuilder.UseMySql(database.Connection, MariaDbServerVersion.AutoDetect(database.Connection));
        //        break;
        //}
    }
}