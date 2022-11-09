//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: 请求抽象类.cs
//摘要: Request
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-10-12
//----------------------------------------------------------------

namespace iMaxSys.Max.Net.Http;

/// <summary>
/// 请求类
/// </summary>
public class HttpRequest
{
    /// <summary>
    /// url
    /// </summary>
    private string _url = string.Empty;

    /// <summary>
    /// 是否使用证书
    /// </summary>
    public bool UseCert { get; set; } = false;

    /// <summary>
    /// 证书/证书路径
    /// </summary>
    public string? Cert { get; set; }

    /// <summary>
    /// 请求方法(GET or POST)
    /// </summary>
    public virtual HttpMethod Method { get; set; } = HttpMethod.Get;

    /// <summary>
    /// ContentType
    /// </summary>
    public virtual string ContentType { get; set; } = string.Empty;

    /// <summary>
    /// 编码格式
    /// </summary>
    public virtual string Charset { get; set; } = "utf-8";

    /// <summary>
    /// Url
    /// </summary>
    public virtual string Url { get => _url; set => _url = value; }

    /// <summary>
    /// response's format is snake
    /// </summary>
    public virtual bool IsSnakeFormat { get; set; } = false;

    /// <summary>
    /// Header
    /// </summary>
    public Dictionary<string, string>? Header { get; set; }

    /// <summary>
    /// Body
    /// </summary>
    public Dictionary<string, string>? Body { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    public string? Data { get; set; }

    /// <summary>
    /// 构建参数字典
    /// </summary>
    /// <returns></returns>
    public virtual HttpRequest Build()
    {
        return this;
    }
}