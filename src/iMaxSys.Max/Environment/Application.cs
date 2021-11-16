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

namespace iMaxSys.Max.Environment
{
    /// <summary>
    /// Application
    /// </summary>
    public class Application : IApplication
    {
        const string TAG_APP = "c:";

        private readonly ICache _cache;
        private readonly MaxOption _maxOption;

        public Application(IOptions<MaxOption> maxOption, IGenericCache cache)
        {
            _maxOption = maxOption.Value;
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>($"{TAG_APP}{key}");
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await _cache.GetAsync<T>($"{TAG_APP}{key}");
        }

        public void Set(string key, object data)
        {
            _cache.Set($"{TAG_APP}{key}", data);
        }

        public async Task SetAsync(string key, object data)
        {
            await _cache.SetAsync($"{TAG_APP}{key}", data);
        }
    }
}
