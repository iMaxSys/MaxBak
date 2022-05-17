//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: UnitOfWork.cs
//摘要: UnitOfWork 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Exceptions;
using iMaxSys.Data.Common;
using iMaxSys.Data.Entities;
using iMaxSys.Data.Repositories;

namespace iMaxSys.Data;

/// <summary>
/// UnitOfWork
/// </summary>
/// <typeparam name="T">读写</typeparam>
/// <typeparam name="K">只读</typeparam>
public class UnitOfWork<T, K> : UnitOfWork<T>, IUnitOfWork<T, K> where T : DbContext where K : DbContext
{
    protected readonly K _readOnlyContext;

    /// <summary>
    /// Gets the read only db context.
    /// </summary>
    /// <returns>The instance of type <typeparamref name="TContext"/>.</returns>
    public K ReadOnlyDbContext => _readOnlyContext;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="context"></param>
    /// <param name="serviceProvider"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public UnitOfWork(T context, K readOnlyContext, IServiceProvider serviceProvider) : base(context, serviceProvider)
    {
        _readOnlyContext = readOnlyContext ?? throw new ArgumentNullException(nameof(readOnlyContext));
    }

    /// <summary>
    /// 获取只读范型仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public IReadOnlyRepository<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : Entity
    {
        var respositoy = _serviceProvider.GetServices<IReadOnlyRepository<TEntity>>().FirstOrDefault(x => x.Code == _readOnlyContext.GetType().GetHashCode());
        if (respositoy == null)
        {
            throw new MaxException(ResultCode.CantGetRepository);
        }
        return respositoy;
    }
}

/// <summary>
/// UnitOfWork<T>
/// </summary>
/// <typeparam name="T"></typeparam>
public class UnitOfWork<T> : IUnitOfWork<T> where T : DbContext
{
    protected readonly T _context;
    protected readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Gets the db context.
    /// </summary>
    /// <returns>The instance of type <typeparamref name="TContext"/>.</returns>
    public T DbContext => _context;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="context"></param>
    /// <param name="serviceProvider"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public UnitOfWork(T context, IServiceProvider serviceProvider)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 获取范型仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity
    {
        //var repo = _serviceProvider.GetServices<IRepository<TEntity>>();
        //var repo = _serviceProvider.GetServices(typeof(IRepository<TEntity>));
        var respositoy = _serviceProvider.GetServices<IRepository<TEntity>>().FirstOrDefault(x => x.Code == _context.GetType().GetHashCode());
        if (respositoy == null)
        {
            throw new MaxException(ResultCode.CantGetRepository);
        }
        return respositoy;
    }

    /// <summary>
    /// 获取定制仓储
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    /// <returns></returns>
    public TRepository GetCustomRepository<TRepository>() where TRepository : IRepositoryBase
    {
        try
        {
            return _serviceProvider.GetRequiredService<TRepository>();
        }
        catch (Exception ex)
        {
            throw new MaxException(ex, ResultCode.CantGetCustomRepository);
        }
    }

    /// <summary>
    /// SaveChanges
    /// </summary>
    /// <returns></returns>
    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    /// <summary>
    /// SaveChangesAsync
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// SaveChangesAsync
    /// </summary>
    /// <param name="unitOfWorks"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> SaveChangesAsync(IUnitOfWork[] unitOfWorks, CancellationToken cancellationToken = default)
    {
        using var ts = new TransactionScope();
        int count = 0;
        foreach (var unitOfWork in unitOfWorks)
        {
            count += await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        count += await SaveChangesAsync(cancellationToken);

        ts.Complete();

        return count;
    }
}