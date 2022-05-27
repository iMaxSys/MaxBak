//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ResultCode.cs
//摘要: 身份结果枚举
//说明: 106
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Identity.Common;

/// <summary>
/// 身份结果枚举
/// </summary>
public enum ResultCode
{
    /// <summary>
    /// 数据库连接串为空
    /// </summary>
    [Description("数据库连接串为空")]
    ConnectionIsNull = 106000,

    /// <summary>
    /// token不可为空
    /// </summary>
    [Description("token不可为空")]
    TokenCantNull = 106001,

    /// <summary>
    /// 成员id不可为空
    /// </summary>
    [Description("成员id不可为空")]
    MemberIdCantNull = 1062002,

    /// <summary>
    /// 成员不存在
    /// </summary>
    [Description("成员不存在")]
    MemberNotExists = 106003,

    /// <summary>
    /// 角色id不可为空
    /// </summary>
    [Description("角色id不可为空")]
    RoleIdCantNull = 106100,

    /// <summary>
    /// 角色不存在
    /// </summary>
    [Description("角色不存在")]
    RoleNotExists = 106101,
    /// <summary>
    /// 验证码请求过快
    /// </summary>
    [Description("验证码请求过快")]
    CheckCodeTimeLimit = 106301,
    /// <summary>
    /// 验证码不可为空
    /// </summary>
    [Description("验证码不可为空")]
    CheckCodeCantNull = 106302,
    /// <summary>
    /// 验证码无效或不存在
    /// </summary>
    [Description("验证码失效或不存在")]
    CheckCodeNotExists = 106303,
    /// <summary>
    /// 验证码错误
    /// </summary>
    [Description("验证码错误")]
    CheckCodeError = 106304,
}