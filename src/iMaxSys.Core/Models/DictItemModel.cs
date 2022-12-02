//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: DictItemModel.cs
//摘要: DictItemModel 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-29
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Core.Models;

public class DictItemModel
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// DictId
    /// </summary>
    public long DictId { get; set; }

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
    /// Logo
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
    /// 状态
    /// </summary>
    public Status Status { get; set; } = Status.Enable;
}

