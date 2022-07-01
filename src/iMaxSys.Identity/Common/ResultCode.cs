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
    /// 请求Headers中无token
    /// </summary>
    [Description("需要提供Token")]
    NeedToken = 106001,

    /// <summary>
    /// token不可为空
    /// </summary>
    [Description("token不可为空")]
    TokenCantNull = 106002,

    /// <summary>
    /// 成员id不可为空
    /// </summary>
    [Description("成员id不可为空")]
    MemberIdCantNull = 1062003,

    /// <summary>
    /// 成员不存在
    /// </summary>
    [Description("成员不存在")]
    MemberNotExists = 106004,

    /// <summary>
    /// 角色id不可为空
    /// </summary>
    [Description("角色id不可为空")]
    RoleIdCantNull = 106100,

    /// <summary>
    /// 角色不存在
    /// </summary>
    [Description("角色不存在")]
    RoleNotExists = 106105,
    /// <summary>
    /// 验证码请求过快
    /// </summary>
    [Description("验证码请求过快")]
    CheckCodeTimeLimit = 106306,
    /// <summary>
    /// 验证码不可为空
    /// </summary>
    [Description("验证码不可为空")]
    CheckCodeCantNull = 106307,
    /// <summary>
    /// 验证码无效或不存在
    /// </summary>
    [Description("验证码失效或不存在")]
    CheckCodeNotExists = 106308,
    /// <summary>
    /// 验证码错误
    /// </summary>
    [Description("验证码错误")]
    CheckCodeError = 106309,
    /// <summary>
    /// 无效的父级部门
    /// </summary>
    [Description("无效的父级部门")]
    ParentDepartmentIsInvalid = 106400,
    /// <summary>
    /// 无效的部门
    /// </summary>
    [Description("无效的部门")]
    DepartmentIsInvalid = 106401,
    /// <summary>
    /// 该部门下还有下属部门
    /// </summary>
    [Description("该部门下还有下属部门")]
    HasChildren = 106402,
    /// <summary>
    /// 该部门下还有成员
    /// </summary>
    [Description("该部门下还有成员")]
    HasMember = 106403,
}