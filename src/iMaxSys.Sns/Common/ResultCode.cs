//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ResultCode.cs
//摘要: 社交系统结果枚举
//说明: 107
//
//当前：1.0
//作者：陶剑扬
//日期：2019-05-26
//----------------------------------------------------------------

namespace iMaxSys.Sns.Common;

public enum ResultCode
{
    /// <summary>
    /// 微信返回为空
    /// </summary>
    [Description("微信返回为空")]
    WechatResponseIsNull = 200000,

    /// <summary>
    /// 微信返回错误结果
    /// </summary>
    [Description("微信返回错误结果")]
    WechatResponseIsError = 200001,
}