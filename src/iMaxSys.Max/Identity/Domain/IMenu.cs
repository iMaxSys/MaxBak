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
using iMaxSys.Max.Collection.Trees;

namespace iMaxSys.Max.Identity.Domain;

/// <summary>
/// 菜单接口
/// </summary>
public interface IMenu : ITreeNode
{
    /// <summary>
    /// Action
    /// </summary>
    //public string? Action { get; set; }

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
    LinkedList<IMenu>? Children { get; set; }

    /// <summary>
    /// Operations
    /// </summary>
    List<IOperation>? Operations { get; set; }
}