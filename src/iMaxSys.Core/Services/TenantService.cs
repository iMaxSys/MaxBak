//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: TenantService.cs
//摘要: 租户服务
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-29
//----------------------------------------------------------------

using iMaxSys.Max;
using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.DependencyInjection;
using iMaxSys.Data;
using iMaxSys.Core.Models;
using iMaxSys.Core.Data.EFCore;
using iMaxSys.Core.Common;
using iMaxSys.Core.Data.Repositories;
using iMaxSys.Max.Caching;

namespace iMaxSys.Core.Services;

/// <summary>
/// 租户服务
/// </summary>
public class TenantService : ITenantService
{
    private readonly IMapper _mapper;
    private readonly MaxOption _option;
    private readonly ITenantRepository _tenantRepository;

    public TenantService(IMapper mapper, IOptions<MaxOption> option, ITenantRepository tenantRepository)
    {
        _mapper = mapper;
        _option = option.Value;
        _tenantRepository = tenantRepository;
    }

    /// <summary>
    /// 获取租户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<Tenant> GetAsync(long id)
    {
        return _tenantRepository.GetAsync(id);
    }
}

