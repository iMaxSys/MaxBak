//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Enums.cs
//摘要: 枚举
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-04-19
//----------------------------------------------------------------

namespace iMaxSys.Caching.Common.Enums;

/// <summary>
/// 平台
/// </summary>
public enum CacheServer
{
    /// <summary>
    /// 进程内
    /// </summary>
    [Description("Max")]
    Max = 0,
    /// <summary>
    /// Redis
    /// </summary>
    [Description("Redis")]
    Redis = 1
}