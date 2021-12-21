//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ITenant.cs
//摘要: ITenant
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2020-01-01
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max.Identity.Domain;

/// <summary>
/// 租户
/// </summary>
public interface ITenant
{
    /// <summary>
    /// Id
    /// </summary>
    long Id { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// 别名
    /// </summary>
    string? Alias { get; set; }

    /// <summary>
    /// Start
    /// </summary>
    DateTime? Start { get; set; }

    /// <summary>
    /// End
    /// </summary>
    DateTime? End { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    Status Status { get; set; }
}

/// <summary>
/// 租户
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ITenant<T> : ITenant
{
    /// <summary>
    /// 更多信息
    /// </summary>
    T? Info { get; set; }
}