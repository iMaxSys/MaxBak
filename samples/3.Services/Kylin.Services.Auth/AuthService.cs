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

using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Identity.Models;

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
    public async Task<LoginResult> LoginAsync(CodeLoginModel request)
    {
        IAccessChain accessChain = await _memberService.LoginAsync(request);

        return new LoginResult
        {
            Token = accessChain.AccessSession.Token,
            Expires = accessChain.AccessSession.Expires,
            Member = accessChain.Member
        };
    }
}

