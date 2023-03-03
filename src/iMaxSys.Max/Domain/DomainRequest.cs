//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: DomainRequest.cs
//摘要: 领域/服务请求
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Common.Domain;

public abstract class DomainRequest
{
    /// <summary>
    /// 应用Id
    /// </summary>
    public long XppId { get; set; }

    /// <summary>
    /// 租户Id
    /// </summary>
    public long TenantId { get; set; }

    /// <summary>
    /// 第一级Id
    /// </summary>
    public long FirstLevelId { get; set; }

    /// <summary>
    /// 第二级Id
    /// </summary>
    public long SecondLevelId { get; set; }

    /// <summary>
    /// 第三级Id
    /// </summary>
    public long ThridLevelId { get; set; }
}