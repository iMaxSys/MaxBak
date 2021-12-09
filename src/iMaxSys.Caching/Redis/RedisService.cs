//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: RedisService.cs
//摘要: RedisService
//说明: newton
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Caching.Redis;

/// <summary>
/// RedisService
/// </summary>
public class RedisService : IRedisService
{
    private readonly long _appId;

    private readonly ConnectionMultiplexer _connection;
    private readonly IDatabase _database;

    //private readonly Newtonsoft.Json.JsonSerializerSettings _jsonConfig = new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore };

    public RedisService(string connection, long appId)
    {
        _connection = ConnectionMultiplexer.Connect(connection);
        _database = _connection.GetDatabase();
        _appId = appId;
    }

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

    public T? Get<T>(string key, bool global = false)
    {
        var value = _database.StringGet(GetKey(key, global));
        return value.IsNull ? default : JsonSerializer.Deserialize<T>(value);
    }

    public async Task<T?> GetAsync<T>(string key, bool global = false)
    {
        var value = await _database.StringGetAsync(GetKey(key, global));
        return value.IsNull ? default : JsonSerializer.Deserialize<T>(value);
    }

    public void Set(string key, object value, bool global = false)
    {
        _database.StringSet(GetKey(key, global), JsonSerializer.Serialize(value));
    }

    public async Task SetAsync(string key, object value, bool global = false)
    {
        await _database.StringSetAsync(GetKey(key, global), JsonSerializer.Serialize(value));
    }

    public void Set(string key, object value, DateTime? expire, bool global = false)
    {
        _database.StringSet(GetKey(key, global), JsonSerializer.Serialize(value), expire - DateTime.Now);
    }

    public async Task SetAsync(string key, object value, DateTime? expire, bool global = false)
    {
        await _database.StringSetAsync(GetKey(key, global), JsonSerializer.Serialize(value), expire - DateTime.Now);
    }

    public bool Delete(string key, bool global = false)
    {
        return _database.KeyDelete(GetKey(key, global));
    }

    public async Task<bool> DeleteAsync(string key, bool global = false)
    {
        return await _database.KeyDeleteAsync(GetKey(key, global));
    }

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

    public async Task HashSetAsync(string key, string field, string value, bool global = false)
    {
        await _database.HashSetAsync(GetKey(key, global), field, value);
    }

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

    public async Task<string> HashGetAsync(string key, string field, bool global = false)
    {
        return await _database.HashGetAsync(GetKey(key, global), field);
    }

    public async Task HashDeleteAsync(string key, string field, bool global = false)
    {
        await _database.HashDeleteAsync(GetKey(key, global), field);
    }
}
