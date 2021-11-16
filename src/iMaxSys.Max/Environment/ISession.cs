using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Max.Environment
{
    public interface ISession : ISingleton
    {
        /// <summary>
        /// SessionId
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// 获取Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 获取Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);

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
    }
}