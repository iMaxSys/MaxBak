//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: UnitOfWork.cs
//摘要: UnitOfWork 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-12-17
//----------------------------------------------------------------

using iMaxSys.Max.Extentions;
using iMaxSys.Data.Common.Enums;
using iMaxSys.Data.Repositories;
using iMaxSys.Max.Options;
using iMaxSys.Data.EFCore.Repositories;
using iMaxSys.Max.Collection;

namespace iMaxSys.Data;

public static class Extensions
{
    /// <summary>
    /// 已注册标志
    /// </summary>
    static bool _registered = false;

    /// <summary>
    /// AddUnitOfWork
    /// </summary>
    /// <typeparam name="T">读写DbContext类型</typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : DbContext
    {
        services.AddDbContext<T>(ServiceLifetime.Scoped);
        services.AddScoped<IUnitOfWork<T>, UnitOfWork<T>>();
        services.AddScoped<IUnitOfWork, UnitOfWork<T>>();
        RegisterRepositories(services);
        return services;
    }

    /// <summary>
    /// AddUnitOfWork
    /// </summary>
    /// <typeparam name="T">读写DbContext类型</typeparam>
    /// <typeparam name="K">只读DbContext类型</typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddUnitOfWork<T, K>(this IServiceCollection services) where T : DbContext where K : DbContext
    {
        services.AddUnitOfWork<T>();
        services.AddUnitOfWork<K>();
        services.AddScoped<IUnitOfWork<T, K>, UnitOfWork<T, K>>();
        services.AddScoped<IUnitOfWork, UnitOfWork<T, K>>();
        return services;
    }

    /// <summary>
    /// 注册范型仓储
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterRepositories(IServiceCollection services)
    {
        //仅注册一次
        if (_registered)
        {
            return;
        }

        var types = UtilityExtensions.GetAppTypes();
        Type root = typeof(IRepositoryBase);                    //仓储接口标识
        Type iroot = typeof(IRepository<>);                     //范型仓储接口标识
        Type irroot = typeof(IReadOnlyRepository<>);            //范型只读仓储接口标识
        IEnumerable<Type>? irepositories;                       //读写仓储集合
        IEnumerable<Type>? irrepositories;                      //只读仓储集合

        //获取所有仓储实现类
        var repositories = types.Where(t => t.GetInterfaces().Any(x => x == root));

        //遍历实现类，进行注册
        foreach (var repository in repositories)
        {
            var interfaces = repository.GetInterfaces();

            //按范型(ef<>&xx<>)和非范型(定制)处理
            if (repository.IsGenericType)
            {
                //to-do:只读和读写会重复注册
                //范型读写
                irepositories = interfaces.Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == iroot).Select(x => x.GenericTypeArguments.Length > 0 && x.GenericTypeArguments[0].IsGenericParameter ? x.GetGenericTypeDefinition() : x);
                irepositories.ForEach(x => services.AddScoped(x, repository));

                //范型只读
                irrepositories = interfaces.Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == irroot).Select(x => x.GenericTypeArguments.Length > 0 && x.GenericTypeArguments[0].IsGenericParameter ? x.GetGenericTypeDefinition() : x);
                irrepositories.ForEach(x => services.AddScoped(x, repository));
            }
            else
            {
                //非范型只取非范型非仓储标识接口
                interfaces.Where(x => !x.IsGenericType && x != root).ForEach(x => services.AddScoped(x, repository));
            }
        }

        var x = services.Where(x => x.ServiceType.GetInterfaces().Any(i => i == root));

        _registered = true;
    }

    //public static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, string connection, DbServer type)
    //{
    //    return type switch
    //    {
    //        DbServer.MariaDB => builder.UseMySql(connection, new MariaDbServerVersion(ServerVersion.AutoDetect(connection))),
    //        DbServer.MySQL => builder.UseMySql(connection, new MariaDbServerVersion(ServerVersion.AutoDetect(connection))),
    //        _ => builder.UseMySql(connection, new MariaDbServerVersion(ServerVersion.AutoDetect(connection))),
    //    };
    //}

    //private static DbContextOptionsBuilder UseDatabase(DbContextOptionsBuilder optionsBuilder, List<DatabaseOption> databases, ref int index)
    //{
    //    int idx = index % databases.Count;
    //    index = ++index > 1024 ? 0 : index;
    //    return UseDatabase(optionsBuilder, databases[idx]);
    //}

    //private static DbContextOptionsBuilder UseDatabase(DbContextOptionsBuilder optionsBuilder, DatabaseOption database)
    //{
    //    return database.Type switch
    //    {
    //        0 => optionsBuilder.UseMySql(database.Connection, MariaDbServerVersion.LatestSupportedServerVersion),
    //        1 => optionsBuilder.UseSqlServer(database.Connection),
    //        _ => optionsBuilder.UseMySql(database.Connection, MariaDbServerVersion.LatestSupportedServerVersion),
    //    };
    //}
}