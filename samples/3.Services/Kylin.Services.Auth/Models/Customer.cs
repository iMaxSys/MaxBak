// ----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Customer.cs
//摘要: 顾客
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-15
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;

namespace Kylin.Services.Auth.Models;

/// <summary>
/// 顾客
/// </summary>
public class Customer : User, IUser
{
	/// <summary>
	/// 到场次数
	/// </summary>
	public int Count { get; set; }
}

