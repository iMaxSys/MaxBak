//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IMenu.cs
//摘要: IMenu
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-01-01
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max.Identity.Domain;

public interface IMenu
{
    /// <summary>
    /// Id
    /// </summary>
    long Id { get; set; }

    /// <summary>
    /// Code
    /// </summary>
    string? Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// Descripton
    /// </summary>
    string? Description { get; set; }

    /// <summary>
    /// Icon
    /// </summary>
    string? Icon { get; set; }

    /// <summary>
    /// Style
    /// </summary>
    string? Style { get; set; }

    /// <summary>
    /// Router
    /// </summary>
    string? Router { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    Status Status { get; set; }

    /// <summary>
    /// Menus
    /// </summary>
    List<IMenu>? Menus { get; set; }

    /// <summary>
    /// Operations
    /// </summary>
    List<IOperation>? Operations { get; set; }
}