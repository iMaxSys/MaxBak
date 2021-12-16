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
        Type ignores = typeof(EfRepository<>);          //排除中间类型, 多库情况下，直接注入范型仓储无法识别, 例如IReposotory<Rule>, 因为无法确定具体哪个库/DbContext
        Type irepository;

        //获取仓储实现类型集合, 来着IRepository<>和ICustomRepository之实现
        //var ts = types.Where(t => t != ignores && t.GetInterfaces().Any(i => (i.IsGenericType && i.GetGenericTypeDefinition() == root) || i == iroot));
        var repositories = types.Where(t => t.GetInterfaces().Any(i => (i.IsGenericType && i.GetGenericTypeDefinition() == root) || i == iroot));
        foreach (var repository in repositories)
        {
            var interfaces = repository.GetInterfaces();

            //定制仓储注册, 定制仓储注册后, 便不再注册范型仓储
            var icustomrepository = repository.GetInterfaces().FirstOrDefault(i => i.GetInterfaces().Contains(iroot));
            if (icustomrepository != null)
            {
                services.AddScoped(icustomrepository, repository);
                continue;
            }

            var its = interfaces.Where(i => (i.IsGenericType && i.GetGenericTypeDefinition() == root) || i.GetInterfaces().Any(i => i.GetGenericTypeDefinition() == root));
            foreach (var it in its)
            {
                irepository = it.IsGenericType ? (it.GenericTypeArguments.Length > 0 && it.GenericTypeArguments[0].IsGenericParameter ? it.GetGenericTypeDefinition() : it) : it;
                services.AddScoped(irepository, repository);
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