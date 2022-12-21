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

using System.Data;

using StackExchange.Redis;

using iMaxSys.Max.Caching;
using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.Entities;
using iMaxSys.Data.Repositories;
using iMaxSys.Core.Models;
using iMaxSys.Core.Common;
using iMaxSys.Core.Data.EFCore;
using iMaxSys.Core.Data.Entities;

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

    /// <summary>
    /// 新增字典
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictModel"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public async Task<Dict> AddAsync(long tenantId, DictModel dictModel)
    {
        //重名判断
        bool hasName = await AnyAsync(x => x.TenantId == tenantId && x.Name == dictModel.Name);
        if (hasName)
        {
            throw new MaxException(ResultCode.DictExists);
        }

        Dict dict = Mapper.Map<Dict>(dictModel);
        dict.TenantId = tenantId;
        await AddAsync(dict);

        return dict;
    }

    public async Task<DictItem> AddItemAsync(long tenantId, long dictId, DictItemModel dictItemModel)
    {
        //字典判断
        Dict? dict = await FirstOrDefaultAsync(x => x.TenantId == tenantId && x.Id == dictId, null, x => x.Include(y => y.DictItems));
        if (dict is null)
        {
            throw new MaxException(ResultCode.DictIdIsInvalid);
        }

        if (dict.DictItems is null)
        {
            dict.DictItems = new List<DictItem>();
        }

        //重名判断
        if (dict.DictItems.Any(x => x.Name == dictItemModel.Name))
        {
            throw new MaxException(ResultCode.DictItemExists);
        }

        DictItem dictItem = Mapper.Map<DictItem>(dictItemModel);
        dictItem.TenantId = tenantId;
        dict.DictItems.Add(dictItem);


        //重名判断
        bool hasName = await AnyAsync(x => x.TenantId == tenantId && x.Name == dictItemModel.Name);
        if (hasName)
        {
            throw new MaxException(ResultCode.DictExists);
        }

        //Update(dict);

        return dictItem;
    }

    /// <summary>
    /// AllAsync
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task<IList<Dict>> AllAsync(long tenantId)
    {
        return await AllAsync(x => x.TenantId == tenantId, null, x => x.Include(y => y.DictItems));
    }

    /// <summary>
    /// AllAsync
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public async Task<IList<Dict>> AllAsync(long tenantId, string text)
    {
        return await AllAsync(x => x.TenantId == tenantId && x.Name.Contains(text), null, x => x.Include(y => y.DictItems));
    }

    /// <summary>
    /// 获取字典
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DictModel> GetAsync(long tenantId, long id)
    {
        //取缓存
        DictModel? dictModel = await Cache.GetAsync<DictModel>(GetDictKey(tenantId, id), _global);

        //为空则刷新
        if (dictModel == null)
        {
            dictModel = await RefreshAsync(tenantId, id);
        }

        return dictModel;
    }

    /// <summary>
    /// 获取字典项
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public async Task<DictItemModel> GetItemAsync(long tenantId, long dictId, long id)
    {
        //取缓存
        DictModel dictModel = await GetAsync(tenantId, dictId);
        DictItemModel? dictItemModel = dictModel.DictItems?.FirstOrDefault(x => x.Id == id);

        if (dictItemModel is null)
        {
            throw new MaxException(ResultCode.DictItemIdIsInvalid);
        }

        return dictItemModel;
    }

    /// <summary>
    /// 获取字典项
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<List<DictItemModel>?> GetItemsAsync(long tenantId, long dictId)
    {
        //取缓存
        DictModel dictModel = await GetAsync(tenantId, dictId);
        return dictModel.DictItems;
    }

    /// <summary>
    /// 获取字典项
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictId"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public async Task<List<DictItemModel>?> GetItemsAsync(long tenantId, long dictId, string text)
    {
        var items = await GetItemsAsync(tenantId, dictId);
        return items?.Where(x => x.Name.Contains(text)).ToList();
    }

    /// <summary>
    /// 刷新租户所有字典
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public async Task RefreshAsync(long tenantId)
    {
        var dicts = await AllAsync(x => x.TenantId == tenantId);
        foreach (var dict in dicts)
        {
            await RefreshAsync(tenantId, dict);
        }
    }

    /// <summary>
    /// 刷新字典到缓存
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictId"></param>
    /// <returns></returns>
    public async Task<DictModel> RefreshAsync(long tenantId, long dictId)
    {
        Dict? dict = await FirstOrDefaultAsync(x => x.TenantId == tenantId && x.Id == dictId);
        if (dict is null)
        {
            throw new MaxException(ResultCode.DictIdIsInvalid);
        }
        return await RefreshAsync(tenantId, dict);
    }

    /// <summary>
    /// 刷新字典
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictModel"></param>
    /// <returns></returns>
    private async Task<DictModel> RefreshAsync(long tenantId, Dict dict)
    {
        DictModel dictModel = Mapper.Map<DictModel>(dict);
        await Cache.SetAsync(GetDictKey(tenantId, dictModel.Id), dictModel, new TimeSpan(0, Option.Identity.Expires, 0), _global);
        return dictModel;
    }

    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public async Task RemoveAsync(long tenantId, long id)
    {
        Dict? dict = await FirstOrDefaultAsync(x => x.TenantId == tenantId && x.Id == id);
        if (dict is null)
        {
            throw new MaxException(ResultCode.DictIdIsInvalid);
        }

        if (!dict.Editable)
        {
            throw new MaxException(ResultCode.DictItemCantRemove);
        }

        Remove(id);

        await Cache.DeleteAsync(GetDictKey(tenantId, id), _global);
    }

    /// <summary>
    /// 移除字典项
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public async Task RemoveItemAsync(long tenantId, long dictId, long id)
    {
        //字典判断
        Dict? dict = await FirstOrDefaultAsync(x => x.TenantId == tenantId && x.Id == dictId, null, x => x.Include(y => y.DictItems));
        if (dict is null)
        {
            throw new MaxException(ResultCode.DictIdIsInvalid);
        }

        DictItem? dictItem = dict.DictItems?.FirstOrDefault(x=>x.Id == id);

        if (dictItem is null)
        {
            throw new MaxException(ResultCode.DictItemIdIsInvalid);
        }

        if (!dictItem.Editable)
        {
            throw new MaxException(ResultCode.DictItemCantRemove);
        }

        dict.DictItems?.Remove(dictItem);

        await RefreshAsync(tenantId, dict);
    }

    public async Task UpdateAsync(long tenantId, DictModel dictModel)
    {
        Dict? dict = await FirstOrDefaultAsync(x => x.TenantId == tenantId && x.Id == dictModel.Id);
        if (dict is null)
        {
            throw new MaxException(ResultCode.DictIdIsInvalid);
        }

        if (!dict.Editable)
        {
            throw new MaxException(ResultCode.DictItemCantRemove);
        }

        Mapper.Map(dictModel, dict);

        Update(dict);

        await RefreshAsync(tenantId, dict);
    }

    public async Task UpdateItemAsync(long tenantId, DictItemModel dictItemModel)
    {
        //字典判断
        Dict? dict = await FirstOrDefaultAsync(x => x.TenantId == tenantId && x.Id == dictItemModel.DictId, null, x => x.Include(y => y.DictItems));
        if (dict is null)
        {
            throw new MaxException(ResultCode.DictIdIsInvalid);
        }

        DictItem? dictItem = dict.DictItems?.FirstOrDefault(x => x.Id == dictItemModel.Id);

        if (dictItem is null)
        {
            throw new MaxException(ResultCode.DictItemIdIsInvalid);
        }

        if (!dictItem.Editable)
        {
            throw new MaxException(ResultCode.DictItemCantRemove);
        }

        Mapper.Map(dictItemModel, dictItem);

        Update(dict);

        await RefreshAsync(tenantId, dict);
    }

    /// <summary>
    /// 获取字典key
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictId"></param>
    /// <returns></returns>
    private string GetDictKey(long tenantId, long dictId) => $"{GetTenantKey(tenantId)}{Cache.Separator}{dictId}";

    /// <summary>
    /// 获取租户key
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    private string GetTenantKey(long tenantId) => $"{_tagDict}{Cache.Separator}{tenantId}";
}