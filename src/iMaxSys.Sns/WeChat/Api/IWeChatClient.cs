//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IWeChatClient.cs
//摘要: IWeChatClient
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

using iMaxSys.Sns.WeChat.Common;
using iMaxSys.Sns.WeChat.Api.Request;

namespace iMaxSys.Sns.WeChat.Api;

/// <summary>
/// IWeChatClient
/// </summary>
public interface IWeChatClient : IDependency
{
    /// <summary>
    /// ExecuteAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<T?> ExecuteAsync<T>(WeChatRequest request);

    /// <summary>
    /// ExecuteAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request"></param>
    /// <param name="weChatResultCode"></param>
    /// <returns></returns>
    Task<T?> ExecuteAsync<T>(WeChatRequest request, WeChatResultCode weChatResultCode);
}