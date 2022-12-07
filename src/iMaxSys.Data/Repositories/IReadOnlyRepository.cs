//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IReadOnlyRepository.cs
//摘要: 只读仓储接口 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-05-07
//----------------------------------------------------------------

using iMaxSys.Data.Entities;
using iMaxSys.Max.Collection;

namespace iMaxSys.Data.Repositories;

/// <summary>
/// 只读仓储接口
/// </summary>
public interface IReadOnlyRepository<TEntity> : IRepositoryBase where TEntity : Entity
{
    /// <summary>
    /// Code
    /// </summary>
    int Code { get; }

    /// <summary>
    /// Changes the table name. This require the tables in the same database.
    /// </summary>
    /// <param name="table"></param>
    /// <remarks>
    /// This only been used for supporting multiple tables in the same model. This require the tables in the same database.
    /// </remarks>
    void ChangeTable(string table);

    /// <summary>
    /// Gets the <see cref="IPagedList{TEntity}"/> based on a predicate, orderby delegate and page information. This method default no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="pageIndex">The index of page.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>>? predicate = null,
                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                     Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                     int pageIndex = 0,
                                     int pageSize = 50,
                                     bool ignoreQueryFilters = false);

    /// <summary>
    /// Gets the <see cref="IPagedList{TEntity}"/> based on a predicate, orderby delegate and page information. This method default no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="pageIndex">The index of page.</param>
    /// <param name="pageSize">The size of the page.</param>
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
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TResult}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector,
                                              Expression<Func<TEntity, bool>>? predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                              int pageIndex = 0,
                                              int pageSize = 50,
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
                                                         bool ignoreQueryFilters = false,
                                                         CancellationToken cancellationToken = default) where TResult : class;

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method defaults to a read-only, no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method defaults to a read-only, no-tracking query.</remarks>
    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>>? predicate = null,
                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                              bool ignoreQueryFilters = false);

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method defaults to a read-only, no-tracking query.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method defaults to a read-only, no-tracking query.</remarks>
    TResult? FirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                       Expression<Func<TEntity, bool>>? predicate = null,
                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                       bool ignoreQueryFilters = false) where TResult : class;

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method defaults to a read-only, no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>An <see cref="Task{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query. </remarks>
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool ignoreQueryFilters = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method defaults to a read-only, no-tracking query.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
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
        bool ignoreQueryFilters = false,
        CancellationToken cancellationToken = default) where TResult : class;


    /// <summary>
    /// Uses raw SQL queries to fetch the specified <typeparamref name="TEntity" /> data.
    /// </summary>
    /// <param name="sql">The raw SQL.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>An <see cref="IQueryable{TEntity}" /> that contains elements that satisfy the condition specified by raw SQL.</returns>
    IQueryable<TEntity> FromSql(string sql, params object[] parameters);

    /// <summary>
    /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
    /// </summary>
    /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
    /// <returns>The found entity or null.</returns>
    TEntity? Find(params object[] keyValues);

    /// <summary>
    /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
    /// </summary>
    /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
    /// <returns>A <see cref="Task{TEntity}"/> that represents the asynchronous find operation. The task result contains the found entity or null.</returns>
    ValueTask<TEntity?> FindAsync(params object[] keyValues);

    /// <summary>
    /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
    /// </summary>
    /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task{TEntity}"/> that represents the asynchronous find operation. The task result contains the found entity or null.</returns>
    ValueTask<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <returns>The <see cref="IQueryable{TEntity}"/>.</returns>
    IQueryable<TEntity> All();

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
    IQueryable<TEntity> All(Expression<Func<TEntity, bool>>? predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                              bool ignoreQueryFilters = false);

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
    IQueryable<TResult> All<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool ignoreQueryFilters = false) where TResult : class;

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>The <see cref="IQueryable{TEntity}"/>.</returns>
    Task<IList<TEntity>> AllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>An <see cref="IList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
    Task<IList<TEntity>> AllAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
                                              bool ignoreQueryFilters = false,
                                              CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all entities. This method is not recommended
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>An <see cref="IList{TResult}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
    Task<IList<TResult>> AllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
        bool ignoreQueryFilters = false,
        CancellationToken cancellationToken = default) where TResult : class;

    /// <summary>
    /// Gets the count based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    int Count(Expression<Func<TEntity, bool>>? predicate = null);

    /// <summary>
    /// Gets async the count based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns></returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the long count based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    long LongCount(Expression<Func<TEntity, bool>>? predicate = null);

    /// <summary>
    /// Gets async the long count based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns></returns>
    Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the max based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns>decimal</returns>
    TEntity? Max(Expression<Func<TEntity, bool>>? predicate = null);

    /// <summary>
    /// Gets the max based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    T? Max<T>(Expression<Func<TEntity, T>> selector);

    /// <summary>
    /// Gets the max based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    T? Max<T>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, T>> selector);

    /// <summary>
    /// Gets the max based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    Task<TEntity?> MaxAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the max based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    Task<T?> MaxAsync<T>(Expression<Func<TEntity, T>> selector, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the max based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    Task<T?> MaxAsync<T>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, T>> selector, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the min based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns>decimal</returns>
    TEntity? Min(Expression<Func<TEntity, bool>>? predicate = null);

    /// <summary>
    /// Gets the min based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    T? Min<T>(Expression<Func<TEntity, T>> selector);

    /// <summary>
    /// Gets the min based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    T? Min<T>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, T>> selector);

    /// <summary>
    /// Gets the min based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    Task<TEntity?> MinAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the min based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    Task<T?> MinAsync<T>(Expression<Func<TEntity, T>> selector, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the min based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    Task<T?> MinAsync<T>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, T>> selector, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the average based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    decimal Average(Expression<Func<TEntity, decimal>> selector);

    /// <summary>
    /// Gets the average based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    ///  /// <param name="selector"></param>
    /// <returns>decimal</returns>
    decimal Average(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> selector);

    /// <summary>
    /// Gets the average based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the average based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    Task<decimal> AverageAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> selector, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the sum based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <returns>decimal</returns>
    decimal Sum(Expression<Func<TEntity, decimal>> selector);

    /// <summary>
    /// Gets the sum based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    ///  /// <param name="selector"></param>
    /// <returns>decimal</returns>
    decimal Sum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> selector);

    /// <summary>
    /// Gets the sum based on a predicate.
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the sum based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>decimal</returns>
    Task<decimal> SumAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> selector, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the Exists record based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    bool Any(Expression<Func<TEntity, bool>>? predicate = null);

    /// <summary>
    /// Gets the Async Exists record based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns></returns>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

}