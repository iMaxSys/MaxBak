//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: AccessToken.cs
//摘要: AccessToken
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-01-01
//----------------------------------------------------------------

namespace iMaxSys.Max.Identity.Domain;

/// <summary>
/// 访问令牌
/// </summary>
public class AccessToken : IAccessToken
{
    /// <summary>
    /// 令牌
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// 过期时间(分钟)
    /// </summary>
    public DateTime Expires { get; set; }
}