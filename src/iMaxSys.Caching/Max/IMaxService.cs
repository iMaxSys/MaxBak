//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IMaxService.cs
//摘要: IMaxService
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Caching;

namespace iMaxSys.Caching.Max;

/// <summary>
/// IMaxService
/// </summary>
public interface IMaxService : ICache, ISingleton
{
}

