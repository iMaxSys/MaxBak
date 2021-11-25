//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ServiceLocator.cs
//摘要: 服务定位器
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-2-11
//----------------------------------------------------------------

using System;

using Microsoft.Extensions.DependencyInjection;

namespace iMaxSys.Max.DependencyInjection
{
    /// <summary>
    /// 服务定位器
    /// </summary>
    public static class ServiceLocator
    {
        /// <summary>
        /// IServiceProvider
        /// </summary>
        public static IServiceProvider? ServiceProvider { get; private set; }

        /// <summary>
        /// 设置ServiceProvider
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T? GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
