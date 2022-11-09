//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: AuthRequest.cs
//摘要: 获取授权请求
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

namespace iMaxSys.Sns.WeChat.Api.Request;

/// <summary>
/// 获取授权请求
/// </summary>
public class AuthRequest : WeChatRequest
{
    /// <summary>
    /// AppId
    /// </summary>
    public string AppId { get; set; } = String.Empty;

    /// <summary>
    /// AppSecret
    /// </summary>
    public string AppSecret { get; set; } = String.Empty;

    /// <summary>
    /// AppId
    /// </summary>
    public string Code { get; set; } = String.Empty;

    /// <summary>
    /// 请求方法(GET or POST)
    /// </summary>
    public override HttpMethod Method { get; set; } = HttpMethod.Get;


    /// <summary>
    /// Action
    /// https://api.weixin.qq.com/sns/jscode2session?appid=<AppId>&secret=<AppSecret>&js_code=<code>&grant_type=authorization_code
    /// </summary>
    public override string Action => $"/sns/jscode2session?appid={AppId}&secret={AppSecret}&js_code={Code}&grant_type=authorization_code";
}