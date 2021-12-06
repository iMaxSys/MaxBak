//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IGenericCache.cs
//摘要: IGenericCache
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Caching.Redis;
using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Max.Caching;

public interface IGenericCache : ICache, ISingleton
{
}

