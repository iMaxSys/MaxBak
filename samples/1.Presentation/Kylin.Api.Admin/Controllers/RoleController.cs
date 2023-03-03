//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: RoleController.cs
//摘要: 角色控制器
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

public class RoleController : MaxController
{
    private readonly IMapper _mapper;
    private readonly KylinOption _kylinOption;
    private readonly IRoleService _roleService;

    public RoleController(IMapper mapper, IOptions<KylinOption> kylinOption, IRoleService authService)
    {
        _mapper = mapper;
        _kylinOption = kylinOption.Value;
        _roleService = authService;
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    //[HttpPost]
    //public async Task<Result<GetRoleResponse>> GetList(GetRolesRequest request)
    //{
    //    RolesRequest rolesRequest = new();
    //    rolesRequest.Name = request.Name;
    //    rolesRequest.XppId = WorkContext.Xpp.Id;
    //    rolesRequest.TenantId = WorkContext.;
    //    PasswordLoginModel model = new();
    //    model.UserName = request.UserName;
    //    model.Password = request.Password;
    //    model.IP = this.WorkContext.IP;
    //    model.SID = request.SID;
    //    var result = await _authService.LoginAsync(model);
    //    var response = _mapper.Map<LoginResponse>(result);
    //    return Success(response);
    //}
}