//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: DependencyInjectionExtensions.cs
//摘要: 统一依赖注入
//说明:
//
//当前：1.1
//作者：陶剑扬
//日期：2017-11-15
//说明：改为获取所有程序集
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using iMaxSys.Max.Data;
using iMaxSys.Max.Extentions;
using iMaxSys.Max.Data.EFCore;
using System.Collections.Generic;

namespace iMaxSys.Max.DependencyInjection
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// AddDependencyInjection
        /// </summary>
        /// <param name="services"></param>
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            ServiceLifetime lifetime;

            Type[] flags = { typeof(IDependency), typeof(ISingleton), typeof(ITransient) };

            //var singleton = typeof(ISingleton);
            //var transient = typeof(ITransient);
            //var scoped = typeof(IDependency);

            //为提高性能,只加载框架相关程序集和应用程序程序集
            var assemblies = UtilityExtentions.GetAppAssemblies();

            //获取所有包含接口标识类型
            var types = assemblies.SelectMany(x => x.GetTypes()).Where(c => c.IsClass && !c.IsAbstract && c.GetInterfaces().Contains(flags[0]));

            //按实现类型来循环
            foreach (var type in types)
            {
                //该类型实现的所有接口
                var its = type.GetInterfaces().Where(x => !flags.Contains(x));
                foreach (var it in its)
                {
                    //重置to Scoped
                    lifetime = ServiceLifetime.Scoped;

                    //不可指派为scoped的皆跳过
                    if (!flags[0].IsAssignableFrom(it))
                    {
                        continue;
                    }

                    //该接口是否可以指派为注入标识
                    if (flags[1].IsAssignableFrom(it))
                    {
                        lifetime = ServiceLifetime.Singleton;
                    }
                    else
                    {
                        if (flags[2].IsAssignableFrom(it))
                        {
                            lifetime = ServiceLifetime.Transient;
                        }
                    }
                    //注册
                    services.Add(new ServiceDescriptor(it, type.IsGenericType ? type.GetGenericTypeDefinition() : type, lifetime));
                }
            }
        }
    }
}