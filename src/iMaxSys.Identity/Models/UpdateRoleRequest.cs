﻿//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: UpdateRoleRequest.cs
//摘要: 更新角色模型
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
/// 获取角色请求
/// </summary>
public class UpdateRoleRequest : RoleModelRequest
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; } = -1;
}