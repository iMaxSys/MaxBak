//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: PasswordLoginModel.cs
//摘要: 密码登录请求
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2020-05-01
//----------------------------------------------------------------


namespace iMaxSys.Identity.Models;

/// <summary>
/// 密码登录请求
/// </summary>
public class PasswordLoginModel
{
    /// <summary>
    /// SID
    /// </summary>
    public long SID { get; set; }

    /// <summary>
    /// UserName
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// IP
    /// </summary>
    public string IP { get; set; } = string.Empty;
}

