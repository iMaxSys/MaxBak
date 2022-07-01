﻿//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ITreeService.cs
//摘要: ITreeService 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-29
//----------------------------------------------------------------

using iMaxSys.Max.Collection.Trees;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Data.Entities;

namespace iMaxSys.Data.Services;

/// <summary>
/// ITreeService
/// </summary>
public interface ITreeService<T, M, R> where T : Entity, ITreeNode, new() where M : ITreeNode
{
    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="includeChildren"></param>
    /// <returns></returns>
    Task<ITree<T>?> GetAsync(long tenantId, bool includeChildren);

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="includeChildren"></param>
    /// <returns></returns>
    Task<ITree<T>?> GetAsync(long tenantId, long xppId, bool includeChildren);

    /// <summary>
    /// 插入
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="targetId"></param>
    /// <param name="model"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    Task InsertAsync(long tenantId, long targetId, M model, NodePosition position);

    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="targetId"></param>
    /// <param name="currentId"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    Task MoveAsync(long tenantId, long targetId, long currentId, NodePosition position);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="parentId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<M> AddAsync(long tenantId, long? parentId, M model);

    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="parentId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task RemoveAsync(long tenantId, long currentId);
}