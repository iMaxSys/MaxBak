//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: DomainRequest.cs
//摘要: 领域/服务请求分页请求
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Common.Domain;

/// <summary>
/// 领域/服务请求分页请求
/// </summary>
public abstract class PagedDomainDataRequest : DomainDataRequest
{
    /// <summary>
    /// 关键字
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// 索引
    /// </summary>
    public int Index { get; set; } = 0;

    /// <summary>
    /// 页大小
    /// </summary>
    public int Size { get; set; } = 100;

    /// <summary>
    /// 排序字段
    /// </summary>
    public string? Sort { get; set; }

    /// <summary>
    /// 是否升序
    /// </summary>
    public bool Ascending { get; set; }
}