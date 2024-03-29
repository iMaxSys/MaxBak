﻿//----------------------------------------------------------------
//Copyright (C) 2016-2025 Care Co.,Ltd.
//All rights reserved.
//
//文件: MemberExt.cs
//摘要: 成员账号扩展
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Common;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Data.Entities;

namespace iMaxSys.Identity.Data.Entities;

/// <summary>
/// 会员扩展
/// </summary>
public class MemberExt : TenantMasterEntity
{
    /// <summary>
    /// 应用x社交账号Id
    /// </summary>
    public long XppSnsId { get; set; }

    /// <summary>
    /// 会员Id
    /// </summary>
    public long MemberId { get; set; }

    /// <summary>
    /// OpenId
    /// </summary>
    public string OpenId { get; set; } = string.Empty;

    /// <summary>
    /// UnionId
    /// </summary>
    public string UnionId { get; set; } = string.Empty;

    /// <summary>
    /// Token
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime Expires { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; } = Status.Enable;

    /// <summary>
    ///  会员信息
    /// </summary>
    public virtual Member Member { get; set; } = new();
}