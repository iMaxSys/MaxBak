﻿//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IdentityCache.cs
//摘要: IdentityCache
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Options;
using iMaxSys.Max.Caching.Redis;
using iMaxSys.Max.Identity.Domain;

namespace iMaxSys.Identity.Data.Repositories;

/// <summary>
/// IdentityCache
/// </summary>
public class IdentityCache : RedisService, IIdentityCache
{
    public IdentityCache(IOptions<MaxOption> option) : base(option.Value.Caching.Connection, option.Value.XppId)
    {
    }
}