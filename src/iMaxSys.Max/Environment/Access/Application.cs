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

namespace iMaxSys.Max.Environment.Access
{
    /// <summary>
    /// Application
    /// </summary>
    public class Application : IApplication
    {
        private readonly IApplicationStore _applicationStore;


        public Application(IApplicationStore applicationStore)
        {
            _applicationStore = applicationStore;
        }

        public T? Get<T>(string key)
        {
            return _applicationStore.Get<T>(key);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            return await _applicationStore.GetAsync<T>(key);
        }

        public void Set(string key, object data)
        {
            _applicationStore.Set(key, data);
        }

        public async Task SetAsync(string key, object data)
        {
            await _applicationStore.SetAsync(key, data);
        }

        public void Clear()
        {
            _applicationStore.Clear();
        }

        /// <summary>
        /// 清除指定key
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _applicationStore.Remove(key);
        }
    }
}