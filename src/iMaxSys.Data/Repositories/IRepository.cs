//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IRepository.cs
//摘要: IRepository 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Data.Entities;

namespace iMaxSys.Data.Repositories;

/// <summary>
/// Defines the interfaces for generic repository.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : Entity
{
    /// <summary>
    /// AutoCommit
    /// </summary>
    bool AutoCommit { get; set; }

    /// <summary>
    /// Add a new entity synchronously.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    TEntity Add(TEntity entity);

    /// <summary>
    /// Add a range of entities synchronously.
    /// </summary>
    /// <param name="entities">The entities to insert.</param>
    void Add(params TEntity[] entities);

    /// <summary>
    /// Add a range of entities synchronously.
    /// </summary>
    /// <param name="entities">The entities to insert.</param>
    void Add(IEnumerable<TEntity> entities);

    /// <summary>
    /// Add a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
    ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a range of entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to insert.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
    Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    TEntity Update(TEntity entity);

    /// <summary>
    /// Updates the specified entities.
    /// </summary>
    /// <param name="entities">The entities.</param>
    void Update(params TEntity[] entities);

    /// <summary>
    /// Updates the specified entities.
    /// </summary>
    /// <param name="entities">The entities.</param>
    void Update(IEnumerable<TEntity> entities);

    /// <summary>
    /// Deletes the entity by the specified primary key.
    /// </summary>
    /// <param name="id">The primary key value.</param>
    TEntity? Delete(object id);

    /// <summary>
    /// Deletes the specified entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    TEntity Delete(TEntity entity);

    /// <summary>
    /// Deletes the specified entities.
    /// </summary>
    /// <param name="entities">The entities.</param>
    void Delete(params TEntity[] entities);

    /// <summary>
    /// Deletes the specified entities.
    /// </summary>
    /// <param name="entities">The entities.</param>
    void Delete(IEnumerable<TEntity> entities);

    /// <summary>
    /// Soft deletes the entity by the specified primary key.
    /// </summary>
    /// <param name="id">The primary key value.</param>
    TEntity? Remove(object id);

    /// <summary>
    /// Soft deletes the specified entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    TEntity Remove(TEntity entity);

    /// <summary>
    /// Soft deletes the specified entities.
    /// </summary>
    /// <param name="entities">The entities.</param>
    void Remove(params TEntity[] entities);

    /// <summary>
    /// DeleSoft deletestes the specified entities.
    /// </summary>
    /// <param name="entities">The entities.</param>
    void Remove(IEnumerable<TEntity> entities);

    /// <summary>
    /// Change entity state for patch method on web api.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="state">The entity state.</param>
    void ChangeEntityState(TEntity entity, EntityState state);
}

/// <summary>
/// 仓储标识接口
/// </summary>
//public interface IRepository
//{
//}