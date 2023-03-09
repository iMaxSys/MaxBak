//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MenuIdsResult.cs
//摘要: 菜单ids模型
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Collection.Trees;
using iMaxSys.Max.Common.Domain;

namespace iMaxSys.Identity.Models;

/// <summary>
/// 菜单ids模型
/// </summary>
public class MenuIdsResult : DomainResult
{
    /// <summary>
    /// MenuIds
    /// </summary>
    public long[]? MenuIds { get; set; }

    /// <summary>
    /// operationIds
    /// </summary>
    public long[]? OperationIds { get; set; }
}