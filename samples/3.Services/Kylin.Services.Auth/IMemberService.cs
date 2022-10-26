// ----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IMemberService.cs
//摘要: 用户服务接口
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2022-10-15
//----------------------------------------------------------------

using iMaxSys.Identity;
using iMaxSys.Max.DependencyInjection;

namespace Kylin.Services.Auth;

/// <summary>
/// 用户服务接口
/// </summary>
public interface IMemberService : IDependency, IUserProvider
{
}