
//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IRole.cs
//摘要: IRole
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-01-01
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max.Identity.Domain;

/// <summary>
/// IRole
/// </summary>
public interface IRole
{
    /// <summary>
    /// Id
    /// </summary>
    long Id { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Alias
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// Code
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// QuickCode
    /// </summary>
    public string QuickCode { get; set; }

    /// <summary>
    /// Descripton
    /// </summary>
    string? Descripton { get; set; }

    /// <summary>
    /// Icon
    /// </summary>
    string? Icon { get; set; }

    /// <summary>
    /// Style
    /// </summary>
    string? Style { get; set; }

    /// <summary>
    /// Menus("45675,45677")
    /// </summary>
    long[] MenuIds { get; set; }

    /// <summary>
    /// Operations("45675,45677")
    /// </summary>
    long[] OperationIds { get; set; }

    /// <summary>
    /// Start
    /// </summary>
    DateTime? Start { get; set; }

    /// <summary>
    /// End
    /// </summary>
    DateTime? End { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    Status Status { get; set; }

    /// <summary>
    /// Menu
    /// </summary>
    IMenu? Menu { get; set; }
}