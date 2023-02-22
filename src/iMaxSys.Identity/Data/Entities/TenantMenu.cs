//----------------------------------------------------------------
//Copyright (C) 2016-2025 Care Co.,Ltd.
//All rights reserved.
//
//文件: TenantMenu.cs
//摘要: TenantMenu 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.Collection.Trees;
using iMaxSys.Data.Entities;

namespace iMaxSys.Identity.Data.Entities;

/// <summary>
/// Menu
/// </summary>
public class TenantMenu : TenantMasterEntity
{
    /// <summary>
    /// 菜单Id
    /// </summary>
    public long MenuId { get; set; }

    /// <summary>
    /// 是否可见
    /// </summary>
    public bool IsShow { get; set; } = true;

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; } = Status.Enable;

    /// <summary>
    /// Menu
    /// </summary>
    public virtual Menu Menu { get; set; } = new();
}