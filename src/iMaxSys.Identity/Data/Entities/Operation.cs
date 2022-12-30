//----------------------------------------------------------------
//Copyright (C) 2016-2025 Care Co.,Ltd.
//All rights reserved.
//
//文件: Operation.cs
//摘要: 操作
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Common;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Data.Entities;

namespace iMaxSys.Identity.Data.Entities;

/// <summary>
/// Operation
/// </summary>
public class Operation : TenantMasterEntity
{
    /// <summary>
    /// XppId
    /// </summary>
    public long XppId { get; set; }

    /// <summary>
    /// MenuId
    /// </summary>
    public long MenuId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 别名
    /// </summary>
    public string Alias { get; set; } = string.Empty;

    /// <summary>
    /// Code
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// QuickCode
    /// </summary>
    public string QuickCode { get; set; } = string.Empty;

    /// <summary>
    /// Descripton
    /// </summary>
    public string? Descripton { get; set; }

    /// <summary>
    /// Value
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// Icon
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Style
    /// </summary>
    public string? Style { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 服务器端路由
    /// </summary>
    public string? ServerRouter { get; set; }

    /// <summary>
    /// 客户端路由
    /// </summary>
    public string? ClientRouter { get; set; }

    /// <summary>
    /// 是否可见
    /// </summary>
    public bool IsShow { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Menu
    /// </summary>
    public virtual Menu Menu { get; set; } = new();
}