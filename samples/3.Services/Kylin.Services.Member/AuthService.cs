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

using iMaxSys.Max.DependencyInjection;
using iMaxSys.Identity;
using iMaxSys.Identity.Models.Request;
using iMaxSys.Identity.Models.Response;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Common.Enums;

namespace Kylin.Services.Member;

public class AuthService : IDependency
{
    private readonly MemberService _memberService;

    public AuthService(MemberService memberService)
    {
        _memberService = memberService;
    }

    /// <summary>
    /// 代码登录
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    //public async Task<LoginResponse> LoginAsync(CodeLoginRequest request)
    //{
    //    IAccessChain accessChain = await _memberService.LoginAsync(request);

    //    LoginResponse response = new();
    //    response.Code = MaxCode.Success.GetHashCode();
    //}
}