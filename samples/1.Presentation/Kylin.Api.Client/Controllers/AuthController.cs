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

using iMaxSys.Max.Options;
using iMaxSys.Max.Web.Mvc;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Identity;
using iMaxSys.Identity.Models;

using Kylin.Framework.Options;
using Kylin.Services.Auth;
using Kylin.Api.Client.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Kylin.Api.Client.Controllers;

public class AuthController : MaxController
{
    private readonly IMapper _mapper;
    private readonly MaxOption _maxOption;
    private readonly KylinOption _kylinOption;
    private readonly IAuthService _authService;

    public AuthController(IMapper mapper, IOptions<MaxOption> option, IOptions<KylinOption> kylinOption, IAuthService authService)
    {
        _mapper = mapper;
        _maxOption = option.Value;
        _kylinOption = kylinOption.Value;
        _authService = authService;
    }

    [HttpGet]
    public object Name()
    {
        return _maxOption;
        //throw new MaxException(99, "错误");
        //return "hello world";
    }

    [HttpGet]
    public object Config()
    {
        return _kylinOption;
        //throw new MaxException(99, "错误");
        //return "hello world";
    }

    [HttpPost]
    public async Task<Result<LoginResponse>> WeChatLiteLogin(CodeLoginModel reqeust)
    {
        reqeust.IP = WorkContext.IP;
        LoginResult result = await _authService.LoginAsync(reqeust);
        var loginResponse = _mapper.Map<LoginResponse>(result);
        return Success(loginResponse);
    }
}

