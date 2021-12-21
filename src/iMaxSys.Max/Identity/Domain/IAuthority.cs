//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IAuthority.cs
//摘要: IAuthority
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-01-01
//----------------------------------------------------------------

namespace iMaxSys.Max.Identity.Domain;

/// <summary>
/// IAuthority
/// </summary>
public interface IAuthority
{
    /// <summary>
    /// Menus
    /// </summary>
    IMenu? Menu { get; set; }

    /// <summary>
    /// Roles
    /// </summary>
    IList<IRole>? Roles { get; set; }
}