//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: PhoneNumberResponse.cs
//摘要: 手机号应答
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-12-24
//----------------------------------------------------------------

using iMaxSys.Sns.Api.Response;

namespace iMaxSys.Sns.WeChat.Api.Response;

/// <summary>
/// 手机号应答
/// </summary>
public class PhoneNumberResponse
{
    /// <summary>
    /// 用户手机号信息
    /// </summary>
    [JsonPropertyName("phone_info")]
    public PhoneInfo PhoneInfo { get; set; } = new PhoneInfo();
}

/// <summary>
/// 用户手机号信息
/// </summary>
public class PhoneInfo
{
    /// <summary>
    /// 用户绑定的手机号（国外手机号会有区号）
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// 没有区号的手机号
    /// </summary>
    public string PurePhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// 区号
    /// </summary>
    public string CountryCode { get; set; } = string.Empty;

    /// <summary>
    /// 数据水印
    /// </summary>
    public Watermark Watermark { get; set; } = new();
}

/// <summary>
/// 数据水印
/// </summary>
public class Watermark
{
    /// <summary>
    /// 小程序appid
    /// </summary>
    public string Appid { get; set; } = string.Empty;

    /// <summary>
    /// 用户获取手机号操作的时间戳
    /// </summary>
    public int Timetamp { get; set; }
}