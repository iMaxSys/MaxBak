//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ApplicationStore.cs
//摘要: IApplicationStore
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-11-30
//----------------------------------------------------------------

using iMaxSys.Max.Caching;

namespace iMaxSys.Max.Environment.Access;

public class ApplicationStore : IApplicationStore
{
    const string TAG_APP = "a:";

    private readonly ICache _cache;
    private readonly MaxOption _maxOption;

    public ApplicationStore(IOptions<MaxOption> maxOption, ICacheFactory cacheFactory)
    {
        _maxOption = maxOption.Value;
        _cache = cacheFactory.GetService();
    }

    public T? Get<T>(string key)
    {
        return _cache.Get<T>($"{TAG_APP}{key}");
    }

    public async Task<T?> GetAsync<T>(string key)
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

    public void Clear()
    {
        _cache.Delete(TAG_APP);
    }

    /// <summary>
    /// 删除指定key
    /// </summary>
    /// <param name="key"></param>
    public void Remove(string key)
    {
        _cache.Delete($"{TAG_APP}{key}");
    }
}