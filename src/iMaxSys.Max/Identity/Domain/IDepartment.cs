//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IDepartment.cs
//摘要: IDepartment
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.Collection.Trees;

namespace iMaxSys.Max.Identity.Domain;

/// <summary>
/// 部门
/// </summary>
public interface IDepartment : ITreeNode
{
    //子部门
    public List<Department>? Children { get; set; }
}