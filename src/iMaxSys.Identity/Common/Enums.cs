//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Enums.cs
//摘要: 身份结果枚举
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-11-15
//----------------------------------------------------------------

namespace iMaxSys.Identity.Common;

/// <summary>
/// 身份系统结果枚举
/// </summary>
public enum IdentityResultEnum
{
    /// <summary>
    /// token不可为空
    /// </summary>
    [Description("token不可为空")]
    TokenCantNull = 102000,

    /// <summary>
    /// 成员id不可为空
    /// </summary>
    [Description("成员id不可为空")]
    MemberIdCantNull = 102001,

    /// <summary>
    /// 角色不存在
    /// </summary>
    [Description("角色不存在")]
    RoleIsNotExist = 102100,
}