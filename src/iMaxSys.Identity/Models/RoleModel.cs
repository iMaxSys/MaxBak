//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: RoleModel.cs
//摘要: 角色模型
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2019-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Identity.Models;

/// <summary>
/// 角色模型
/// </summary>
public class RoleModel
{
    /// <summary>
    /// Id
    /// </summary>
    public long? Id { get; set; }

    /// <summary>
    /// TenantId
    /// </summary>
    public long? TenantId { get; set; }

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
    /// Descripton
    /// </summary>
    public string? Descripton { get; set; }

    /// <summary>
    /// Icon
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Style
    /// </summary>
    public string? Style { get; set; }

    /// <summary>
    /// Menus("45675,45677")
    /// </summary>
    public string? Menus { get; set; }

    /// <summary>
    /// Operations("45675,45677")
    /// </summary>
    public string? Operations { get; set; }

    /// <summary>
    /// Start
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// End
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    public Status Status { get; set; } = Status.Enable;
}