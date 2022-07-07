//----------------------------------------------------------------
//Copyright (C) 2016-2025 Care Co.,Ltd.
//All rights reserved.
//
//文件: RoleMenu.cs
//摘要: RoleMenu 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;
using iMaxSys.Data.Entities;

namespace iMaxSys.Identity.Data.Entities;

/// <summary>
/// RoleMenu
/// </summary>
public class RoleMenu : TenantMasterEntity
{
    /// <summary>
    /// RoleId
    /// </summary>
    public long RoleId { get; set; }

    /// <summary>
    /// MenuId
    /// </summary>
    public long MenuId { get; set; }

    /// <summary>
    /// XppId
    /// </summary>
    public long XppId { get; set; }

    /// <summary>
    /// MemberId
    /// </summary>
    public Status Status { get; set; } = Status.Enable;

    /// <summary>
    /// Roles
    /// </summary>
    public Role Role { get; set; } = new();

    /// <summary>
    /// Menus
    /// </summary>
    public Menu Menu { get; set; } = new();
}