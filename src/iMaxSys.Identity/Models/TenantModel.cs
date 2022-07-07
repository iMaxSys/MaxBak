//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: TenantModel.cs
//摘要: 租户模型
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Identity.Models;

/// <summary>
/// 租户模型
/// </summary>
public class TenantModel
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = String.Empty;

    /// <summary>
    /// Alias
    /// </summary>
    public string Alias { get; set; } = String.Empty;

    /// <summary>
    /// Code
    /// </summary>
    public string Code { get; set; } = String.Empty;

    /// <summary>
    /// QuickCode
    /// </summary>
    public string QuickCode { get; set; } = String.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

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