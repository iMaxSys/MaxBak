using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Max.Environment.Access
{
    /// <summary>
    /// ISession
    /// 此处继承ISingleton性能更好
    /// </summary>
    public interface ISession : IDependency
    {
        /// <summary>
        /// SessionId
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 获取Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T? Get<T>(string key);

        /// <summary>
        /// 获取Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T?> GetAsync<T>(string key);

        /// <summary>
        /// 设置Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        void Set(string key, object data);

        /// <summary>
        /// 设置Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task SetAsync(string key, object data);

        /// <summary>
        /// 清除
        /// </summary>
        void Clear();

        /// <summary>
        /// 清除指定key
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}