//----------------------------------------------------------------
//Copyright (C) 2016-2025 Co.,Ltd.
//All rights reserved.
//
//文件: EfDbContext.cs
//摘要: ef读写上下文 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Data.Common;
using iMaxSys.Data.DbContexts;

namespace iMaxSys.Data.EFCore;

/// <summary>
/// read/write dbcontext
/// </summary>
public class EfDbContext : EfReadOnlyDbContext, IDbContext
{
    public EfDbContext(List<DatabaseOption> databases) : base(databases)
    {
    }

    protected override DatabaseOption SelectDatabase(List<DatabaseOption> databases)
    {
        if (databases.Count == 0)
        {
            throw new MaxException(ResultCode.ConnectionIsNull);
        }
        return databases[0];
    }
}