//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Result.cs
//摘要: Result
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.DependencyInjection;
using iMaxSys.Identity.Models.Request;
using iMaxSys.Identity.Models.Response;

namespace Kylin.Services.Member;

public interface IAuthService : IDependency
{
    /// <summary>
    /// 代码登录
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<LoginResponse> LoginAsync(CodeLoginRequest request);
}

