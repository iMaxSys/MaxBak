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

using iMaxSys.Max.Domain;
using iMaxSys.Max.Extentions;
using iMaxSys.Data.EFCore;

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
    /// <typeparam name="T">DbContext</typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : DbContext
    {
        services.AddScoped<IUnitOfWork<T>, UnitOfWork<T>>();
        services.AddScoped<IUnitOfWork, UnitOfWork<T>>();
        RegisterRepositories(services);
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
        Type root = typeof(IRepository);                    //仓储接口标识
        Type iroot = typeof(IRepository<>);                 //范型仓储接口标识
        IEnumerable<Type>? irepositories;

        //获取所有仓储实现类
        var repositories = types.Where(t => t.GetInterfaces().Any(x => x == root));
        foreach (var repository in repositories)
        {
            var interfaces = repository.GetInterfaces();

            //按范型(ef<>&xx<>)和非范型(定制)处理
            if (repository.IsGenericType)
            {
                //范型实现只取范型接口
                irepositories = interfaces.Where(x => x.IsGenericType).Select(x => x.GenericTypeArguments.Length > 0 && x.GenericTypeArguments[0].IsGenericParameter ? x.GetGenericTypeDefinition() : x);
            }
            else
            {
                //非范型只取非范型非仓储标识接口
                irepositories = interfaces.Where(x => !x.IsGenericType && x != root);
            }

            foreach (var irepository in irepositories)
            {
                services.AddScoped(irepository, repository);
            }
        }

        //var x = services.Where(x => x.ServiceType.GetInterfaces().Any(i => i == root));

        _registered = true;
    }

    public static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, string connection, DbServer type)
    {
        return type switch
        {
            DbServer.MariaDB => builder.UseMySql(connection, new MariaDbServerVersion(ServerVersion.AutoDetect(connection))),
            DbServer.MySQL => builder.UseMySql(connection, new MariaDbServerVersion(ServerVersion.AutoDetect(connection))),
            _ => builder.UseMySql(connection, new MariaDbServerVersion(ServerVersion.AutoDetect(connection))),
        };
    }
}