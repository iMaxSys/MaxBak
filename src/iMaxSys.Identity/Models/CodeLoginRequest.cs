//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: CodeLoginRequest.cs
//摘要: 代码登录请求
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2020-05-01
//----------------------------------------------------------------

using iMaxSys.Max.Common;
using iMaxSys.Max.Common.Domain;

namespace iMaxSys.Identity.Models;

/// <summary>
/// 代码登录请求
/// </summary>
public class CodeLoginRequest : DomainRequest
{
    /// <summary>
    /// sid
    /// </summary>
    public long SID { get; set; }

    /// <summary>
    /// Code
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 用户类型
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// IP
    /// </summary>
    public string IP { get; set; } = Const.DEFAULT_IP;
}