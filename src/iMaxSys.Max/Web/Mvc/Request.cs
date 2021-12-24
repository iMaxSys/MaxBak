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

namespace iMaxSys.Max.Web.Mvc;

public interface IRequestBase
{
}

/// <summary>
/// 请求抽象类
/// </summary>
public abstract class Request : IRequestBase
{
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
    public virtual string Method { get; set; } = "POST";

    /// <summary>
    /// API接口名称
    /// </summary>
    public abstract string Action { get; }

    /// <summary>
    /// 数据格式
    /// </summary>
    public string Format { get; set; } = "json";

    /// <summary>
    /// 编码格式
    /// </summary>
    public string Charset { get; set; } = "utf-8";

    /// <summary>
    /// Url
    /// </summary>
    public virtual string Url { get; set; } = String.Empty;

    /// <summary>
    /// API访问需要携带的参数，具体参见每个API的详细文档
    /// </summary>
    protected Dictionary<string, string> Params = new();

    /// <summary>
    /// 构建参数字典
    /// </summary>
    /// <returns></returns>
    public virtual Request Build()
    {
        return this;
    }
}