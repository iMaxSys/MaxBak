//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IDapperRepository.cs
//摘要: IDapperRepository
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-11-16
//----------------------------------------------------------------

using Dapper;

namespace iMaxSys.Data.Dapper.Repositories;

/// <summary>
/// DapperRepository<T>
/// </summary>
/// <typeparam name="T"></typeparam>
public class DapperRepository<TDbContext> : IDapperRepository where TDbContext : DbContext
{
    private readonly TDbContext _context;

    public DapperRepository(TDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 连接
    /// </summary>
    public IDbConnection Connection => _context.Database.GetDbConnection();

    /// <summary>
    /// 事务
    /// </summary>
    public IDbContextTransaction? Transaction => _context.Database.CurrentTransaction;

    /// <summary>
    /// ExecuteAsync
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    public async Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await Connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    /// QueryAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await Connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }
}