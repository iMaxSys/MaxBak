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
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

using iMaxSys.Max.Caching;
using iMaxSys.Max.Options;
using iMaxSys.Max.Caching.Redis;

namespace iMaxSys.Max.Environment.Access
{
    /// <summary>
    /// Application
    /// </summary>
    public class Application : IApplication
    {
        private readonly IApplication _application;


        public Application(IApplication application)
        {
            _application = application;
        }

        public T? Get<T>(string key)
        {
            return _application.Get<T>(key);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            return await _application.GetAsync<T>(key);
        }

        public void Set(string key, object data)
        {
            _application.Set(key, data);
        }

        public async Task SetAsync(string key, object data)
        {
            await _application.SetAsync(key, data);
        }

        public void Clear()
        {
            _application.Clear();
        }

        /// <summary>
        /// 清除指定key
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _application.Remove(key);
        }
    }
}