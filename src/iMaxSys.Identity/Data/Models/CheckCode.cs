//----------------------------------------------------------------
//Copyright (C) 2016-2025 Care Co.,Ltd.
//All rights reserved.
//
//文件: CheckCode.cs
//摘要: CheckCode 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using System;

using iMaxSys.Max.Domain;
using iMaxSys.Data.Entities;

namespace iMaxSys.Identity.Data.Models;

/// <summary>
/// 
/// </summary>
public class CheckCode : TenantMasterEntity
{
    /// <summary>
    /// 应用Id
    /// </summary>
    public long XppId { get; set; }

    /// <summary>
    /// 业务Id
    /// </summary>
    public long BizId { get; set; }

    /// <summary>
    /// MemberId
    /// </summary>
    public long MemberId { get; set; }

    /// <summary>
    /// 目标
    /// </summary>
    public string To { get; set; } = string.Empty;

    /// <summary>
    /// 验证码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime Expires { get; set; }

    /// <summary>
    /// 校验次数
    /// </summary>
    public int CheckCount { get; set; } = 0;

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; } = Status.Enable;
}