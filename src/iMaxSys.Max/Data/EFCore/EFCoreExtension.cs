//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Extensions.cs
//摘要: EntityFrameworkCore扩展
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Data.EFCore;

/// <summary>
/// EFCoreExtension
/// </summary>
public static class EFCoreExtension
{
    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static int Execute(this DbContext dbContext, string sql)
    {
        return dbContext.Database.ExecuteSqlRaw(sql);
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static async Task<int> ExecuteAsync(this DbContext dbContext, string sql)
    {
        return await dbContext.Database.ExecuteSqlRawAsync(sql);
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="db"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static T Query<T>(this DbContext dbContext, string sql) where T : class, new()
    {
        using (var command = dbContext.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            dbContext.Database.OpenConnection();

            using (var reader = command.ExecuteReader())
            {
                var newObject = new T();
                var columns = new T().GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToList();
                while (reader.Read())
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var name = reader.GetName(i);
                        PropertyInfo? prop = columns.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
                        if (prop == null)
                        {
                            continue;
                        }
                        var val = reader.IsDBNull(i) ? null : reader[i];
                        prop.SetValue(newObject, val, null);
                    }
                    break;
                }

                return newObject;
            }
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="db"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static async Task<T> QueryAsync<T>(this DbContext dbContext, string sql) where T : class, new()
    {
        using (var command = dbContext.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            await dbContext.Database.OpenConnectionAsync();

            using (var reader = await command.ExecuteReaderAsync())
            {
                var newObject = new T();
                var columns = new T().GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToList();
                while (await reader.ReadAsync())
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var name = reader.GetName(i);
                        PropertyInfo? prop = columns.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
                        if (prop == null)
                        {
                            continue;
                        }
                        var val = reader.IsDBNull(i) ? null : reader[i];
                        prop.SetValue(newObject, val, null);
                    }
                    break;
                }

                return newObject;
            }
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="db"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static async Task<T> QueryAsync<T>(this DbContext dbContext, string sql, DbParameter[] parameters) where T : class, new()
    {
        using (var command = dbContext.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(parameters);

            await dbContext.Database.OpenConnectionAsync();

            using (var reader = await command.ExecuteReaderAsync())
            {
                var newObject = new T();
                var columns = new T().GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToList();
                while (await reader.ReadAsync())
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var name = reader.GetName(i);
                        PropertyInfo? prop = columns.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
                        if (prop == null)
                        {
                            continue;
                        }
                        var val = reader.IsDBNull(i) ? null : reader[i];
                        prop.SetValue(newObject, val, null);
                    }
                    break;
                }

                return newObject;
            }
        }
    }

    /// <summary>
    /// 查询列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="db"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static List<T> QueryList<T>(this DbContext dbContext, string sql) where T : class, new()
    {
        using (var command = dbContext.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            dbContext.Database.OpenConnection();

            using (var reader = command.ExecuteReader())
            {
                var list = new List<T>();
                var columns = new T().GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToList();
                while (reader.Read())
                {
                    var newObject = new T();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var name = reader.GetName(i);
                        PropertyInfo? prop = columns.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
                        if (prop == null)
                        {
                            continue;
                        }
                        var val = reader.IsDBNull(i) ? null : reader[i];
                        prop.SetValue(newObject, val, null);
                    }
                    list.Add(newObject);
                }

                return list;
            }
        }
    }

    /// <summary>
    /// 查询列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="db"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static List<T> QueryList<T>(this DbContext dbContext, string sql, DbParameter[] parameters) where T : class, new()
    {
        using (var command = dbContext.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(parameters);

            dbContext.Database.OpenConnection();

            using (var reader = command.ExecuteReader())
            {
                var list = new List<T>();
                var columns = new T().GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToList();
                while (reader.Read())
                {
                    var newObject = new T();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var name = reader.GetName(i);
                        PropertyInfo? prop = columns.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
                        if (prop == null)
                        {
                            continue;
                        }
                        var val = reader.IsDBNull(i) ? null : reader[i];
                        prop.SetValue(newObject, val, null);
                    }
                    list.Add(newObject);
                }

                return list;
            }
        }
    }

    /// <summary>
    /// 查询列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="db"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static async Task<List<T>> QueryListAsync<T>(this DbContext dbContext, string sql) where T : class, new()
    {
        using (var command = dbContext.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            await dbContext.Database.OpenConnectionAsync();

            using (var reader = await command.ExecuteReaderAsync())
            {
                var list = new List<T>();
                var columns = new T().GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToList();
                while (await reader.ReadAsync())
                {
                    var newObject = new T();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var name = reader.GetName(i);
                        PropertyInfo? prop = columns.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
                        if (prop == null)
                        {
                            continue;
                        }
                        var val = reader.IsDBNull(i) ? null : reader[i];
                        prop.SetValue(newObject, val, null);
                    }
                    list.Add(newObject);
                }

                return list;
            }
        }
    }

    /// <summary>
    /// 查询列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="db"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static async Task<List<T>> QueryListAsync<T>(this DbContext dbContext, string sql, DbParameter[] parameters) where T : class, new()
    {
        using (var command = dbContext.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(parameters);

            await dbContext.Database.OpenConnectionAsync();

            using (var reader = await command.ExecuteReaderAsync())
            {
                var list = new List<T>();
                var columns = new T().GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToList();
                while (await reader.ReadAsync())
                {
                    var newObject = new T();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var name = reader.GetName(i);
                        PropertyInfo? prop = columns.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
                        if (prop == null)
                        {
                            continue;
                        }
                        var val = reader.IsDBNull(i) ? null : reader[i];
                        prop.SetValue(newObject, val, null);
                    }
                    list.Add(newObject);
                }

                return list;
            }
        }
    }

    #region ExecuteSql

    public static int ExecuteCommand(this DbContext dbContext, string sql, params object[] parameters)
    {
        return dbContext.Database.ExecuteSqlRaw(sql, parameters);
    }

    public static async Task<int> ExecuteCommandAsync(this DbContext dbContext, string sql, params object[] parameters)
    {
        return await dbContext.Database.ExecuteSqlRawAsync(sql, parameters).ConfigureAwait(false);
    }

    public static async Task<int> ExecuteCommandAsync(this DbContext dbContext, string sql, CancellationToken cancellationToken = default)
    {
        return await dbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken).ConfigureAwait(false);
    }

    public static async Task<int> ExecuteCommandAsync(this DbContext dbContext, string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
    {
        return await dbContext.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken).ConfigureAwait(false);
    }

    #endregion

}
