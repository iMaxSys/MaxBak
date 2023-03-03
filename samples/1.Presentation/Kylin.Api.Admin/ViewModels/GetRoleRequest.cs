//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: 获取角色请求.cs
//摘要: GetRoleRequest
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-04-26
//----------------------------------------------------------------

using iMaxSys.Max.Web.Mvc;

namespace Kylin.Api.Admin.ViewModels;

/// <summary>
/// 获取角色请求
/// </summary>
public class GetRoleRequest : Request
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }
}

