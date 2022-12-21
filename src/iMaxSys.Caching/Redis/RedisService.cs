//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: RedisService.cs
//摘要: RedisService
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Extentions;

namespace iMaxSys.Caching.Redis;

/// <summary>
/// RedisService
/// </summary>
public class RedisService : IRedisService
{
    private readonly ConnectionMultiplexer _connection;
    private readonly IDatabase _database;
    private readonly long _appId;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="appId"></param>
    public RedisService(string connection, long appId)
    {
        _connection = ConnectionMultiplexer.Connect(connection);
        _database = _connection.GetDatabase();
        _appId = appId;
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="option"></param>
    public RedisService(IOptions<MaxOption> option)
    {
        _connection = ConnectionMultiplexer.Connect(option.Value.Caching.Connection);
        _database = _connection.GetDatabase();
        _appId = option.Value.XppId;
    }

    /// <summary>
    /// 路径分隔符
    /// </summary>
    public string Separator => ":";

    /// <summary>
    /// getKey
    /// </summary>
    /// <param name="key"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    private string GetKey(string key, bool global = false)
    {
        return (global ? key : $"{_appId}:{key}");
    }

    /// <summary>
    /// 存在键
    /// </summary>
    /// <returns></returns>
    public async Task<bool> KeyExistsAsync(string key, bool global = false)
    {
        return await _database.KeyExistsAsync(GetKey(key, global));
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    public T? Get<T>(string key, bool global = false)
    {
        var value = _database.StringGet(GetKey(key, global));
        return value.IsNull ? default : value.ToString().ToObject<T>();
    }

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    public async Task<T?> GetAsync<T>(string key, bool global = false)
    {
        var value = await _database.StringGetAsync(GetKey(key, global));
        return value.IsNull ? default : value.ToString().ToObject<T>();
    }

    /// <summary>
    /// Set
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="global"></param>
    public void Set(string key, object value, bool global = false)
    {
        _database.StringSet(GetKey(key, global), value.ToJson());
    }

    /// <summary>
    /// SetAsync
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    public async Task SetAsync(string key, object value, bool global = false)
    {
        await _database.StringSetAsync(GetKey(key, global), value.ToJson());
    }

    /// <summary>
    /// Set
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expire"></param>
    /// <param name="global"></param>
    public void Set(string key, object value, DateTime? expire, bool global = false)
    {
        _database.StringSet(GetKey(key, global), value.ToJson(), expire - DateTime.Now);
    }

    // <summary>
    /// Set
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="timeSpan"></param>
    /// <param name="global"></param>
    public void Set(string key, object value, TimeSpan? timeSpan, bool global = false)
    {
        _database.StringSet(GetKey(key, global), value.ToJson(), timeSpan);
    }

    /// <summary>
    /// SetAsync
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expire"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    public async Task SetAsync(string key, object value, DateTime? expire, bool global = false)
    {
        await _database.StringSetAsync(GetKey(key, global), value.ToJson(), expire - DateTime.Now);
    }

    /// <summary>
    /// SetAsync
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="timeSpan"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    public async Task SetAsync(string key, object value, TimeSpan? timeSpan, bool global = false)
    {
        await _database.StringSetAsync(GetKey(key, global), value.ToJson(), timeSpan);
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="key"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    public bool Delete(string key, bool global = false)
    {
        return _database.KeyDelete(GetKey(key, global));
    }

    /// <summary>
    /// DeleteAsync
    /// </summary>
    /// <param name="key"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(string key, bool global = false)
    {
        return await _database.KeyDeleteAsync(GetKey(key, global));
    }

    /// <summary>
    /// HashSetAsync
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ht"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    public async Task HashSetAsync(string key, Hashtable ht, bool global = false)
    {
        int i = 0;
        HashEntry[] hashEntries = new HashEntry[ht.Keys.Count];
        foreach (var k in ht.Keys)
        {
            hashEntries[i++] = new HashEntry(k.ToString(), ht[k]?.ToString());
        }
        await _database.HashSetAsync(GetKey(key, global), hashEntries);
    }

    /// <summary>
    /// HashSetAsync
    /// </summary>
    /// <param name="key"></param>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    public async Task HashSetAsync(string key, string field, string value, bool global = false)
    {
        await _database.HashSetAsync(GetKey(key, global), field, value);
    }

    /// <summary>
    /// HashGetAllAsync
    /// </summary>
    /// <param name="key"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    public async Task<Hashtable> HashGetAllAsync(string key, bool global = false)
    {
        Hashtable ht = new();
        HashEntry[] hashEntries = await _database.HashGetAllAsync(GetKey(key, global));
        foreach (var item in hashEntries)
        {
            ht.Add(item.Name.ToString(), item.Value.ToString());
        }
        return ht;
    }

    /// <summary>
    /// HashGetAsync
    /// </summary>
    /// <param name="key"></param>
    /// <param name="field"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    public async Task<string> HashGetAsync(string key, string field, bool global = false)
    {
        var value = await _database.HashGetAsync(GetKey(key, global), field);
        return value.IsNull ? String.Empty : value.ToString();
    }

    /// <summary>
    /// HashDeleteAsync
    /// </summary>
    /// <param name="key"></param>
    /// <param name="field"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    public async Task HashDeleteAsync(string key, string field, bool global = false)
    {
        await _database.HashDeleteAsync(GetKey(key, global), field);
    }
}