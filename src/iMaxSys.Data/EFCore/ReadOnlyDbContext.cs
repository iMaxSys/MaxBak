//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ReadOnlyDbContext.cs
//摘要: 只读仓储
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-05-07
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Data.Common;
using iMaxSys.Data.DbContexts;

namespace iMaxSys.Data.EFCore;

/// <summary>
/// 只读上下文
/// </summary>
public class ReadOnlyDbContext : MaxDbContext, IReadOnlyDbContext
{
    private static int _index = 0;

    public ReadOnlyDbContext(List<DatabaseOption> databases) : base(databases)
    {
    }

    /// <summary>
    /// 选择数据库
    /// 0为主库(读写),1+为从库(只读)
    /// </summary>
    /// <param name="databases"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    protected override DatabaseOption SelectDatabase(List<DatabaseOption> databases)
    {
        if (databases.Count > 0)
        {
            if (databases.Count > 1)
            {
                //去除主库
                databases.RemoveAt(0);
                DatabaseOption database = databases[_index % databases.Count];
                _index = ++_index > 1024 ? 0 : _index;
                return database;
            }
            else
            {
                return databases[0];
            }
        }
        else
        {
            throw new MaxException(ResultCode.ConnectionIsNull);
        }
    }
}