﻿//----------------------------------------------------------------
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
using iMaxSys.Max.Identity.Domain;

namespace iMaxSys.Identity.Models;

/// <summary>
/// 获取角色请求
/// </summary>
public class RoleRequest : DomainRequest
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public long Id { get; set; }
}

/// <summary>
/// 获取roles请求
/// </summary>
public class RolesRequest : PagedDomainRequest
{
}
