//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ITenantRepository.cs
//摘要: 租户仓储接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max;
using iMaxSys.Max.Options;
using iMaxSys.Max.Caching;
using iMaxSys.Max.Exceptions;
using iMaxSys.Core.Models;
using iMaxSys.Core.Common;
using iMaxSys.Core.Data.EFCore;
using iMaxSys.Core.Data.Repositories;
using DbXpp = iMaxSys.Core.Data.Entities.Xpp;
using DbXppSns = iMaxSys.Core.Data.Entities.XppSns;
using DbTenant = iMaxSys.Core.Data.Entities.Tenant;

namespace iMaxSys.Core.Data.Repositories;

/// <summary>
/// 应用仓储接口
/// </summary>
public class TenantRepository : CoreReadOnlyRepository<DbTenant>, ITenantRepository
{
    private const string TAG = "x";
    private const string TAG_TENANT = "t";

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    /// <param name="option"></param>
    /// <param name="cacheFactory"></param>
    public TenantRepository(CoreReadOnlyContext context, IMapper mapper, IOptions<MaxOption> option, ICacheFactory cacheFactory) : base(context, mapper, option, cacheFactory)
    {
    }

    /// <summary>
    /// 获取租户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Tenant> GetAsync(long id)
    {
        //取缓存
        Tenant? tenant = await Cache.GetAsync<Tenant>(GetKey(id), _global);

        //为空则刷新
        if (tenant is null)
        {
            tenant = await RefreshAsync(id);
        }

        return tenant;
    }

    /// <summary>
    /// 刷新全部租户信息
    /// </summary>
    /// <returns></returns>
    public async Task RefreshAsync()
    {
        var tenants = await AllAsync(default);
        foreach (var item in tenants)
        {
            await RefreshAsync(item);
        }
    }

    /// <summary>
    /// 刷新租户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Tenant> RefreshAsync(long id)
    {
        var tenant = await FirstOrDefaultAsync(x => x.Id == id);
        return await RefreshAsync(tenant);
    }

    /// <summary>
    /// 刷新刷新租户信息
    /// </summary>
    /// <param name="dbTenant"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    private async Task<Tenant> RefreshAsync(DbTenant? dbTenant)
    {
        if (dbTenant is null)
        {
            throw new MaxException(ResultCode.TenantIsInvalid);
        }
        Tenant tenant = Mapper.Map<Tenant>(dbTenant);
        await Cache.SetAsync(GetKey(tenant.Id), tenant, new TimeSpan(0, Option.Identity.Expires, 0), _global);

        return tenant;
    }

    /// <summary>
    /// 获取key
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private string GetKey(long id) => $"{TAG}{Cache.Separator}{TAG_TENANT}{Cache.Separator}{id}";
}
