//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: GenericCache.cs
//摘要: 通用缓存
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Caching;

public class GenericCache : RedisService, IGenericCache
{
    public GenericCache(IOptions<MaxOption> option) : base(option.Value.Caching!.Connection, option.Value.XppId)
    {
    }
}