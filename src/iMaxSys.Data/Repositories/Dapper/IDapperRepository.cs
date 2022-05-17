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

namespace iMaxSys.Data.Repositories.Dapper;

/// <summary>
/// IDapperRepository
/// </summary>
public interface IDapperRepository : IRepositoryBase
{
    /// <summary>
    /// 连接
    /// </summary>
    IDbConnection Connection { get; }

    /// <summary>
    /// 事务
    /// </summary>
    IDbContextTransaction? Transaction { get; }

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
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);

    /// <summary>
    /// ExecuteAsync
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);
}