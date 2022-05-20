//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IUnitOfWork.cs
//摘要: IUnitOfWork 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Data.Entities;
using iMaxSys.Data.Repositories;

namespace iMaxSys.Data;

/// <summary>
/// 工作单元接口
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// 提交
    /// </summary>
    /// <returns></returns>
    int SaveChanges();

    /// <summary>
    /// 提交
    /// </summary>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 多工作单元提交
    /// </summary>
    /// <param name="unitOfWorks">其他工作单元</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(IUnitOfWork[] unitOfWorks, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取范型仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;

    /// <summary>
    /// 获取只读范型仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    IReadOnlyRepository<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : Entity;

    /// <summary>
    /// 获取定制仓储
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    /// <returns></returns>
    TRepository GetCustomRepository<TRepository>() where TRepository : IRepositoryBase;
}

/// <summary>
/// 读写分离IUnitOfWork接口
/// </summary>
/// <typeparam name="T">读写类型</typeparam>
/// <typeparam name="K">只读类型</typeparam>
public interface IUnitOfWork<T, K> : IUnitOfWork where T : DbContext where K : DbContext
{
    /// <summary>
    /// DbContext
    /// </summary>
    T DbContext { get; }

    /// <summary>
    /// ReadOnlyDbContext
    /// </summary>
    K ReadOnlyDbContext { get; }
}

/// <summary>
/// 读写不分离IUnitOfWork接口
/// </summary>
/// <typeparam name="T">读写类型</typeparam>
public interface IUnitOfWork<T> : IUnitOfWork<T, T> where T : DbContext
{
}