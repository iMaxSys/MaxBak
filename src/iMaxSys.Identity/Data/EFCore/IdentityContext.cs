//----------------------------------------------------------------
//Copyright (C) 2016-2026 Care Co.,Ltd.
//All rights reserved.
//
//文件: MaxIdentityContext.cs
//摘要: 身份系统上下文
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Data.EFCore;
using iMaxSys.Identity.Common;

namespace iMaxSys.Identity.Data.EFCore;

/// <summary>
/// MaxContext
/// </summary>
public class IdentityContext : EfDbContext
{
    //private readonly MaxOption _maxOption;
    //public static readonly LoggerFactory LoggerFactory =
    //new LoggerFactory(new[] { new DebugLoggerProvider() });

    public IdentityContext(IOptions<MaxOption> options) : base(options.Value.Identity.Databases ?? options.Value.Core.Databases)
    {
        //_maxOption = options.Value;
    }
}

public class IdentityReadOnlyContext : EfReadOnlyDbContext
{
    //private readonly MaxOption _maxOption;
    //public static readonly LoggerFactory LoggerFactory =
    //new LoggerFactory(new[] { new DebugLoggerProvider() });

    public IdentityReadOnlyContext(IOptions<MaxOption> options) : base(options.Value.Identity.Databases ?? options.Value.Core.Databases)
    {
        //_maxOption = options.Value;
    }
}