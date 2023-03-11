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
using iMaxSys.Max.Collection;
using iMaxSys.Identity;
using iMaxSys.Identity.Models;

using Kylin.Services.Auth;
using Kylin.Framework.Options;
using Kylin.Api.Admin.ViewModels;

namespace Kylin.Api.Client.Controllers;

/// <summary>
/// 角色
/// </summary>
public class RoleController : MaxController
{
    private readonly IMapper _mapper;
    private readonly KylinOption _kylinOption;
    private readonly IRoleService _roleService;

    /// <summary>
    /// 角色
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="kylinOption"></param>
    /// <param name="roleService"></param>
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
    [HttpPost]
    public async Task<Result<PagedList<RoleApiResponse>>> GetList([FromBody] RolesApiRequest request)
    {
        RolesRequest rolesRequest = new()
        {
            XppId = WorkContext.Xpp.Id,
            TenantId = WorkContext.Tenant.Id,
            Key = request.Name,
            Index = request.Index,
            Size = request.Size,
        };

        var result = await _roleService.GetListAsync(rolesRequest);
        var response = _mapper.Map<PagedList<RoleApiResponse>>(result);
        return Success(response);
    }

    /// <summary>
    /// 获取角色
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Result<RoleApiResponse>> Get([FromBody] RoleApiRequest request)
    {
        RoleRequest roleRequest = new()
        {
            XppId = WorkContext.Xpp.Id,
            TenantId = WorkContext.Tenant.Id,
            Id = request.Id
        };

        var result = await _roleService.GetAsync(roleRequest);
        var response = _mapper.Map<RoleApiResponse>(result);
        return Success(response);
    }

    /// <summary>
    /// 新增角色
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Result<RoleApiResponse>> Add([FromBody] AddRoleApiRequest request)
    {
        var model = _mapper.Map<AddRoleRequest>(request);
        model.XppId = WorkContext.Xpp.Id;
        model.TenantId = WorkContext.Tenant.Id;

        var result = await _roleService.AddAsync(model);
        var response = _mapper.Map<RoleApiResponse>(result);
        return Success(response);
    }

    /// <summary>
    /// 修改角色
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Result<RoleApiResponse>> Update([FromBody] UpdateRoleApiRequest request)
    {
        var model = _mapper.Map<UpdateRoleRequest>(request);
        model.XppId = WorkContext.Xpp.Id;
        model.TenantId = WorkContext.Tenant.Id;

        var result = await _roleService.UpdateAsync(model);
        var response = _mapper.Map<RoleApiResponse>(result);
        return Success(response);
    }
}