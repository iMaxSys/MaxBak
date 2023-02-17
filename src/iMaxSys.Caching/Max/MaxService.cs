//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MaxService.cs
//摘要: MaxService
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Extentions;

namespace iMaxSys.Caching.Max;

/// <summary>
/// Max cache Service
/// </summary>
public class MaxService : IMaxService
{
    public string Separator => ":";

    private static Hashtable _store = _store ?? new Hashtable();

    /// <summary>
    /// 存储
    /// </summary>
    protected static Hashtable Store { get => _store; }

    public MaxService()
    {
        _store = new Hashtable();
    }

    public bool Delete(string key, bool global = false)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string key, bool global = false)
    {
        throw new NotImplementedException();
    }

    public T? Get<T>(string key, bool global = false)
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetAsync<T>(string key, bool global = false)
    {
        throw new NotImplementedException();
    }

    public Task<object?> GetAsync(string key, Type type, bool global = false)
    {
        throw new NotImplementedException();
    }

    public Task HashDeleteAsync(string key, string field, bool global = false)
    {
        throw new NotImplementedException();
    }

    public Task<Hashtable> HashGetAllAsync(string key, bool global = false)
    {
        throw new NotImplementedException();
    }

    public Task<string> HashGetAsync(string key, string field, bool global = false)
    {
        throw new NotImplementedException();
    }

    public Task HashSetAsync(string key, Hashtable ht, bool global = false)
    {
        throw new NotImplementedException();
    }

    public Task HashSetAsync(string key, string field, string value, bool global = false)
    {
        throw new NotImplementedException();
    }

    public Task<bool> KeyExistsAsync(string key, bool global = false)
    {
        throw new NotImplementedException();
    }

    public void Set(string key, object value, bool global = false)
    {
        throw new NotImplementedException();
    }

    public void Set(string key, object value, DateTime? expire, bool global = false)
    {
        throw new NotImplementedException();
    }

    public void Set(string key, object value, TimeSpan? timeSpan, bool global = false)
    {
        throw new NotImplementedException();
    }

    public Task SetAsync(string key, object value, bool global = false)
    {
        throw new NotImplementedException();
    }

    public Task SetAsync(string key, object value, DateTime? expire, bool global = false)
    {
        throw new NotImplementedException();
    }

    public Task SetAsync(string key, object value, TimeSpan? timeSpan, bool global = false)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 新增到集合
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Task SetAddAsync(string key, string value, TimeSpan? timeSpan, bool global = false)
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// 新增到集合
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public Task SetAddAsync(string key, string[] values, TimeSpan? timeSpan, bool global = false)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 集合是否包含
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Task<bool> SetContainsAsync(string key, string value, bool global = false)
    {
        throw new NotImplementedException();
    }

    public Task SetAsync<T>(string key, object value, TimeSpan? timeSpan, bool global = false)
    {
        throw new NotImplementedException();
    }
}