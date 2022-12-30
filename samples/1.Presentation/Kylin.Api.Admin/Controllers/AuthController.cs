//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: AuthController.cs
//摘要: 授权控制器
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-10-12
//----------------------------------------------------------------

using iMaxSys.Max.Common;
using iMaxSys.Max.Options;
using iMaxSys.Max.Web.Mvc;
using iMaxSys.Max.Exceptions;
using iMaxSys.Core.Models;
using iMaxSys.Core.Services;
using iMaxSys.Identity;
using iMaxSys.Identity.Models;

using Kylin.Services.Auth;
using Kylin.Framework.Options;
using Kylin.Api.Admin.ViewModels;

namespace Kylin.Api.Client.Controllers;

public class AuthController : MaxController
{
    private readonly IMapper _mapper;
    private readonly MaxOption _maxOption;
    private readonly KylinOption _kylinOption;
    private readonly IAuthService _authService;
    private readonly IDictService _dictService;

    public AuthController(IMapper mapper, IOptions<MaxOption> option, IOptions<KylinOption> kylinOption, IAuthService authService, IDictService dictService)
    {
        _mapper = mapper;
        _maxOption = option.Value;
        _kylinOption = kylinOption.Value;
        _authService = authService;
        _dictService = dictService;
    }

    [HttpPost]
    public Result<LoginResponse> Login(LoginRequest loginRequest)
    {
        LoginResponse loginResponse = new();
        loginResponse.Token = "1345677";
        return Success(loginResponse);
    }

    [HttpPost]
    public Result<MemberResponse> Info()
    {
        MemberResponse response = new();
        response.Name = "孙悟空";
        response.Mobile = "18666666666";
        return Success(response);
    }
}