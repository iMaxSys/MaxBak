//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MenuModel.cs
//摘要: 菜单模型
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Collection.Trees;

namespace iMaxSys.Identity.Models;

/// <summary>
/// 菜单模型
/// </summary>
public class MenuResult : TreeNode
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
    public LinkedList<MenuResult>? Children { get; set; }

    /// <summary>
    /// Operations
    /// </summary>
    public List<OperationResult>? Operations { get; set; }
}