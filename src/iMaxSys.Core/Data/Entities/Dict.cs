//----------------------------------------------------------------
//Copyright (C) 2016-2025 Care Co.,Ltd.
//All rights reserved.
//
//文件: Dictionary.cs
//摘要: 字典目录 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Data.Entities;
using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Core.Data.Entities;

/// <summary>
/// Dictionary
/// </summary>
public class Dict : TenantMasterEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 别名
    /// </summary>
    public string Alias { get; set; } = string.Empty;

    /// <summary>
    /// 编号
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 速查码
    /// </summary>
    public string QuickCode { get; set; } = string.Empty;

    /// <summary>
    /// 数据类型(0:string,1:int,2:decimal,3:datetime)
    /// </summary>
    public int DataType { get; set; }

    /// <summary>
    /// value
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 缩略图
    /// </summary>
    public string? Thumbnail { get; set; }

    /// <summary>
    /// 图像
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// style
    /// </summary>
    public string? Style { get; set; }

    /// <summary>
    /// 序号
    /// </summary>
    public int Ordinal { get; set; }

    /// <summary>
    /// 可编辑
    /// </summary>
    public bool Editable { get; set; }

    /// <summary>
    /// XppId
    /// </summary>
    public long XppId { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; } = Status.Enable;

    /// <summary>
    /// 项目
    /// </summary>
    public virtual ICollection<DictItem>? DictItems { get; set; }
}