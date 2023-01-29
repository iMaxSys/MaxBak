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
    private readonly IMenuService _menuService;

    public AuthController(IMapper mapper, IOptions<MaxOption> option, IOptions<KylinOption> kylinOption, IAuthService authService, IDictService dictService, IMenuService menuService)
    {
        _mapper = mapper;
        _maxOption = option.Value;
        _kylinOption = kylinOption.Value;
        _authService = authService;
        _dictService = dictService;
        _menuService = menuService;
    }

    [HttpPost]
    public async Task<Result<LoginResponse>> Login(PasswordLoginRequest request)
    {
        PasswordLoginModel model = new();
        model.UserName = request.UserName;
        model.Password = request.Password;
        model.IP = this.WorkContext.IP;
        model.Type = request.Type;
        model.XppSnsId = request.SID;
        var result = await _authService.LoginAsync(model);
        var response = _mapper.Map<LoginResponse>(result);
        return Success(response);
    }

    [HttpPost]
    public Result<MemberResponse> Info()
    {
        MemberResponse response = new();
        response.Name = "孙悟空";
        response.Mobile = "18666666666";
        return Success(response);
    }

    [HttpPost]
    public async Task<Result<MenuModel?>> GetMenu()
    {
        var menu = await _menuService.GetRoleMenuAsync(1, 2000, 0);
        return Success(menu);
    }
}