// ----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MemberService.cs
//摘要: 用户服务
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-15
//----------------------------------------------------------------

using iMaxSys.Data.Entities;
using Kylin.Data.Models.Entities;

namespace Kylin.Data.Models.Auth;

/// <summary>
/// 客户
/// </summary>
public class Customer : KylinMasterEntity
{
    /// <summary>
    /// 消费次数
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// 消费总额
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// 入场次数
    /// </summary>
    public int InCount { get; set; }
}