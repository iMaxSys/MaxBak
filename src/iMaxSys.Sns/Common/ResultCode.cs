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
    /// 用户名不可为空
    /// </summary>
    [Description("用户名不可为空")]
    UserNameCantNull = 107000,

    /// <summary>
    /// 密码强度弱
    /// </summary>
    [Description("密码强度弱")]
    PasswordIsWeak = 107001,

    /// <summary>
    /// 无效的手机号码
    /// </summary>
    [Description("无效的手机号码")]
    MobileIsInvalid = 107002,
}