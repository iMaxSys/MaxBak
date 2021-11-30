//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IApplication.cs
//摘要: IApplication
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-11-15
//----------------------------------------------------------------

using System.Threading.Tasks;

using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Max.Environment.Access
{
    /// <summary>
    /// IApplication
    /// </summary>
    public interface IApplication : ISingleton
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T? Get<T>(string key);

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T?> GetAsync<T>(string key);

        /// <summary>
        /// 设置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        void Set(string key, object data);

        /// <summary>
        /// 设置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task SetAsync(string key, object data);

        /// <summary>
        /// Clear
        /// </summary>
        void Clear();

        /// <summary>
        /// 删除指定key
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}
