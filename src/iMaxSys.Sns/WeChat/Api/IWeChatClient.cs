﻿//----------------------------------------------------------------
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

using System.Threading.Tasks;

using iMaxSys.Max.DependencyInjection;
using iMaxSys.SDK.Sns.WeChat.Domain;
using iMaxSys.SDK.Sns.WeChat.Api.Request;

namespace iMaxSys.SDK.Sns.WeChat.Api
{
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
        Task<T> ExecuteAsync<T>(WeChatRequest request, WeChatResultCode code);
    }
}