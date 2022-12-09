//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: DictRepository.cs
//摘要: 字典仓储
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Caching;
using iMaxSys.Max.Options;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.Entities;
using iMaxSys.Data.Repositories;
using iMaxSys.Core.Models;
using iMaxSys.Core.Data.Entities;
using iMaxSys.Core.Data.EFCore;

namespace iMaxSys.Core.Data.Repositories;

/// <summary>
/// 字典仓储
/// </summary>
public class DictRepository : CoreRepository<Dict>, IDictRepository
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    /// <param name="option"></param>
    /// <param name="cacheFactory"></param>
    public DictRepository(CoreContext context, IMapper mapper, IOptions<MaxOption> option, ICacheFactory cacheFactory) : base(context, mapper, option, cacheFactory)
    {
    }

    public Task<DictModel> AddAsync(long tenantId, DictModel dictModel)
    {
        throw new NotImplementedException();
    }

    public Task<DictItemModel> AddItemAsync(long tenantId, DictModel dictModel)
    {
        throw new NotImplementedException();
    }

    public Task<List<Dict>> AllAsync(long tenantId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Dict>> AllAsync(long tenantId, string text)
    {
        throw new NotImplementedException();
    }

    public Task<Dict> GetAsync(long tenantId, long id)
    {
        throw new NotImplementedException();
    }

    public Task<DictItem> GetItemAsync(long tenantId, long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<DictItem>?> GetItemsAsync(long tenantId, long dictId)
    {
        throw new NotImplementedException();
    }

    public Task<List<DictItem>?> GetItemsAsync(long tenantId, long dictId, string text)
    {
        throw new NotImplementedException();
    }

    public Task<Dict> RefreshAsync(long tenantId)
    {
        throw new NotImplementedException();
    }

    public Task<Dict> RefreshAsync(long tenantId, long dictId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(long tenantId, long id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveItemAsync(long tenantId, long id)
    {
        throw new NotImplementedException();
    }

    public Task<DictModel> UpdateAsync(long tenantId, DictModel dictModel)
    {
        throw new NotImplementedException();
    }

    public Task<DictItemModel> UpdateItemAsync(long tenantId, DictItemModel dictItemModel)
    {
        throw new NotImplementedException();
    }
}