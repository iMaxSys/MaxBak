//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: MenuApiResponse.cs
//摘要: 菜单应答
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
/// 菜单应答
/// </summary>
public class MenuApiResponse : ApiResponse
{
    /// <summary>
    /// MenuIds
    /// </summary>
    public string[]? MenuIds { get; set; }

    /// <summary>
    /// operationIds
    /// </summary>
    public string[]? operationIds { get; set; }
}