// ----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: AuthService.cs
//摘要: 权限服务
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2022-10-15
//----------------------------------------------------------------

using iMaxSys.Identity.Models.Request;
using iMaxSys.Identity.Models.Response;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.Identity.Domain;

namespace Kylin.Services.Auth;

/// <summary>
/// 权限服务
/// </summary>
public class AuthService : IAuthService
{
    private readonly iMaxSys.Identity.IMemberService _memberService;

    public AuthService(iMaxSys.Identity.IMemberService memberService)
    {
        _memberService = memberService;
    }

    /// <summary>
    /// 代码登录
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<LoginResponse> LoginAsync(CodeLoginRequest request)
    {
        IAccessChain accessChain = await _memberService.LoginAsync(request);

        LoginResponse response = new();
        response.Code = MaxCode.Success.GetHashCode();

        return response;
    }
}

