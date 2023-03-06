//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: DomainDataRequest.cs
//摘要: 领域/服务数据请求
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Common.Domain;

/// <summary>
/// 领域/服务数据请求
/// </summary>
public abstract class DomainDataRequest : DomainRequest
{
    /// <summary>
    /// 数据归属ids
    /// </summary>
    public long[] Ids { get; set; } = new long[4];
}