//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: LoginResponse.cs
//摘要: 代码登录应答
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2020-05-01
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;

namespace iMaxSys.Identity.Models.Response;

/// <summary>
/// 登录应答
/// </summary>
public class LoginResponse : iMaxSys.Max.Web.Mvc.Response
{
    /// <summary>
    /// 令牌
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime Expires { get; set; }

    /// <summary>
    /// 成员信息
    /// </summary>
    public IMember? Member { get; set; }
}