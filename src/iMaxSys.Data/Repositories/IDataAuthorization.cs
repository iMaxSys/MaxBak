//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IDataAuthorization.cs
//摘要: 数据权限 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-05-07
//----------------------------------------------------------------

using iMaxSys.Data.Entities;

namespace iMaxSys.Data.Repositories;

/// <summary>
/// 数据权限
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IDataAuthorization<TEntity> where TEntity : Entity
{
    /// <summary>
    /// 过滤数据
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    IQueryable<TEntity> AllByCurrentUser(IQueryable<TEntity> query);

    /// <summary>
    /// 过滤数据
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<IQueryable<TEntity>> AllByCurrentUserAsync(IQueryable<TEntity> query);

    /// <summary>
    /// FirstOrDefaultByCurrentUser
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    TEntity FirstOrDefaultByCurrentUser(IQueryable<TEntity> query);

    /// <summary>
    /// FirstOrDefaultByCurrentUser
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<TEntity> FirstOrDefaultByCurrentUserAsync(IQueryable<TEntity> query);
}

