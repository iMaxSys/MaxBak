//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: ChangePasswordApiRequest.cs
//摘要: 修改密码请求
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
/// 修改密码请求
/// </summary>
public class ChangePasswordApiRequest : ApiRequest
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Display(Name = "旧密码")]
    [Required(ErrorMessage = "{0}必须填写")]
    public string OldPassword { get; set; } = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>
    [Display(Name = "新密码")]
    [Required(ErrorMessage = "{0}必须填写")]
    public string NewPassword { get; set; } = string.Empty;
}

