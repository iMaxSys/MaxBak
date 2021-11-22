//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: HttpExtensions.cs
//摘要: HttpExtensions
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Net.Http;

public static class HttpExtensions
{
    /// <summary>
    /// 读取Json反序列化对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <returns></returns>
    public static T? ReadAsJson<T>(this HttpContent content)
    {
        return JsonSerializer.Deserialize<T>(content.ReadAsStream());
    }

    /// <summary>
    /// 异步读取Json反序列化对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <returns></returns>
    public static async Task<T?> ReadAsJsonAsync<T>(this HttpContent content)
    {
        return await JsonSerializer.DeserializeAsync<T>(await content.ReadAsStreamAsync());
    }
}