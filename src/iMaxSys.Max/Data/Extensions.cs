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

using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using iMaxSys.Max.Data.EFCore;
using iMaxSys.Max.Extentions;

namespace iMaxSys.Max.Data
{
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

            var types = UtilityExtentions.GetAppTypes();
            var root = typeof(IRepository<>);
            Type ignores = typeof(EfRepository<>);
            Type st;

            var mts = types.Where(item => (item != ignores) && item.GetInterfaces().Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == root));

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
    }
}