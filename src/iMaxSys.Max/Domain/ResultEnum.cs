//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ResultEnum.cs
//摘要: ResultEnum
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using System.ComponentModel;

namespace iMaxSys.Max.Domain
{
    /// <summary>
    /// ResultEnum
    /// </summary>
    public enum ResultEnum
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 0,

        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Fail = 999999,

        /// <summary>
        /// 未授权
        /// </summary>
        [Description("未授权")]
        Unauthorized = 401,

        /// <summary>
        /// 禁止访问
        /// </summary>
        [Description("禁止访问")]
        Forbidden = 403,

        /// <summary>
        /// 未找到
        /// </summary>
        [Description("未找到")]
        NotFound = 404,

        /// <summary>
        /// 请求Headers中无token
        /// </summary>
        [Description("需要提供Token")]
        NeedToken = 1000,
        /// <summary>
        /// token为空不可设置Session
        /// </summary>
        [Description("token为空不可设置Session")]
        CantSetSession = 1001,
        /// <summary>
        /// 请求数据为空
        /// </summary>
        [Description("请求数据为空")]
        RequestIsEmpty = 1002,
        /// <summary>
        /// 用户未登录或登录已失效
        /// </summary>
        [Description("用户未登录或登录已失效")]
        UnLogin = 1003,
        /// <summary>
        /// 用户已存在
        /// </summary>
        [Description("用户已存在")]
        UserExists = 1004,
        /// <summary>
        /// 注册需要手机号码
        /// </summary>
        [Description("注册需要手机号码")]
        NeedMobile = 1005,
        /// <summary>
        /// 注册需要手机号码
        /// </summary>
        [Description("用户名不可为空")]
        UserNameCantNull = 1006,
        /// <summary>
        /// 注册需要手机号码
        /// </summary>
        [Description("密码不可为空")]
        PasswordCantNull = 1007,
        /// <summary>
        /// 代码和OpenId不可同时为空
        /// </summary>
        [Description("代码和OpenId不可同时为空")]
        CodeOpenIdCantNull = 1008,
        /// <summary>
        /// 用户不存在或者密码错误
        /// </summary>
        [Description("用户不存在")]
        UserNotExsits = 1009,
        /// <summary>
        /// 要更新的成员Id不可为空
        /// </summary>
        [Description("要更新的成员Id不可为空")]
        UserIdCantNull = 1010,
        /// <summary>
        /// 用户不存在或者密码错误
        /// </summary>
        [Description("密码错误")]
        PasswordError = 1011,
        /// <summary>
        /// 用户被禁用
        /// </summary>
        [Description("用户被禁用")]
        UserIsDisable = 1012,
        /// <summary>
        /// 用户已经过期
        /// </summary>
        [Description("用户已经过期")]
        UserIsExpired = 1013,
        /// <summary>
        /// 角色之下存在用户
        /// </summary>
        [Description("角色之下存在用户")]
        RoleHasMember = 1020,
        /// <summary>
        /// 该社交账号已绑定
        /// </summary>
        [Description("该社交账号已绑定")]
        SnsIsBind = 1021,
        /// <summary>
        /// 该手机号码已绑定
        /// </summary>
        [Description("该手机号码已绑定")]
        MobileIsBind = 1022,
        /// <summary>
        /// 验证码请求过快
        /// </summary>
        [Description("验证码请求过快")]
        CheckCodeTimeLimit = 1023,
        /// <summary>
        /// 验证码不可为空
        /// </summary>
        [Description("验证码不可为空")]
        CheckCodeCantNull = 1024,
        /// <summary>
        /// 验证码无效或不存在
        /// </summary>
        [Description("验证码失效或不存在")]
        CheckCodeNotExists = 1025,
        /// <summary>
        /// 验证码错误
        /// </summary>
        [Description("验证码错误")]
        CheckCodeError = 1026,
        /// <summary>
        /// 菜单下还存在子菜单
        /// </summary>
        [Description("菜单下还存在子菜单")]
        MenuHasSub = 1030,
    }
}
