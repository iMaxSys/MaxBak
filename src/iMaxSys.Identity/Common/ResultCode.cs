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
    /// token不可为空
    /// </summary>
    [Description("token不可为空")]
    TokenCantNull = 106000,

    /// <summary>
    /// 成员id不可为空
    /// </summary>
    [Description("成员id不可为空")]
    MemberIdCantNull = 1062001,

    /// <summary>
    /// 成员不存在
    /// </summary>
    [Description("成员不存在")]
    MemberNotExists = 106002,

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
}