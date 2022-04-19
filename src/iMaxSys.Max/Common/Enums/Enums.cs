//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Result.cs
//摘要: Result
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Common.Enums;

/// <summary>
/// 通用状态
/// </summary>
public enum Status
{
    /// <summary>
    /// 禁用
    /// </summary>
    [Description("禁用")]
    Disable = 0,

    /// <summary>
    /// 启用
    /// </summary>
    [Description("启用")]
    Enable = 1
}

/// <summary>
/// 性别 0男1女2未知
/// </summary>
public enum Gender
{
    /// <summary>
    /// 男
    /// </summary>
    [Description("男")]
    Male = 0,

    /// <summary>
    /// 女
    /// </summary>
    [Description("女")]
    Female = 1,

    /// <summary>
    /// 未知
    /// </summary>
    [Description("未知")]
    Unknown = 2,
}

/// <summary>
/// 应用来源
/// </summary>
public enum XppSource
{
    /// <summary>
    /// PCWeb
    /// </summary>
    [Description("PCWeb")]
    Web = 0,
    /// <summary>
    /// 移动端H5
    /// </summary>
    [Description("移动端H5")]
    H5 = 1,
    /// <summary>
    /// 微信小程序
    /// </summary>
    [Description("微信小程序")]
    WeChatLite = 10,
    /// <summary>
    /// 微信小程序
    /// </summary>
    [Description("微信公众号")]
    WeChatPub = 11,
    /// <summary>
    /// 支付宝小程序
    /// </summary>
    [Description("支付宝小程序")]
    AliPayLite = 20,
    /// <summary>
    /// Android
    /// </summary>
    [Description("Android")]
    Android = 80,
    /// <summary>
    /// iOS
    /// </summary>
    [Description("iOS")]
    iOS = 90,
}

/// <summary>
/// 账号/社交平台来源
/// </summary>
public enum PlatformSource
{
    /// <summary>
    /// 本体系
    /// </summary>
    [Description("Max")]
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

/// <summary>
/// 业务来源类型
/// </summary>
public enum BizSource
{
    /// <summary>
    /// 注册
    /// </summary>
    [Description("注册")]
    BindCheckCode = 1000,
    /// <summary>
    /// 解绑
    /// </summary>
    [Description("解绑")]
    UnbindCheckCode = 1001,
    /// <summary>
    /// 找回密码
    /// </summary>
    [Description("找回密码")]
    FindCheckCode = 1002,
}

/// <summary>
/// 占用时间
/// </summary>
public enum TakeTime
{
    /// <summary>
    /// 无
    /// </summary>
    [Description("无")]
    None = 0,
    /// <summary>
    /// 上午
    /// </summary>
    [Description("上午")]
    AM = 1,

    /// <summary>
    /// 下午
    /// </summary>
    [Description("下午")]
    PM = 2,

    /// <summary>
    /// 全天
    /// </summary>
    [Description("全天")]
    AllDay = 3
}

public enum MaritalStatus
{
    /// <summary>
    /// 未婚
    /// </summary>
    [Description("未婚")]
    Single = 0,

    /// <summary>
    /// 已婚
    /// </summary>
    [Description("已婚")]
    Married = 1,

    /// <summary>
    /// 离婚
    /// </summary>
    [Description("离婚")]
    Divorced = 2,

    /// <summary>
    /// 丧偶
    /// </summary>
    [Description("丧偶")]
    Widowed = 3
}
