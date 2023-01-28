//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: LoginRequest.cs
//摘要: 登录请求
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-04-26
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using iMaxSys.Max.Web.Mvc;

namespace Kylin.Api.Admin.ViewModels;

/// <summary>
/// 登录请求
/// </summary>
public class LoginRequest : Request
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Display(Name = "用户名")]
    [Required(ErrorMessage = "{0}必须填写")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>
    [Display(Name = "密码")]
    [Required(ErrorMessage = "{0}必须填写")]
    public string Password { get; set; } = string.Empty;
}

