//----------------------------------------------------------------
//Copyright (C) 2016-2026 Care Co.,Ltd.
//All rights reserved.
//
//文件: CoreContext.cs
//摘要: Core上下文
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Data.EFCore;
using Microsoft.Extensions.Options;

namespace iMaxSys.Core.Data.EFCore;

/// <summary>
/// MaxContext
/// </summary>
public class CoreContext : EfDbContext
{
    //private readonly MaxOption _maxOption;
    //public static readonly LoggerFactory LoggerFactory =
    //new LoggerFactory(new[] { new DebugLoggerProvider() });

    public CoreContext(IOptions<MaxOption> options) : base(options.Value.Identity.Databases ?? options.Value.Core.Databases)
    {
        //_maxOption = options.Value;
    }
}

public class CoreReadOnlyContext : EfReadOnlyDbContext
{
    //private readonly MaxOption _maxOption;
    //public static readonly LoggerFactory LoggerFactory =
    //new LoggerFactory(new[] { new DebugLoggerProvider() });

    public CoreReadOnlyContext(IOptions<MaxOption> options) : base(options.Value.Identity.Databases ?? options.Value.Core.Databases)
    {
        //_maxOption = options.Value;
    }
}