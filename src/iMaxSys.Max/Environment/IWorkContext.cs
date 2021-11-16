//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IWorkContext.cs
//摘要: IWorkContext
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Max.Environment
{
    /// <summary>
    /// IWorkContext
    /// </summary>
    public interface IWorkContext : IDependency
    {
        /// <summary>
        /// 访问链
        /// </summary>
        IAccessChain AccessChain { get; set; }

        /// <summary>
        /// Application
        /// </summary>
        IApplication Application { get; }

        /// <summary>
        /// Session
        /// </summary>
        ISession Session { get; }

        /// <summary>
        /// IP
        /// </summary>
        string IP { get; set; }

        /// <summary>
        /// Get service of type
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <returns></returns>
        T GetService<T>();

        /// <summary>
        /// Get service of type
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <returns></returns>
        T GetRequiredService<T>();
    }
}
