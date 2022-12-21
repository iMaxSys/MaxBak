//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: DictService.cs
//摘要: DictService 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-29
//----------------------------------------------------------------

using iMaxSys.Max.Caching;
using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Data;
using iMaxSys.Core.Models;
using iMaxSys.Core.Common;
using iMaxSys.Core.Data.EFCore;
using iMaxSys.Core.Data.Entities;
using iMaxSys.Core.Data.Repositories;

namespace iMaxSys.Core.Services;

/// <summary>
/// DictService
/// </summary>
public class DictService : IDictService
{
    private readonly IMapper _mapper;
    private readonly MaxOption _option;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICache _cache;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="option"></param>
    /// <param name="unitOfWork"></param>
    public DictService(IMapper mapper, IOptions<MaxOption> option, IUnitOfWork<CoreContext, CoreReadOnlyContext> unitOfWork, ICacheFactory cacheFactory)
    {
        _mapper = mapper;
        _option = option.Value;
        _unitOfWork = unitOfWork;
        _cache = cacheFactory.GetService();
    }

    /// <summary>
    /// 新增字典
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictModel"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<DictModel> AddDictAsync(long tenantId, DictModel dictModel)
    {
        var repository = _unitOfWork.GetCustomRepository<IDictRepository>();

        var dict = await repository.AddAsync(tenantId, dictModel);
        await _unitOfWork.SaveChangesAsync();
        dictModel.Id = dict.Id;

        return dictModel;
    }

    /// <summary>
    /// 新增字典项
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictItemModel"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<DictItemModel> AddItemAsync(long tenantId, long dictId, DictItemModel dictItemModel)
    {
        var repository = _unitOfWork.GetCustomRepository<IDictRepository>();

        var dictItem = await repository.AddItemAsync(tenantId, dictId, dictItemModel);
        await _unitOfWork.SaveChangesAsync();
        dictItemModel.Id = dictItem.Id;

        return dictItemModel;
    }

    /// <summary>
    /// 获取字典
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<DictModel> GetDictAsync(long tenantId, long id)
    {
       return await _unitOfWork.GetCustomRepository<IDictRepository>().GetAsync(tenantId, id);
    }

    public async Task<List<DictModel>> GetDictsAsync(long tenantId)
    {
        var list = await _unitOfWork.GetCustomRepository<IDictRepository>().AllAsync(tenantId);
        return _mapper.Map<List<DictModel>>(list);
    }

    public async Task<DictItemModel> GetItemAsync(long tenantId, long dictId, long id)
    {
        return await _unitOfWork.GetCustomRepository<IDictRepository>().GetItemAsync(tenantId, dictId, id);
    }

    public async Task<List<DictItemModel>?> GetItemsAsync(long tenantId, long dictId)
    {
        return await _unitOfWork.GetCustomRepository<IDictRepository>().GetItemsAsync(tenantId, dictId);
    }

    public async Task RefreshAysnc(long tenantId)
    {
        await _unitOfWork.GetCustomRepository<IDictRepository>().RefreshAsync(tenantId);
    }

    public async Task RemoveDictAsysnc(long tenantId, long id)
    {
        await _unitOfWork.GetCustomRepository<IDictRepository>().RemoveAsync(tenantId, id);
    }

    public async Task RemoveItemAsysnc(long tenantId, long dictId, long id)
    {
        await _unitOfWork.GetCustomRepository<IDictRepository>().RemoveItemAsync(tenantId, dictId, id);
    }

    public async Task UpdateDictAsync(long tenantId, DictModel dictModel)
    {
        await _unitOfWork.GetCustomRepository<IDictRepository>().UpdateAsync(tenantId, dictModel);
    }

    public async Task UpdateItemAsync(long tenantId, DictItemModel dictItemModel)
    {
        await _unitOfWork.GetCustomRepository<IDictRepository>().UpdateItemAsync(tenantId, dictItemModel);
    }
}

