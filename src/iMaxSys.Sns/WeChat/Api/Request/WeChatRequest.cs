//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: WeChatRequest.cs
//摘要: 微信请求
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

using iMaxSys.Sns.Api.Reqeust;

namespace iMaxSys.Sns.WeChat.Api.Request;

/// <summary>
/// 微信请求
/// </summary>
public abstract class WeChatRequest : SnsRequest
{
    /// <summary>
    /// BaseAPIUrl
    /// </summary>
    public const string BASEAPIURL = "https://api.weixin.qq.com";

    /// <summary>
    /// Action
    /// </summary>
    public abstract string Action { get; }

    /// <summary>
    /// ContentType
    /// </summary>
    public override string? ContentType { get; set; } = "application/json";

    /// <summary>
    /// response's format is snake
    /// </summary>
    public override bool IsSnakeFormat { get; set; } = false;

    /// <summary>
    /// Url
    /// </summary>
    public override string Url
    {
        get
        {
            return $"{BASEAPIURL}{Action}";
        }
    }
}