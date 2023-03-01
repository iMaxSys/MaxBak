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
using iMaxSys.Max.Collection;
using Microsoft.EntityFrameworkCore;

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
	/// Gets the <see cref="IPagedList{TEntity}"/> based on a predicate, orderby delegate and page information. This method default no-tracking query.
	/// </summary>
	/// <param name="predicate">A function to test each element for a condition.</param>
	/// <param name="orderBy">A function to order elements.</param>
	/// <param name="include">A function to include navigation properties</param>
	/// <param name="pageIndex">The index of page.</param>
	/// <param name="pageSize">The size of the page.</param>
	/// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
	/// <param name="ignoreQueryFilters">Ignore query filters</param>
	/// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
	/// <remarks>This method default no-tracking query.</remarks>
	IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>>? predicate = null,
                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                     Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                     int pageIndex = 0,
                                     int pageSize = 50,
                                     bool disableTracking = false,
                                     bool ignoreQueryFilters = false);

    /// <summary>
    /// Gets the <see cref="IPagedList{TEntity}"/> based on a predicate, orderby delegate and page information. This method default no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="pageIndex">The index of page.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                                int pageIndex = 0,
                                                int pageSize = 50,
                                                bool disableTracking = false,
                                                bool ignoreQueryFilters = false,
                                                CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the <see cref="IPagedList{TResult}"/> based on a predicate, orderby delegate and page information. This method default no-tracking query.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="pageIndex">The index of page.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TResult}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector,
                                              Expression<Func<TEntity, bool>>? predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                              int pageIndex = 0,
                                              int pageSize = 50,
                                              bool disableTracking = false,
                                              bool ignoreQueryFilters = false) where TResult : class;

    /// <summary>
    /// Gets the <see cref="IPagedList{TEntity}"/> based on a predicate, orderby delegate and page information. This method default no-tracking query.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="pageIndex">The index of page.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                         Expression<Func<TEntity, bool>>? predicate = null,
                                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                                         int pageIndex = 0,
                                                         int pageSize = 50,
                                                         bool disableTracking = false,
                                                         bool ignoreQueryFilters = false,
                                                         CancellationToken cancellationToken = default) where TResult : class;

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method defaults to a read-only, no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method defaults to a read-only, no-tracking query.</remarks>
    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null,
                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                              bool disableTracking = false,
                              bool ignoreQueryFilters = false);

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method defaults to a read-only, no-tracking query.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method defaults to a read-only, no-tracking query.</remarks>
    TResult? FirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                       Expression<Func<TEntity, bool>>? predicate = null,
                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                       bool disableTracking = false,
                                       bool ignoreQueryFilters = false) where TResult : class;

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method defaults to a read-only, no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>An <see cref="Task{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query. </remarks>
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool disableTracking = false,
        bool ignoreQueryFilters = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method defaults to a read-only, no-tracking query.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>An <see cref="Task{TResult}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
    Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool disableTracking = false,
        bool ignoreQueryFilters = false,
        CancellationToken cancellationToken = default) where TResult : class;

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
    IQueryable<TEntity> All(Expression<Func<TEntity, bool>>? predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                              bool disableTracking = false,
                                              bool ignoreQueryFilters = false);

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
    IQueryable<TResult> All<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool disableTracking = false,
        bool ignoreQueryFilters = false) where TResult : class;

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>The <see cref="IQueryable{TEntity}"/>.</returns>
    Task<IList<TEntity>> AllAsync(bool disableTracking = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>An <see cref="IList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
    Task<IList<TEntity>> AllAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                              bool disableTracking = false,
                                              bool ignoreQueryFilters = false,
                                              CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>An <see cref="IList{TResult}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
    Task<IList<TResult>> AllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool disableTracking = false,
        bool ignoreQueryFilters = false,
        CancellationToken cancellationToken = default) where TResult : class;

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
    /// Update
    /// </summary>
    /// <param name="predicate">predicate</param>
    /// <param name="setPropertyCalls">set property calls</param>
    void Update(Expression<Func<TEntity, bool>> predicate, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls);

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="predicate">predicate</param>
    /// <param name="setPropertyCalls">set property calls</param>
    Task UpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls);


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
    /// Soft deletes the entity by the specified predicate.
    /// </summary>
    /// <param name="predicate"></param>
    void Delete(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Soft deletes the entity by the specified predicate.
    /// </summary>
    /// <param name="predicate"></param>
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

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
    /// Soft deletes the entity by the specified predicate.
    /// </summary>
    /// <param name="predicate"></param>
    void Remove(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Change entity state for patch method on web api.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="state">The entity state.</param>
    void ChangeEntityState(TEntity entity, EntityState state);
}