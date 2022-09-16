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
    /// 无效的AccessSession
    /// </summary>
    [Description("无效的AccessSession")]
    AccessSessionIsNull = 106003,

    /// <summary>
    /// 成员id不可为空
    /// </summary>
    [Description("成员id不可为空")]
    MemberIdCantNull = 1062004,

    /// <summary>
    /// 成员不存在
    /// </summary>
    [Description("成员不存在")]
    MemberNotExists = 106005,

    /// <summary>
    /// 成员已存在
    /// </summary>
    [Description("成员已存在")]
    UserExists = 106006,

    /// <summary>
    /// 用户被禁用
    /// </summary>
    [Description("用户被禁用")]
    UserIsDisable = 106007,
    /// <summary>
    /// 用户已经过期
    /// </summary>
    [Description("用户已经过期")]
    UserIsExpired = 106008,

    /// <summary>
    /// 无效的XppSnsId
    /// </summary>
    [Description("无效的应用社交Id")]
    XppSnsIdNotExists = 106009,

    /// <summary>
    /// 用户名不可为空
    /// </summary>
    [Description("用户名不可为空")]
    UserNameCantNull = 106010,

    /// <summary>
    /// 密码不可为空
    /// </summary>
    [Description("密码不可为空")]
    PasswordCantNull = 106011,

    /// <summary>
    /// 密码强度弱
    /// </summary>
    [Description("密码强度弱")]
    PasswordIsWeak = 106012,

    /// <summary>
    /// 密码错误
    /// </summary>
    [Description("密码错误")]
    PasswordError = 106013,

    /// <summary>
    /// 无效的手机号码
    /// </summary>
    [Description("无效的手机号码")]
    MobileIsInvalid = 1060134,

    /// <summary>
    /// 代码和OpenId不可同时为空
    /// </summary>
    [Description("Code和OpenId不可同时为空")]
    CodeOpenIdCantNull = 106015,

    /// <summary>
    /// 获取手机号码失败
    /// </summary>
    [Description("获取手机号码失败")]
    GetMobileFail = 106016,

    /// <summary>
    /// 角色id不可为空
    /// </summary>
    [Description("角色id不可为空")]
    RoleIdCantNull = 106100,

    /// <summary>
    /// 角色已存在
    /// </summary>
    [Description("角色已存在")]
    RoleIsExists = 106104,

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
    /// 验证目标不可为空
    /// </summary>
    [Description("验证目标不可为空")]
    ToCantNull = 106307,
    /// <summary>
    /// 验证码不可为空
    /// </summary>
    [Description("验证码不可为空")]
    CheckCodeCantNull = 1063078,
    /// <summary>
    /// 验证码无效或不存在
    /// </summary>
    [Description("验证码失效或不存在")]
    CheckCodeNotExists = 106309,
    /// <summary>
    /// 验证码错误
    /// </summary>
    [Description("验证码错误")]
    CheckCodeError = 106310,
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
    /// <summary>
    /// 菜单不存在
    /// </summary>
    [Description("菜单不存在")]
    MenuNotExits = 106500,
}