//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: EfBaseRepostory.cs
//摘要: EfBaseRepostory 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-12-01
//----------------------------------------------------------------

using iMaxSys.Max.Collection;
using iMaxSys.Data.Entities;
using iMaxSys.Data.DbContexts;
using iMaxSys.Data.Repositories;

namespace iMaxSys.Data.EFCore.Repositories;

/// <summary>
/// EF Core只读范型仓储
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class EfRepostoryBase<TEntity> : IRepositoryBase where TEntity : Entity
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Code
    /// </summary>
    public int Code => _dbContext.GetType().GetHashCode();

    /// <summary>
    /// ConnectionString
    /// </summary>
    public string? ConnectionString => _dbContext.Database.GetConnectionString();

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="dbContext"></param>
    public EfRepostoryBase(EfReadOnlyDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    #region ChangeTable

    /// <summary>
    /// Changes the table name. This require the tables in the same database.
    /// </summary>
    /// <param name="table"></param>
    /// <remarks>
    /// This only been used for supporting multiple tables in the same model. This require the tables in the same database.
    /// </remarks>
    public virtual void ChangeTable(string table)
    {
        if (_dbContext.Model.FindEntityType(typeof(TEntity)) is IConventionEntityType relational)
        {
            relational.SetTableName(table);
        }
    }

    #endregion

    #region All

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <returns>The <see cref="IQueryable{TEntity}"/>.</returns>
    public IQueryable<TEntity> All() => _dbSet;

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>true</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
    public IQueryable<TEntity> All(Expression<Func<TEntity, bool>>? predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                              bool disableTracking = true,
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
    /// <param name="disableTracking"><c>true</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
    public IQueryable<TResult> All<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool disableTracking = true,
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
    public async Task<IList<TEntity>> AllAsync(CancellationToken cancellationToken = default) => await _dbSet.ToListAsync(cancellationToken);

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>true</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
    public async Task<IList<TEntity>> AllAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                              bool disableTracking = true,
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
    /// <param name="disableTracking"><c>true</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
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
        bool disableTracking = true,
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
    /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    public IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>>? predicate = null,
                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                     Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                     int pageIndex = 0,
                                     int pageSize = 50,
                                     bool disableTracking = true,
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
    /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TResult}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    public IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector,
                                              Expression<Func<TEntity, bool>>? predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                              int pageIndex = 0,
                                              int pageSize = 50,
                                              bool disableTracking = true,
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
    /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
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
                                                bool disableTracking = true,
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
    /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
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
                                                         bool disableTracking = true,
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
    /// <param name="disableTracking"><c>true</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="TEntity"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method defaults to a read-only, no-tracking query.</remarks>
    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null,
                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                              bool disableTracking = true,
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
    /// <param name="disableTracking"><c>true</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method defaults to a read-only, no-tracking query.</remarks>
    public TResult? FirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                       Expression<Func<TEntity, bool>>? predicate = null,
                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                       bool disableTracking = true,
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
    /// <param name="disableTracking"><c>true</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>An <see cref="Task{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query. </remarks>
    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool disableTracking = true,
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
    /// <param name="disableTracking"><c>true</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
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
        bool disableTracking = true,
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

    #region Find

    /// <summary>
    /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
    /// </summary>
    /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
    /// <returns>The found entity or null.</returns>
    public virtual TEntity? Find(params object[] keyValues) => _dbSet.Find(keyValues);

    /// <summary>
    /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
    /// </summary>
    /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
    /// <returns>A <see cref="Task{TEntity}" /> that represents the asynchronous insert operation.</returns>
    public async virtual ValueTask<TEntity?> FindAsync(params object[] keyValues) => await _dbSet.FindAsync(keyValues);

    /// <summary>
    /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
    /// </summary>
    /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task{TEntity}"/> that represents the asynchronous find operation. The task result contains the found entity or null.</returns>
    public async virtual ValueTask<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken) => await _dbSet.FindAsync(keyValues, cancellationToken);

    #endregion

    #region Count

    /// <summary>
    /// Gets the count based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public int Count(Expression<Func<TEntity, bool>>? predicate = null) => predicate == null ? _dbSet.Count() : _dbSet.Count(predicate);

    /// <summary>
    /// Gets async the count based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns></returns>
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default) => predicate == null ? await _dbSet.CountAsync(cancellationToken) : await _dbSet.CountAsync(predicate, cancellationToken);

    #endregion

    #region LongCount

    /// <summary>
    /// Gets the long count based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public long LongCount(Expression<Func<TEntity, bool>>? predicate = null) => predicate == null ? _dbSet.LongCount() : _dbSet.Count(predicate);

    /// <summary>
    /// Gets async the long count based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns></returns>
    public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default) => predicate == null ? await _dbSet.LongCountAsync(cancellationToken) : await _dbSet.LongCountAsync(predicate, cancellationToken);

    #endregion

    #region Max

    /// <summary>
    /// Gets the max based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns>decimal</returns>
    public TEntity? Max(Expression<Func<TEntity, bool>>? predicate = null) => predicate == null ? _dbSet.Max() : _dbSet.Where(predicate).Max();

    /// <summary>
    /// Gets the max based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual T? Max<T>(Expression<Func<TEntity, T>> selector) => _dbSet.Max(selector);

    /// <summary>
    /// Gets the max based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual T? Max<T>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, T>> selector) => _dbSet.Where(predicate).Max(selector);

    /// <summary>
    /// Gets the max based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    public async Task<TEntity?> MaxAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default) => predicate == null ? await _dbSet.MaxAsync(cancellationToken) : await _dbSet.Where(predicate).MaxAsync(cancellationToken);

    /// <summary>
    /// Gets the max based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    public async Task<T?> MaxAsync<T>(Expression<Func<TEntity, T>> selector, CancellationToken cancellationToken = default) => await _dbSet.MaxAsync(selector, cancellationToken);

    /// <summary>
    /// Gets the max based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    public async Task<T?> MaxAsync<T>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, T>> selector, CancellationToken cancellationToken = default) => await _dbSet.Where(predicate).MaxAsync(selector, cancellationToken);

    #endregion

    #region Min

    /// <summary>
    /// Gets the min based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns>decimal</returns>
    public TEntity? Min(Expression<Func<TEntity, bool>>? predicate = null) => predicate == null ? _dbSet.Min() : _dbSet.Where(predicate).Min();

    /// <summary>
    /// Gets the min based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual T? Min<T>(Expression<Func<TEntity, T>> selector) => _dbSet.Min(selector);

    /// <summary>
    /// Gets the min based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual T? Min<T>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, T>> selector) => _dbSet.Where(predicate).Min(selector);

    /// <summary>
    /// Gets the min based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    public async Task<TEntity?> MinAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default) => predicate == null ? await _dbSet.MinAsync(cancellationToken) : await _dbSet.Where(predicate).MinAsync(cancellationToken);

    /// <summary>
    /// Gets the min based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    public async Task<T?> MinAsync<T>(Expression<Func<TEntity, T>> selector, CancellationToken cancellationToken = default) => await _dbSet.MinAsync(selector, cancellationToken);

    /// <summary>
    /// Gets the min based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    public async Task<T?> MinAsync<T>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, T>> selector, CancellationToken cancellationToken = default) => await _dbSet.Where(predicate).MinAsync(selector, cancellationToken);

    #endregion

    #region Average

    /// <summary>
    /// Gets the average based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual decimal Average(Expression<Func<TEntity, decimal>> selector) => _dbSet.Average(selector);

    /// <summary>
    /// Gets the average based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual decimal Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> selector) => _dbSet.Where(predicate).Average(selector);

    /// <summary>
    /// Gets the average based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual async Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector, CancellationToken cancellationToken = default) => await _dbSet.AverageAsync(selector, cancellationToken);

    /// <summary>
    /// Gets the average based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual async Task<decimal> AverageAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> selector, CancellationToken cancellationToken = default) => await _dbSet.Where(predicate).AverageAsync(selector, cancellationToken);

    #endregion

    #region Sum

    /// <summary>
    /// Gets the sum based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual decimal Sum(Expression<Func<TEntity, decimal>> selector) => _dbSet.Sum(selector);

    /// <summary>
    /// Gets the sum based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual decimal Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> selector) => _dbSet.Where(predicate).Sum(selector);

    /// <summary>
    /// Gets the sum based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual async Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector, CancellationToken cancellationToken = default) => await _dbSet.SumAsync(selector, cancellationToken);

    /// <summary>
    /// Gets the sum based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    public virtual async Task<decimal> SumAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> selector, CancellationToken cancellationToken = default) => await _dbSet.Where(predicate).SumAsync(selector, cancellationToken);

    #endregion

    #region Any

    /// <summary>
    /// Gets the exists based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public bool Any(Expression<Func<TEntity, bool>>? predicate = null) => predicate == null ? _dbSet.Any() : _dbSet.Any(predicate);

    /// <summary>
    /// Gets the Async Exists record based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns></returns>
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default) => predicate == null ? await _dbSet.AnyAsync(cancellationToken) : await _dbSet.AnyAsync(predicate, cancellationToken);

    #endregion

    #region FromSql

    /// <summary>
    /// Uses raw SQL queries to fetch the specified <typeparamref name="TEntity" /> data.
    /// </summary>
    /// <param name="sql">The raw SQL.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>An <see cref="IQueryable{TEntity}" /> that contains elements that satisfy the condition specified by raw SQL.</returns>
    public virtual IQueryable<TEntity> FromSql(string sql, params object[] parameters) => _dbSet.FromSqlRaw(sql, parameters);

    #endregion

}