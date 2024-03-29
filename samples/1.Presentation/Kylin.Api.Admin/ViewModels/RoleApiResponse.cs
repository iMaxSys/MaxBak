﻿//----------------------------------------------------------------
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
public class RoleApiResponse : ApiResponse
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; } = -1;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 别名
    /// </summary>
    public string Alias { get; set; } = String.Empty;

    /// <summary>
    /// 代码
    /// </summary>
    public string Code { get; set; } = String.Empty;

    /// <summary>
    /// 快速代码
    /// </summary>
    public string QuickCode { get; set; } = String.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string? Descripton { get; set; }

    /// <summary>
    /// MenuIds
    /// </summary>
    public string[]? MenuIds { get; set; }

    /// <summary>
    /// OperationIds
    /// </summary>
    public string[]? OperationIds { get; set; }

    /// <summary>
    /// 启用日期
    /// </summary>
    public string? Start { get; set; }

    /// <summary>
    /// 停用日期
    /// </summary>
    public string? End { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; }
}

