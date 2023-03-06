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

    public RoleController(IMapper mapper, IOptions<KylinOption> kylinOption, IRoleService roleService)
    {
        _mapper = mapper;
        _kylinOption = kylinOption.Value;
        _roleService = roleService;
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    //[HttpPost]
    //public async Task<Result<RoleApiResponse>> GetList(RolesApiRequest request)
    //{
    //    RolesRequest rolesRequest = new()
    //    {
    //        Key = request.Name,
    //        XppId = WorkContext.Xpp.Id,
    //        TenantId = WorkContext.Tenant.Id
    //    };

    //    var result = await _roleService.GetListAsync(rolesRequest);
    //    var response = _mapper.Map<LoginApiResponse>(result);
    //    return Success(response);
    //}
}