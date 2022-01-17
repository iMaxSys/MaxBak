//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ExceptionHandlingMiddleware.cs
//摘要: ExceptionHandlingMiddleware
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Caching;
using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max.Environment.Access;

public class SessionStore : ISessionStore
{
    const string TAG_SESSION = "s:";

    private readonly ICache _cache;
    private readonly MaxOption _maxOption;

    private string _id;

    public string Id => _id ??= Guid.NewGuid().ToString().Replace("-", "");

    public string Key => throw new NotImplementedException();

    public SessionStore(IOptions<MaxOption> maxOption, IGenericCache cache)
    {
        _maxOption = maxOption.Value;
        _cache = cache;
        _id = Guid.NewGuid().ToString().Replace("-", "");
    }

    public void SetId(string id)
    {
        _id = id;
    }

    public T? Get<T>(string key)
    {
        return _cache.Get<T>($"{TAG_SESSION}{Id}:{key}");
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        return await _cache.GetAsync<T>($"{TAG_SESSION}{Id}:{key}");
    }

    public void Set(string key, object data)
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            throw new MaxException(ResultCode.CantSetSession);
        }
        _cache.Set($"{TAG_SESSION}{Id}:{key}", data, DateTime.Now.AddMinutes(_maxOption.Identity.Expires));
    }

    public async Task SetAsync(string key, object data)
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            throw new MaxException(ResultCode.CantSetSession);
        }
        await _cache.SetAsync($"{TAG_SESSION}{Id}:{key}", data, DateTime.Now.AddMinutes(_maxOption.Identity.Expires));
    }

    public void Clear()
    {
        _cache.Delete($"{TAG_SESSION}{Id}");
    }

    /// <summary>
    /// 删除指定key
    /// </summary>
    /// <param name="key"></param>
    public void Remove(string key)
    {
        _cache.Delete($"{TAG_SESSION}{Id}:{key}");
    }
}