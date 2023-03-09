//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: UpdateRoleApiRequest.cs
//摘要: 更新角色请求
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
/// 更新角色请求
/// </summary>
public class UpdateRoleApiRequest : RoleModelApiRequest
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; } = -1;
}