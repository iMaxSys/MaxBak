//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: TenantMasterEntity.cs
//摘要: TenantMasterEntity
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Data.Entities;

public abstract class TreeEntity : TenantMasterEntity
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// ParentId
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 索引/序号
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// IsLeaf
    /// </summary>
    public bool IsLeaf { get; set; } = true;

    /// <summary>
    /// Level
    /// </summary>
    public int Level { get; set; } = 0;

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
    /// 上级部门
    /// </summary>
    public virtual TreeEntity? Parent { get; set; }

    /// <summary>
    /// 下级部门
    /// </summary>
    public virtual ICollection<TreeEntity>? Children { get; set; }
}

public class MyTree : TreeEntity
{
    public override TreeEntity? Parent { get; set; }
}