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
/// <typeparam name="R">只读</typeparam>
public class UnitOfWork<T, R> : IUnitOfWork<T, R> where T : DbContext where R : DbContext
{
    protected readonly T _context;
    protected readonly R _readOnlyContext;
    protected readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Gets the db context.
    /// </summary>
    /// <returns>The instance of type <typeparamref name="TContext"/>.</returns>
    public T DbContext => _context;

    /// <summary>
    /// Gets the read only db context.
    /// </summary>
    /// <returns>The instance of type <typeparamref name="TContext"/>.</returns>
    public R ReadOnlyDbContext => _readOnlyContext;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="context"></param>
    /// <param name="onlyContext"></param>
    /// <param name="serviceProvider"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public UnitOfWork(T context, R onlyContext, IServiceProvider serviceProvider)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _readOnlyContext = onlyContext ?? throw new ArgumentNullException(nameof(onlyContext));
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
    /// 获取只读范型仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public IReadOnlyRepository<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : Entity
    {
        //var respositories = _serviceProvider.GetServices<IReadOnlyRepository<TEntity>>();
        var respositoy = _serviceProvider.GetServices<IReadOnlyRepository<TEntity>>().FirstOrDefault(x => x.Code == _readOnlyContext.GetType().GetHashCode());
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
        //try
        //{
        //    var a = _context.Database.GetDbConnection();
        //    return await _context.SaveChangesAsync(cancellationToken);

        //}
        //catch (Exception ex)
        //{
        //    return 0;
        //}
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

    /// <summary>
    /// 切换数据库
    /// </summary>
    /// <param name="database"></param>
    public void ChangeDatabase(string database)
    {
        //var connection = _context.Database.GetDbConnection();
        //if (connection.State.HasFlag(ConnectionState.Open))
        //{
        //    connection.ChangeDatabase(database);
        //}
        //else
        //{
        //    var connectionString = Regex.Replace(connection.ConnectionString.Replace(" ", ""), @"(?<=[Dd]atabase=)\w+(?=;)", database, RegexOptions.Singleline);
        //    connection.ConnectionString = connectionString;
        //}

        //// Following code only working for mysql.
        //var items = _context.Model.GetEntityTypes();
        //foreach (var item in items)
        //{
        //    if (item is IConventionEntityType entityType)
        //    {
        //        entityType.SetSchema(database);
        //    }
        //}
    }

    /// <summary>
    /// 执行Sql命令
    /// </summary>
    /// <param name="sql">The raw SQL.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>The number of state entities written to database.</returns>
    public int ExecuteSqlCommand(string sql, params object[] parameters) => _context.Database.ExecuteSqlRaw(sql, parameters);

    /// <summary>
    /// 获取数据对象集
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : Entity => _context.Set<TEntity>().FromSqlRaw(sql, parameters);
}

/// <summary>
/// UnitOfWork<T>
/// </summary>
/// <typeparam name="T"></typeparam>
public class UnitOfWork<T> : UnitOfWork<T, T>, IUnitOfWork<T> where T : DbContext
{
    public UnitOfWork(T context, T onlyContext, IServiceProvider serviceProvider) : base(context, onlyContext, serviceProvider)
    {
    }
}