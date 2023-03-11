//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MenuController.cs
//摘要: 菜单控制器
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
/// 菜单
/// </summary>
public class MenuController : MaxController
{
    private readonly IMapper _mapper;
    private readonly KylinOption _kylinOption;
    private readonly IMenuService _menuService;

    /// <summary>
    /// 菜单
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="kylinOption"></param>
    /// <param name="menuService"></param>
    public MenuController(IMapper mapper, IOptions<KylinOption> kylinOption, IMenuService menuService)
    {
        _mapper = mapper;
        _kylinOption = kylinOption.Value;
        _menuService = menuService;
    }

    /// <summary>
    /// 获取租户菜单ids
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<Result<MenuIdsApiResponse>> Get()
    {
        var result = await _menuService.GetXppMenuIdsAsync(WorkContext.Tenant.Id, WorkContext.Xpp.Id);
        var response = _mapper.Map<MenuIdsApiResponse>(result);
        return Success(response);
    }
}