//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IRedisService.cs
//摘要: IRedisService
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Caching;

namespace iMaxSys.Caching.Redis;

/// <summary>
/// IRedisService
/// </summary>
public interface IRedisService : ICache, ISingleton
{
}
