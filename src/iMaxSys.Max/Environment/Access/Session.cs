
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

using iMaxSys.Max.Options;
using iMaxSys.Max.Caching;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.Caching.Redis;

namespace iMaxSys.Max.Environment.Access;

/// <summary>
/// Session
/// </summary>
public class Session : ISession
{
    private readonly ISessionStore _sessionStore;
    private readonly string _id = "";

    /// <summary>
    /// SessionId
    /// </summary>
    public string Id { get => _id; }

    public Session(ISessionStore sessionStore)
    {
        _sessionStore = sessionStore;
        _id = _sessionStore.Id;
    }

    public T? Get<T>(string key)
    {
        return _sessionStore.Get<T>(key);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        return await _sessionStore.GetAsync<T>(key);
    }

    public void Set(string key, object data)
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            throw new MaxException(ResultCode.CantSetSession);
        }
        _sessionStore.Set(key, data);
    }

    public async Task SetAsync(string key, object data)
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            throw new MaxException(ResultCode.CantSetSession);
        }
        await _sessionStore.SetAsync(key, data);
    }

    public void Clear()
    {
        _sessionStore.Clear();
    }

    /// <summary>
    /// 清除指定key
    /// </summary>
    /// <param name="key"></param>
    public void Remove(string key)
    {
        _sessionStore.Remove(key);
    }
}