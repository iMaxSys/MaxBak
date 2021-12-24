//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: HttpService.cs
//摘要: HttpService
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2020-05-01
//----------------------------------------------------------------

using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Json.NamingPolicy;

namespace iMaxSys.Max.Net.Http;

/// <summary>
/// HttpService
/// </summary>
public class HttpService : IHttpService
{
    private readonly IHttpClientFactory _httpClientFactory;

    //静态for性能
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.Create(allowedRanges: UnicodeRanges.All),
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private static readonly JsonSerializerOptions _snakeJsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.Create(allowedRanges: UnicodeRanges.All),
        PropertyNamingPolicy = new SnakeCaseNamingPolicy()
    };

    public HttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<string> GetAsync(string url)
    {
        return await GetAsync(url, null);
    }

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<string> GetAsync(string url, Dictionary<string, string>? data)
    {
        string uri = String.Empty;
        if (data != null && data.Count > 0)
        {
            string query = string.Join("&", data.Select(x => $"{x.Key}={x.Value}"));
            uri = $"{url}?{query}";
        }
        else
        {
            uri = url;
        }

        using var httpClient = _httpClientFactory.CreateClient();
        return await httpClient.GetStringAsync(uri);
    }

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<T?> GetAsync<T>(string url)
    {
        return await GetAsync<T>(url, null, false);
    }

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="isSnakeFormatter"></param>
    /// <returns></returns>
    public async Task<T?> GetAsync<T>(string url, bool isSnakeFormatter)
    {
        return await GetAsync<T>(url, null, isSnakeFormatter);
    }

    /// <summary>
    /// GetAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<T?> GetAsync<T>(string url, Dictionary<string, string> data)
    {
        return await GetAsync<T>(url, data, false);
    }


    /// <summary>
    /// GetAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="isSnakeFormatter"></param>
    /// <returns></returns>
    public async Task<T?> GetAsync<T>(string url, Dictionary<string, string>? data, bool isSnakeFormatter)
    {
        string result = await GetAsync(url, data);
        return JsonSerializer.Deserialize<T>(result, isSnakeFormatter ? _snakeJsonSerializerOptions : _jsonSerializerOptions);
    }

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<string> PostAsync(string url)
    {
        return await PostAsync(url, null, null);
    }

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<string> PostAsync(string url, Dictionary<string, string> data)
    {
        return await PostAsync(url, data, null);
    }

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <param name="url"></param>
    /// <param name="credentials"></param>
    /// <returns></returns>
    public async Task<string> PostAsync(string url, string credentials)
    {
        return await PostAsync(url, null, credentials);
    }

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="credentials"></param>
    /// <returns></returns>
    public async Task<string> PostAsync(string url, Dictionary<string, string>? data, string? credentials)
    {
        string result = string.Empty;
        using var httpClient = _httpClientFactory.CreateClient();
        if (string.IsNullOrWhiteSpace(credentials))
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }

        StringContent content = data != null ? new(string.Join("&", data.Select(x => $"{x.Key}={x.Value}"))) : new StringContent("");
        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        var response = await httpClient.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadAsStringAsync();
        }
        else
        {
            throw new MaxException(response.StatusCode);
        }

        return result;
    }

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<T?> PostAsync<T>(string url)
    {
        return await PostAsync<T>(url, null, null, false);
    }

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="isSnakeFormatter"></param>
    /// <returns></returns>
    public async Task<T?> PostAsync<T>(string url, bool isSnakeFormatter)
    {
        return await PostAsync<T>(url, null, null, isSnakeFormatter);
    }

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="credentials"></param>
    /// <param name="isSnakeFormatter"></param>
    /// <returns></returns>
    public async Task<T?> PostAsync<T>(string url, string credentials, bool isSnakeFormatter)
    {
        return await PostAsync<T>(url, null, credentials, isSnakeFormatter);
    }

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<T?> PostAsync<T>(string url, Dictionary<string, string> data)
    {
        return await PostAsync<T>(url, data, null, false);
    }

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="credentials"></param>
    /// <returns></returns>
    public async Task<T?> PostAsync<T>(string url, Dictionary<string, string> data, string credentials)
    {
        return await PostAsync<T>(url, data, credentials, false);
    }

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="isSnakeFormatter"></param>
    /// <returns></returns>
    public async Task<T?> PostAsync<T>(string url, Dictionary<string, string> data, bool isSnakeFormatter)
    {
        return await PostAsync<T>(url, data, null, isSnakeFormatter);
    }

    /// <summary>
    /// PostAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="credentials"></param>
    /// <param name="isSnakeFormatter"></param>
    /// <returns></returns>
    public async Task<T?> PostAsync<T>(string url, Dictionary<string, string>? data, string? credentials, bool isSnakeFormatter)
    {
        string? result = await PostAsync(url, data, credentials);
        return JsonSerializer.Deserialize<T>(result, isSnakeFormatter ? _snakeJsonSerializerOptions : _jsonSerializerOptions);
    }

    /// <summary>
    /// PostJson
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="json"></param>
    /// <returns></returns>
    public async Task<T?> PostJsonAsync<T>(string url, string json)
    {
        return await PostJsonAsync<T>(url, json, false);
    }

    /// <summary>
    /// PostJson
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="json"></param>
    /// <param name="isSnakeFormatter"></param>
    /// <returns></returns>
    public async Task<T?> PostJsonAsync<T>(string url, string json, bool isSnakeFormatter)
    {
        using var httpClient = _httpClientFactory.CreateClient();
        StringContent content = new(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(url, content);
        Stream result;

        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadAsStreamAsync();
        }
        else
        {
            throw new MaxException(response.StatusCode);
        }

        return await JsonSerializer.DeserializeAsync<T>(result, isSnakeFormatter ? _snakeJsonSerializerOptions : _jsonSerializerOptions);
    }

    /// <summary>
    /// PostXml
    /// </summary>
    /// <param name="url"></param>
    /// <param name="xml"></param>
    /// <returns></returns>
    public async Task<string> PostXmlAsync(string url, string xml)
    {
        using var httpClient = _httpClientFactory.CreateClient();
        StringContent content = new(xml, Encoding.UTF8, "application/xml");

        var response = await httpClient.PostAsync(url, content);

        string result;
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadAsStringAsync();
        }
        else
        {
            throw new MaxException(response.StatusCode);
        }

        return result;
    }
}