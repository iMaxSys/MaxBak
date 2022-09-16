//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: WeChatResultCode.cs
//摘要: 社交系统之微信结果代码枚举
//说明: 1031
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

namespace iMaxSys.Sns.WeChat.Common;

/// <summary>
/// 微信结果代码
/// </summary>
public enum WeChatResultCode
{
    /// <summary>
    /// 访问微信接口服务异常
    /// </summary>
    [Description("访问微信接口服务异常")]
    AccessWeChatFail = 103100,

    /// <summary>
    /// 获取微信访问配置异常
    /// </summary>
    [Description("获取微信访问配置异常")]
    GetAccessConfigFail = 103101,

    /// <summary>
    /// 获取微信用户手机号码异常
    /// </summary>
    [Description("获取微信访问配置异常")]
    GetWeChatPhoneNumberError = 103102
}