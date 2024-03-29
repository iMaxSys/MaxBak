﻿//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: CoreRepository.cs
//摘要: Core通用仓储
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-11-16
//----------------------------------------------------------------

using AutoMapper;

using iMaxSys.Max.Caching;
using iMaxSys.Max.Options;
using iMaxSys.Data.Entities;
using iMaxSys.Core.Data.EFCore;
using iMaxSys.Data.Repositories;
using iMaxSys.Data.EFCore.Repositories;

namespace iMaxSys.Core.Data.Repositories;

/// <summary>
/// Core通用仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public class CoreRepository<T> : EfRepository<T>, ICoreRepository<T> where T : Entity
{
    //全局缓存标志
    protected const bool _global = true;

    protected readonly IMapper Mapper;
    protected readonly ICache Cache;
    protected readonly MaxOption Option;

    public CoreRepository(CoreContext context, IMapper mapper, IOptions<MaxOption> option, ICacheFactory cacheFactory) : base(context)
    {
        Mapper = mapper;
        Option = option.Value;
        Cache = cacheFactory.GetService();
    }
}

public class CoreReadOnlyRepository<T> : EfReadOnlyRepository<T>, ICoreReadOnlyRepository<T> where T : Entity
{
    //全局缓存标志
    protected const bool _global = true;

    protected readonly IMapper Mapper;
    protected readonly ICache Cache;
    protected readonly MaxOption Option;

    public CoreReadOnlyRepository(CoreReadOnlyContext context, IMapper mapper, IOptions<MaxOption> option, ICacheFactory cacheFactory) : base(context)
    {
        Mapper = mapper;
        Option = option.Value;
        Cache = cacheFactory.GetService();
    }
}
