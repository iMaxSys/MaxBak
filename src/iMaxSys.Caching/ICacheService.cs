//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ICache.cs
//摘要: ICache
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Caching;

/// <summary>
/// ICache
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// 获取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    T Get<T>(string key, bool global = false);

    /// <summary>
    /// 获取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    Task<T> GetAsync<T>(string key, bool global = false);

    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="global"></param>
    void Set(string key, object value, bool global = false);

    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    Task SetAsync(string key, object value, bool global = false);

    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expire"></param>
    /// <param name="global"></param>
    void Set(string key, object value, DateTime? expire, bool global = false);

    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expire"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    Task SetAsync(string key, object value, DateTime? expire, bool global = false);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="key"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    bool Delete(string key, bool global = false);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="key"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(string key, bool global = false);

    /// <summary>
    /// 设置Hash集合
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ht"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    Task HashSetAsync(string key, Hashtable ht, bool global = false);

    /// <summary>
    /// 设置Hash值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    Task HashSetAsync(string key, string field, string value, bool global = false);

    /// <summary>
    /// 获取Hash集合
    /// </summary>
    /// <param name="key"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    Task<Hashtable> HashGetAllAsync(string key, bool global = false);

    /// <summary>
    /// 获取Hash值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="field"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    Task<string> HashGetAsync(string key, string field, bool global = false);

    /// <summary>
    /// 删除Hash Key
    /// </summary>
    /// <param name="key"></param>
    /// <param name="field"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    Task HashDeleteAsync(string key, string field, bool global = false);
}
