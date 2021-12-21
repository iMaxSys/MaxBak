//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Authority.cs
//摘要: Authority
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-01-01
//----------------------------------------------------------------

namespace iMaxSys.Max.Identity.Domain;

/// <summary>
/// Authority
/// </summary>
public class Authority : IAuthority
{
    /// <summary>
    /// Menus
    /// </summary>
    public IMenu? Menu { get; set; }

    /// <summary>
    /// Roles
    /// </summary>
    public IList<IRole>? Roles { get; set; }
}