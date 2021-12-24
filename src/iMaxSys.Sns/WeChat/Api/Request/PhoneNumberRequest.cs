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
public class PhoneNumberRequest : WeChatRequest
{
    /// <summary>
    /// 手机号获取凭证
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 接口调用凭证
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Action
    /// https://api.weixin.qq.com/wxa/business/getuserphonenumber?access_token=ACCESS_TOKEN
    /// </summary>
    public override string Action => $"/wxa/business/getuserphonenumber?access_token={AccessToken}";

    /// <summary>
    /// Build
    /// </summary>
    /// <param name="dict"></param>
    public override PhoneNumberRequest Build()
    {
        Params.Add("code", Code);
        return this;
    }
}

