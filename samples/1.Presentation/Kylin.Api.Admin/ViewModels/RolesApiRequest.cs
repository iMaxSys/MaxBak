//----------------------------------------------------------------
//Copyright (C) 2016-2022 Co.,Ltd.
//All rights reserved.
//
//文件: LoginRequest.cs
//摘要: 登录请求
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-04-26
//----------------------------------------------------------------

using iMaxSys.Max.Web.Mvc;

namespace Kylin.Api.Admin.ViewModels;

/// <summary>
/// 获取角色s请求
/// </summary>
public class RolesApiRequest : PagedApiRequest
{
	/// <summary>
	/// 名称
	/// </summary>
	public string Name { get; set; } = string.Empty;
}

