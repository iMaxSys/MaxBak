//----------------------------------------------------------------
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

using iMaxSys.Max.Security.Cryptography;
using iMaxSys.Sns.Api;
using iMaxSys.Sns.Common.Auth;
using iMaxSys.Sns.Common.Open;
using iMaxSys.Sns.WeChat.Api;
using iMaxSys.Sns.WeChat.Api.Request;
using iMaxSys.Sns.WeChat.Api.Response;
using iMaxSys.Sns.WeChat.Common;
using iMaxSys.Sns.WeChat.Common.Open;

namespace iMaxSys.Sns.WeChat;

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
    public async Task<AccessConfig> LoginAsync(SnsAuth snsAuth)
    {
        AuthRequest authRequest = new(snsAuth.AppId, snsAuth.AppSecret, snsAuth.Code);
        AuthResponse? response = await _weChatClient.ExecuteAsync<AuthResponse>(authRequest, WeChatResultCode.GetAccessConfigFail);

        if (response is not null)
        {
            return new AccessConfig
            {
                AppId = authRequest.AppId,
                AppSecret = authRequest.AppSecret,
                OpenId = response.OpenId,
                UnionId = response.UnionId,
                SessionKey = response.SessionKey
            };
        }
        else
        {
            throw new MaxException(WeChatResultCode.GetAccessConfigFail);
        }
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
        var phoneNumber = JsonSerializer.Deserialize<WeChatPhoneNumber>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (phoneNumber is null)
        {
            throw new MaxException(WeChatResultCode.GetWeChatPhoneNumberError, json);
        }

        return phoneNumber;
    }
}