﻿//----------------------------------------------------------------
//Copyright (C) 2016-2025 Care Co.,Ltd.
//All rights reserved.
//
//文件: Tenant.cs
//摘要: Tenant 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Data.Entities;
using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Core.Data.Entities;

/// <summary>
/// Tenant
/// </summary>
public class Tenant : MasterEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 别名
    /// </summary>
    public string Alias { get; set; } = string.Empty;

    /// <summary>
    /// 编号
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 速查码
    /// </summary>
    public string QuickCode { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 营业执照
    /// </summary>
    public string? License { get; set; }

    /// <summary>
    /// 等级
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// Logo
    /// </summary>
    public string? Logo { get; set; }

    /// <summary>
    /// contact
    /// </summary>
    public string? Contact { get; set; }

    /// <summary>
    /// phone
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// mail
    /// </summary>
    public string? Mail { get; set; }

    /// <summary>
    /// mail
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Start
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// End
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; } = Status.Enable;
}