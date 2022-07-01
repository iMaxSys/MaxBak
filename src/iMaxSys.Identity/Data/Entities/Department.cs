//----------------------------------------------------------------
//Copyright (C) 2016-2025 Care Co.,Ltd.
//All rights reserved.
//
//文件: Department.cs
//摘要: Department 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-15
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.Collection.Trees;
using iMaxSys.Data.Entities;

namespace iMaxSys.Identity.Data.Entities;

/// <summary>
/// Department
/// </summary>
public class Department : TenantMasterEntity, ITreeNode
{
    /// <summary>
    /// ParentId
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// XppId
    /// </summary>
    public long XppId { get; set; }

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
    /// Data
    /// </summary>
    public string? Data { get; set; }

    /// <summary>
    /// Ext
    /// </summary>
    public string? Ext { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; } = Status.Enable;

    /// <summary>
    /// 上级部门
    /// </summary>
    public virtual Department? Parent { get; set; }

    /// <summary>
    /// 下级部门
    /// </summary>
    public virtual ICollection<Department>? Departments { get; set; }

    /// <summary>
    /// 成员
    /// </summary>
    public virtual ICollection<Member>? Members { get; set; }
}