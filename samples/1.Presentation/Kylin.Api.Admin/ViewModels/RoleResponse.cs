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
public class RoleResponse : Response
{
    /// <summary>
    /// Id
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? Alias { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public int Type { get; set; }
}

