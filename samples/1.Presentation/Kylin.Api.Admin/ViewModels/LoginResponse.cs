//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: LoginResponse.cs
//摘要: 登录应答
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-04-26
//----------------------------------------------------------------

using iMaxSys.Max.Web.Mvc;

namespace Kylin.Api.Admin.ViewModels;

/// <summary>
/// 登录应答
/// </summary>
public class LoginResponse : Response
{
    /// <summary>
    /// 令牌
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// 过期时间
    /// </summary>
    public string Expires { get; set; } = string.Empty;

    /// <summary>
    /// 信息
    /// </summary>
    public MemberResponse? Member { get; set; }
}