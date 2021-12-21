//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IAccessToken.cs
//摘要: IAccessToken
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
public interface IAccessToken
{
    /// <summary>
    /// 令牌
    /// </summary>
    string? Token { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    DateTime Expires { get; set; }
}