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

/// <summary>
/// 授权
/// </summary>
public class AuthController : MaxController
{
    private readonly IMapper _mapper;
    private readonly MaxOption _maxOption;
    private readonly KylinOption _kylinOption;
    private readonly IAuthService _authService;
    private readonly IMenuService _menuService;

    public AuthController(IMapper mapper, IOptions<MaxOption> option, IOptions<KylinOption> kylinOption, IAuthService authService, IMenuService menuService)
    {
        _mapper = mapper;
        _maxOption = option.Value;
        _kylinOption = kylinOption.Value;
        _authService = authService;
        _menuService = menuService;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Result<LoginApiResponse>> Login(PasswordLoginApiRequest request)
    {
        PasswordLoginRequest model = new();
        model.UserName = request.UserName;
        model.Password = request.Password;
        model.IP = this.WorkContext.IP;
        model.SID = request.SID;
        var result = await _authService.LoginAsync(model);
        var response = _mapper.Map<LoginApiResponse>(result);
        return Success(response);
    }

    /// <summary>
    /// 登出
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<Result> Logout()
    {
        await _authService.LogoutAsync(WorkContext.AccessChain.AccessSession.Token);
        return Success();
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Result> ChangePassword(ChangePasswordApiRequest request)
    {
        await _authService.ChangePasswordAsync(AccessChain.Member!.Id, request.OldPassword, request.NewPassword);
        return Success();
    }

    /// <summary>
    /// 获取成员
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<Result<MemberApiResponse>> GetMember()
    {
        var member = await _authService.GetMemberAsync(AccessChain.AccessSession.MemberId);
        MemberApiResponse response = _mapper.Map<MemberApiResponse>(member);
        return Success(response);
    }

    /// <summary>
    /// 获取菜单
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<Result<MenuResult?>> GetMenu()
    {
        var menu = await _authService.GetRoleMenuAsync(AccessChain);
        return Success(menu);
    }

    /// <summary>
    /// 获取角色
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<Result<RoleApiResponse>> GetRole()
    {
        long xppId = AccessChain.AccessSession.XppId;
        var role = await _authService.GetRoleAsync(AccessChain);
        return Success(_mapper.Map<RoleApiResponse>(role));
    }
}