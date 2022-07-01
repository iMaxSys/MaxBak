//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: EfRepository.cs
//摘要: EfRepository 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-12-01
//----------------------------------------------------------------

using iMaxSys.Max.Collection;
using iMaxSys.Data.Entities;
using iMaxSys.Data.Repositories;

namespace iMaxSys.Data.EFCore.Repositories;

/// <summary>
/// EfRepository
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class EfRepository<TEntity> : EfReadOnlyRepository<TEntity>, IRepository<TEntity> where TEntity : Entity
{
    /// <summary>
    /// AutoCommit
    /// </summary>
    public virtual bool AutoCommit { get; set; } = false;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="dbContext"></param>
    public EfRepository(EfDbContext dbContext) : base(dbContext)
    {
    }

    #region Add

    /// <summary>
    /// Add a new entity synchronously.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    /// <returns>entity</returns>
    public virtual TEntity Add(TEntity entity) => _dbSet.Add(entity).Entity;

    /// <summary>
    /// Add a range of entities synchronously.
    /// </summary>
    /// <param name="entities">The entities to insert.</param>
    public virtual void Add(params TEntity[] entities) => _dbSet.AddRange(entities);

    /// <summary>
    /// Add a range of entities synchronously.
    /// </summary>
    /// <param name="entities">The entities to insert.</param>
    public virtual void Add(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);

    /// <summary>
    /// Add a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
    public virtual ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default) => _dbSet.AddAsync(entity, cancellationToken);

    /// <summary>
    /// Add a range of entities asynchronously.
    /// </summary>
    /// <param name="entities">The entities to insert.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
    public virtual Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) => _dbSet.AddRangeAsync(entities, cancellationToken);

    #endregion

    #region Update

    /// <summary>
    /// Updates the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public virtual TEntity Update(TEntity entity) => _dbSet.Update(entity).Entity;

    /// <summary>
    /// Updates the specified entities.
    /// </summary>
    /// <param name="entities">The entities.</param>
    public virtual void Update(params TEntity[] entities) => _dbSet.UpdateRange(entities);

    /// <summary>
    /// Updates the specified entities.
    /// </summary>
    /// <param name="entities">The entities.</param>
    public virtual void Update(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);

    #endregion

    #region Delete

    /// <summary>
    /// Deletes the entity by the specified primary key.
    /// </summary>
    /// <param name="id">The primary key value.</param>
    public virtual TEntity? Delete(object id)
    {
        var entity = _dbSet.Find(id);
        return entity != null ? Delete(entity) : null;
    }

    /// <summary>
    /// Deletes the specified entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    public virtual TEntity Delete(TEntity entity) => _dbSet.Remove(entity).Entity;

    /// <summary>
    /// Deletes the specified entities.
    /// </summary>
    /// <param name="entities">The entities.</param>
    public virtual void Delete(params TEntity[] entities) => _dbSet.RemoveRange(entities);

    /// <summary>
    /// Deletes the specified entities.
    /// </summary>
    /// <param name="entities">The entities.</param>
    public virtual void Delete(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

    #endregion

    #region Remove

    /// <summary>
    /// Soft deletes the entity by the specified primary key.
    /// </summary>
    /// <param name="id">The primary key value.</param>
    public virtual TEntity? Remove(object id)
    {
        var entity = _dbSet.Find(id);
        return entity != null ? Remove(entity) : null;
    }

    /// <summary>
    /// Soft deletes the specified entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    public virtual TEntity Remove(TEntity entity)
    {
        entity.IsDeleted = true;
        return entity;
    }

    /// <summary>
    /// Soft deletes the specified entities.
    /// </summary>
    /// <param name="entities">The entities.</param>
    public virtual void Remove(params TEntity[] entities)
    {
        entities.ForEach(x => x.IsDeleted = true);
    }

    /// <summary>
    /// Soft deletestes the specified entities.
    /// </summary>
    /// <param name="entities">The entities.</param>
    public virtual void Remove(IEnumerable<TEntity> entities)
    {
        entities.ForEach(x => x.IsDeleted = true);
    }

    #endregion


    #region ChangeEntityState

    /// <summary>
    /// Change entity state for patch method on web api.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// /// <param name="state">The entity state.</param>
    public void ChangeEntityState(TEntity entity, EntityState state)
    {
        _dbContext.Entry(entity).State = state;
    }

    #endregion

    /// <summary>
    /// Executes the specified raw SQL command.
    /// </summary>
    /// <param name="sql">The raw SQL.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>The number of state entities written to database.</returns>
    protected int ExecuteSql(string sql, params object[] parameters) => _dbContext.Database.ExecuteSqlRaw(sql, parameters);

}