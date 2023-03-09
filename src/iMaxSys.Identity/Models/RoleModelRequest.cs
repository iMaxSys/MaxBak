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

using iMaxSys.Max.Common.Domain;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.Identity.Domain;

namespace iMaxSys.Identity.Models;

/// <summary>
/// 角色模型请求
/// </summary>
public class RoleModelRequest : DomainRequest
{
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
    public string? MenuIds { get; set; }

    /// <summary>
    /// OperationIds
    /// </summary>
    public string? OperationIds { get; set; }

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