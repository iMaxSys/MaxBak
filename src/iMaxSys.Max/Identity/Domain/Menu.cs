﻿//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Menu.cs
//摘要: Menu
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-01-01
//----------------------------------------------------------------

using iMaxSys.Max.Collection.Trees;
using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max.Identity.Domain;

/// <summary>
/// Menu
/// 此处最终
/// </summary>
public class Menu : TreeNode, IMenu
{
    /// <summary>
    /// 服务器端路由
    /// </summary>
    public string? ServerRouter { get; set; }

    /// <summary>
    /// 客户端路由
    /// </summary>
    public string? ClientRouter { get; set; }

    /// <summary>
    /// Menus
    /// </summary>
    public LinkedList<IMenu>? Children { get; set; }

    /// <summary>
    /// Operations
    /// </summary>
    public List<IOperation>? Operations { get; set; }
}

/*
/// <summary>
/// MenuShadow
/// </summary>
public class MenuShadow
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Code
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Icon
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Style
    /// </summary>
    public string? Style { get; set; }

    /// <summary>
    /// Router
    /// </summary>
    public string? Router { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Menus
    /// </summary>
    public List<MenuShadow>? Menus { get; set; }

    /// <summary>
    /// Operations
    /// </summary>
    public List<Operation>? Operations { get; set; }
}
*/