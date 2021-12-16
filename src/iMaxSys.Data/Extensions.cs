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
//日期：2018-03-07
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
        Type iroot = typeof(ICustomRepository);         //定制仓储接口标识
        Type root = typeof(IRepository<>);              //范型仓储接口标识
        Type ignores = typeof(EfRepository<>);          //排除中间类型
        Type st;

        //获取仓储实现类型集合
        var mts = types.Where(item => (item != ignores) && item.GetInterfaces().Any(i => (i.IsGenericType && i.GetGenericTypeDefinition() == root) || i == iroot));

        foreach (var assignedType in mts)
        {
            var serviceTypes = assignedType.GetInterfaces().Where(i => (i.IsGenericType && i.GetGenericTypeDefinition() == root) || i.GetInterfaces().Any(i => i.GetGenericTypeDefinition() == root));
            foreach (var serviceType in serviceTypes)
            {
                st = serviceType.IsGenericType ? (serviceType.GenericTypeArguments.Length > 0 && serviceType.GenericTypeArguments[0].IsGenericParameter ? serviceType.GetGenericTypeDefinition() : serviceType) : serviceType;
                services.AddScoped(st, assignedType);
            }
        }

        _registered = true;
    }

    public static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, string connection, DbServer type)
    {
        return type switch
        {
            DbServer.MariaDB => builder.UseMySql(connection, new MariaDbServerVersion(ServerVersion.AutoDetect(connection))),
            _ => builder.UseMySql(connection, new MariaDbServerVersion(ServerVersion.AutoDetect(connection))),
        };
    }
}