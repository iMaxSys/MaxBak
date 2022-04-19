﻿//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ICacheFactory.cs
//摘要: ICacheFactory
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Caching.Common;

namespace iMaxSys.Caching;

public interface ICacheFactory
{
    ICache GetService(CacheSource source, string connection);
}
