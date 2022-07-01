//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ITreeStore.cs
//摘要: 树形存储接口 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-05-31
//----------------------------------------------------------------

using iMaxSys.Max.Collection.Trees;

namespace iMaxSys.Max.Collection;

/// <summary>
/// 树形存储接口
/// </summary>
public interface ITreeStore : IDependency
{
    /// <summary>
    /// 新增节点
    /// </summary>
    /// <param name="treeNode"></param>
    /// <returns></returns>
    Task<LrTreeNode> AddAsync(LrTreeNode treeNode);

    /// <summary>
    /// 移除节点
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveAsync(long id);

    /// <summary>
    /// 移除节点
    /// </summary>
    /// <param name="treeNode"></param>
    /// <returns></returns>
    Task RemoveAsync(LrTreeNode treeNode);

    /// <summary>
    /// 移动节点
    /// </summary>
    /// <param name="at">index</param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task MoveAsync(long at, long id);

    /// <summary>
    /// 移动节点
    /// </summary>
    /// <param name="at">index</param>
    /// <param name="treeNode"></param>
    /// <returns></returns>
    Task MoveAsync(long at, LrTreeNode treeNode);
}