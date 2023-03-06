//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: RoleResponse.cs
//摘要: 角色应答
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-04-26
//----------------------------------------------------------------

using iMaxSys.Max.Web.Mvc;
using iMaxSys.Max.Common.Enums;

namespace Kylin.Api.Admin.ViewModels;

/// <summary>
/// 角色应答
/// </summary>
public class TenantApiResponse : ApiResponse
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 别名
    /// </summary>
    public string Alias { get; set; } = string.Empty;

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
    /// 状态
    /// </summary>
    public Status Status { get; set; } = Status.Enable;
}

