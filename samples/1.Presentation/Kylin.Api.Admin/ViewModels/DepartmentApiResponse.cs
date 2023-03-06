//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: DepartmentResponse.cs
//摘要: 部门应答
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-04-26
//----------------------------------------------------------------

using iMaxSys.Max.Web.Mvc;
using iMaxSys.Max.Common.Enums;

namespace Kylin.Api.Admin.ViewModels;

/// <summary>
/// 部门应答
/// </summary>
public class DepartmentApiResponse : ApiResponse
{
    /// <summary>
	/// Id
	/// </summary>
	public long Id { get; set; }

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
    public int Level { get; set; }

    /// <summary>
    /// IsLeaf
    /// </summary>
    public bool IsRoot { get; set; }

    /// <summary>
    /// IsLeaf
    /// </summary>
    public bool IsLeaf { get; set; }

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
    /// Ext
    /// </summary>
    public string? Ext { get; set; }

    /// <summary>
    /// 是否可见
    /// </summary>
    public bool IsShow { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    public Status Status { get; set; }
}