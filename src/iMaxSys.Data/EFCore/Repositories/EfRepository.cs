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

    #region All

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <returns>The <see cref="IQueryable{TEntity}"/>.</returns>
    public override IQueryable<TEntity> All() => _dbSet;

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
    public IQueryable<TEntity> All(Expression<Func<TEntity, bool>>? predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                              bool disableTracking = false,
                                              bool ignoreQueryFilters = false)
    {
        return All(x => x, predicate, orderBy, include, disableTracking, ignoreQueryFilters);

    }

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
    public IQueryable<TResult> All<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool disableTracking = false,
        bool ignoreQueryFilters = false) where TResult : class
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (include != null)
        {
            query = include(query);
        }

        if (ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        if (orderBy != null)
        {
            return orderBy(query).Select(selector);
        }
        else
        {
            return query.Select(selector);
        }
    }

    #endregion

    #region AllAsync

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>The <see cref="IList{TEntity}"/>.</returns>
    public override async Task<IList<TEntity>> AllAsync(CancellationToken cancellationToken = default) => await _dbSet.ToListAsync(cancellationToken);

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>The <see cref="IQueryable{TEntity}"/>.</returns>
    public async Task<IList<TEntity>> AllAsync(bool disableTracking = false, CancellationToken cancellationToken = default) => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
    public async Task<IList<TEntity>> AllAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                              bool disableTracking = false,
                                              bool ignoreQueryFilters = false,
                                              CancellationToken cancellationToken = default)
    {
        return await AllAsync(x => x, predicate, orderBy, include, disableTracking, ignoreQueryFilters, cancellationToken);
    }

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
    public async Task<IList<TResult>> AllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool disableTracking = false,
        bool ignoreQueryFilters = false,
        CancellationToken cancellationToken = default) where TResult : class
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include != null)
        {
            query = include(query);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        if (orderBy != null)
        {
            return await orderBy(query).Select(selector).ToListAsync(cancellationToken);
        }
        else
        {
            return await query.Select(selector).ToListAsync(cancellationToken);
        }
    }

    #endregion

    #region GetPagedList

    /// <summary>
    /// Gets the <see cref="IPagedList{TEntity}"/> based on a predicate, orderby delegate and page information. This method default no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="pageIndex">The index of page.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    public IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>>? predicate = null,
                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                     Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                     int pageIndex = 0,
                                     int pageSize = 50,
                                     bool disableTracking = false,
                                     bool ignoreQueryFilters = false)
    {
        return GetPagedList(x => x, predicate, orderBy, include, pageSize, pageSize, disableTracking, ignoreQueryFilters);
    }

    /// <summary>
    /// Gets the <see cref="IPagedList{TResult}"/> based on a predicate, orderby delegate and page information. This method default no-tracking query.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="pageIndex">The index of page.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TResult}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    public IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector,
                                              Expression<Func<TEntity, bool>>? predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                              int pageIndex = 0,
                                              int pageSize = 50,
                                              bool disableTracking = false,
                                              bool ignoreQueryFilters = false) where TResult : class
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (include != null)
        {
            query = include(query);
        }

        if (ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        if (orderBy != null)
        {
            return orderBy(query).Select(selector).ToPagedList(pageIndex, pageSize);
        }
        else
        {
            return query.Select(selector).ToPagedList(pageIndex, pageSize);
        }
    }

    #endregion

    #region GetPagedListAsync

    /// <summary>
    /// Gets the <see cref="IPagedList{TEntity}"/> based on a predicate, orderby delegate and page information. This method default no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="pageIndex">The index of page.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    public async Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                                int pageIndex = 0,
                                                int pageSize = 50,
                                                bool disableTracking = false,
                                                bool ignoreQueryFilters = false,
                                                CancellationToken cancellationToken = default)
    {
        return await GetPagedListAsync(x => x, predicate, orderBy, include, pageIndex, pageSize, disableTracking, ignoreQueryFilters, cancellationToken);
    }

    /// <summary>
    /// Gets the <see cref="IPagedList{TResult}"/> based on a predicate, orderby delegate and page information. This method default no-tracking query.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="pageIndex">The index of page.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TResult}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    public async Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                         Expression<Func<TEntity, bool>>? predicate = null,
                                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                                         int pageIndex = 0,
                                                         int pageSize = 50,
                                                         bool disableTracking = false,
                                                         bool ignoreQueryFilters = false,
                                                         CancellationToken cancellationToken = default) where TResult : class
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (include != null)
        {
            query = include(query);
        }

        if (ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        if (orderBy != null)
        {
            return await orderBy(query).Select(selector).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }
        else
        {
            return await query.Select(selector).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }
    }

    #endregion

    #region FirstOrDefault

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method defaults to a read-only, no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="TEntity"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method defaults to a read-only, no-tracking query.</remarks>
    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null,
                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                              bool disableTracking = false,
                              bool ignoreQueryFilters = false)
    {
        return FirstOrDefault<TEntity>(x => x, predicate, orderBy, include, disableTracking, ignoreQueryFilters);
    }

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method defaults to a read-only, no-tracking query.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method defaults to a read-only, no-tracking query.</remarks>
    public TResult? FirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                       Expression<Func<TEntity, bool>>? predicate = null,
                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                       bool disableTracking = false,
                                       bool ignoreQueryFilters = false) where TResult : class
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (include != null)
        {
            query = include(query);
        }

        if (ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        if (orderBy != null)
        {
            return orderBy(query).Select(selector).FirstOrDefault();
        }
        else
        {
            return query.Select(selector).FirstOrDefault();
        }
    }

    #endregion

    #region FirstOrDefaultAsync

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method defaults to a read-only, no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>An <see cref="Task{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query. </remarks>
    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool disableTracking = false,
        bool ignoreQueryFilters = false,
        CancellationToken cancellationToken = default)
    {
        return await FirstOrDefaultAsync<TEntity>(x => x, predicate, orderBy, include, disableTracking, ignoreQueryFilters, cancellationToken);
    }

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method defaults to a read-only, no-tracking query.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>An <see cref="Task{TResult}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
    public async Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool disableTracking = false,
        bool ignoreQueryFilters = false,
        CancellationToken cancellationToken = default) where TResult : class
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (include != null)
        {
            query = include(query);
        }

        if (ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        if (orderBy != null)
        {
            return await orderBy(query).Select(selector).FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            return await query.Select(selector).FirstOrDefaultAsync(cancellationToken);
        }
    }

    #endregion

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

    /// <summary>
    /// Deletes the specified predicate.
    /// </summary>
    /// <param name="predicate"></param>
    public virtual void Delete(Expression<Func<TEntity, bool>> predicate) => Delete(_dbSet.Where(predicate));

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

    /// <summary>
    /// Soft deletes the entity by the specified predicate.
    /// </summary>
    /// <param name="predicate"></param>
    public virtual void Remove(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = _dbSet.Where(predicate);
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