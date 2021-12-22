//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Enums.cs
//摘要: 枚举
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

namespace iMaxSys.Sns.Common;

/// <summary>
/// 平台
/// </summary>
public enum Platform
{
    /// <summary>
    /// 本体系
    /// </summary>
    Max = 0,
    /// <summary>
    /// 微信
    /// </summary>
    [Description("微信")]
    WeChat = 10,
    /// <summary>
    /// 支付宝
    /// </summary>
    [Description("支付宝")]
    AliPay = 20,
    /// <summary>
    /// 微博
    /// </summary>
    [Description("微博")]
    Weibo = 30,
    /// <summary>
    /// QQ
    /// </summary>
    [Description("QQ")]
    QQ = 40
}