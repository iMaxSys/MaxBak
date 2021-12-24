//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IHttpService.cs
//摘要: IHttpService
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2020-05-01
//----------------------------------------------------------------

using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Max.Net.Http;

/// <summary>
/// IHttpService
/// </summary>
public interface IHttpService : ISingleton
{
    /// <summary>
    /// GetAsync
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    Task<string> GetAsync(string url);

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<string> GetAsync(string url, Dictionary<string, string> data);

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <returns></returns>
    Task<T?> GetAsync<T>(string url);

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="isSnakeFormatter"></param>
    /// <returns></returns>
    Task<T?> GetAsync<T>(string url, bool isSnakeFormatter);

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<T?> GetAsync<T>(string url, Dictionary<string, string> data);

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="isSnakeFormatter"></param>
    /// <returns></returns>
    Task<T?> GetAsync<T>(string url, Dictionary<string, string> data, bool isSnakeFormatter);

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    Task<string> PostAsync(string url);

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<string> PostAsync(string url, Dictionary<string, string> data);

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <param name="url"></param>
    /// <param name="credentials"></param>
    /// <returns></returns>
    Task<string> PostAsync(string url, string credentials);

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="credentials"></param>
    /// <returns></returns>
    Task<string> PostAsync(string url, Dictionary<string, string> data, string credentials);

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <returns></returns>
    Task<T?> PostAsync<T>(string url);

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="isSnakeFormatter"></param>
    /// <returns></returns>
    Task<T?> PostAsync<T>(string url, bool isSnakeFormatter);

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="credentials"></param>
    /// <param name="isSnakeFormatter"></param>
    /// <returns></returns>
    Task<T?> PostAsync<T>(string url, string credentials, bool isSnakeFormatter);

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<T?> PostAsync<T>(string url, Dictionary<string, string> data);

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="credentials"></param>
    /// <returns></returns>
    Task<T?> PostAsync<T>(string url, Dictionary<string, string> data, string credentials);

    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="isSnakeFormatter"></param>
    /// <returns></returns>
    Task<T?> PostAsync<T>(string url, Dictionary<string, string> data, bool isSnakeFormatter);

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="credentials"></param>
    /// <param name="isSnakeFormatter"></param>
    /// <returns></returns>
    Task<T?> PostAsync<T>(string url, Dictionary<string, string> data, string credentials, bool isSnakeFormatter);

    /// <summary>
    /// PostJson
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="json"></param>
    /// <returns></returns>
    Task<T?> PostJsonAsync<T>(string url, string json);

    /// <summary>
    /// PostJson
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="json"></param>
    /// <param name="isSnakeFormatter"></param>
    /// <returns></returns>
    Task<T?> PostJsonAsync<T>(string url, string json, bool isSnakeFormatter);

    /// <summary>
    /// PostXml
    /// </summary>
    /// <param name="url"></param>
    /// <param name="xml"></param>
    /// <returns></returns>
    Task<string> PostXmlAsync(string url, string xml);
}