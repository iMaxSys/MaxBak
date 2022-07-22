//----------------------------------------------------------------
//Copyright (C) 2016-2025 Care Co.,Ltd.
//All rights reserved.
//
//文件: Menu.cs
//摘要: Menu 
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
public class Menu : TenantMasterEntity, ITreeNode
{
    /// <summary>
    /// XppId
    /// </summary>
    public long XppId { get; set; }

    /// <summary>
    /// ParentId
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 左值
    /// </summary>
    public int Lv { get; set; }

    /// <summary>
    /// 右值
    /// </summary>
    public int Rv { get; set; }

    /// <summary>
    /// 索引/序号
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Level
    /// </summary>
    public int Level { get; set; } = 0;

    /// <summary>
    /// IsLeaf
    /// </summary>
    public bool IsRoot { get; set; } = false;

    /// <summary>
    /// IsLeaf
    /// </summary>
    public bool IsLeaf { get; set; } = true;

    /// <summary>
    /// Type
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// Code
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// QuickCode
    /// </summary>
    public string? QuickCode { get; set; }

    /// <summary>
    /// Value
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// 别名
    /// </summary>
    public string? Alias { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Style
    /// </summary>
    public string? Style { get; set; }

    /// <summary>
    /// SelectedStyle
    /// </summary>
    public string? SelectedStyle { get; set; }

    /// <summary>
    /// Icon
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// SelectedIcon
    /// </summary>
    public string? SelectedIcon { get; set; }

    /// <summary>
    /// Action
    /// </summary>
    public string? Action { get; set; }

    /// <summary>
    /// Ext
    /// </summary>
    public string? Ext { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; } = Status.Enable;

    /// <summary>
    /// 是否可见
    /// </summary>
    public bool IsShow { get; set; }

    /// <summary>
    /// 上级部门
    /// </summary>
    public virtual Menu? Parent { get; set; }

    /// <summary>
    /// 下级部门
    /// </summary>
    public virtual List<Menu>? Menus { get; set; }

    /// <summary>
    /// Operations
    /// </summary>
    public virtual List<Operation>? Operations { get; set; }
}