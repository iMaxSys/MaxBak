// ----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IAuthService.cs
//摘要: 权限接口
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2022-10-15
//----------------------------------------------------------------

using iMaxSys.Max.DependencyInjection;
using iMaxSys.Identity.Models;

namespace Kylin.Services.Auth;

/// <summary>
/// 权限接口
/// </summary>
public interface IAuthService : IDependency
{
    /// <summary>
    /// 代码登录
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<LoginResult> LoginAsync(CodeLoginModel request);
}

