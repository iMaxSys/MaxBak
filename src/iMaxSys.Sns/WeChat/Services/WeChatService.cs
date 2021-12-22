﻿//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: WeChatService.cs
//摘要: 微信服务
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

using System.Text.Json;
using System.Threading.Tasks;

using iMaxSys.Max.Security.Cryptography;

using iMaxSys.SDK.Sns.Api;
using iMaxSys.SDK.Sns.Domain.Auth;
using iMaxSys.SDK.Sns.Domain.Open;
using iMaxSys.SDK.Sns.WeChat.Api;
using iMaxSys.SDK.Sns.WeChat.Api.Request;
using iMaxSys.SDK.Sns.WeChat.Api.Response;
using iMaxSys.SDK.Sns.WeChat.Domain;
using iMaxSys.SDK.Sns.WeChat.Domain.Open;

namespace iMaxSys.SDK.Sns.WeChat.Services
{
    /// <summary>
    /// 微信服务
    /// </summary>
    public class WeChatService : IWeChatService
    {
        private readonly IWeChatClient _weChatClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="weChatClient"></param>
        public WeChatService(IWeChatClient weChatClient)
        {
            _weChatClient = weChatClient;
        }

        /// <summary>
        /// 获取访问配置
        /// </summary>
        /// <param name="requst"></param>
        /// <returns></returns>
        public async Task<AccessConfig> GetAccessConfigAsync(SnsAuth snsAuth)
        {
            AuthRequest authRequest = new AuthRequest
            {
                AppId = snsAuth.AppId,
                AppSecret = snsAuth.AppSecret,
                Code = snsAuth.Code,
                Method = "GET"
            };  
            AuthResponse response = await _weChatClient.ExecuteAsync<AuthResponse>(authRequest, WeChatResultCode.GetAccessConfigFail);
            return new AccessConfig
            {
                AppId = authRequest.AppId,
                AppSecret = authRequest.AppSecret,
                OpenId = response.OpenId,
                UnionId = response.UnionId,
                SessionKey = response.SessionKey
            };
        }

        /// <summary>
        /// 获取电话号码
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public SnsPhoneNumber GetPhoneNumber(string data, string key, string iv)
        {
            string json = AES.Decrypt(data, key, iv);
            return JsonSerializer.Deserialize<WeChatPhoneNumber>(json, new JsonSerializerOptions {  PropertyNameCaseInsensitive = true});
        }
    }
}