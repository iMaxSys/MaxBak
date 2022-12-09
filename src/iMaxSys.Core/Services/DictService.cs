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

using iMaxSys.Data;
using iMaxSys.Core.Models;
using iMaxSys.Core.Common;
using iMaxSys.Core.Data.EFCore;
using iMaxSys.Core.Data.Entities;
using iMaxSys.Max.Caching;
using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;

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
        var repository = _unitOfWork.GetRepository<Dict>();
        //存在/重名判断
        bool hasName = await repository.AnyAsync(x=>x.TenantId == tenantId && x.Name == dictModel.Name);
        if (hasName)
        {
            throw new MaxException(ResultCode.DictExists);
        }

        Dict dict = _mapper.Map<Dict>(dictModel);
        dict.TenantId = tenantId;
        await repository.AddAsync(dict);
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
    public async Task<DictItemModel> AddItemAsync(long tenantId, DictItemModel dictItemModel)
    {
        var repository = _unitOfWork.GetRepository<DictItem>();
        //存在/重名判断
        bool hasName = await repository.AnyAsync(x => x.TenantId == tenantId && x.DictId == dictItemModel.DictId && x.Name == dictItemModel.Name);
        if (hasName)
        {
            throw new MaxException(ResultCode.DictItemExists);
        }

        DictItem dictItem = _mapper.Map<DictItem>(dictItemModel);
        dictItem.TenantId = tenantId;
        await repository.AddAsync(dictItem);
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
    public Task<DictModel> GetDictAsync(long tenantId, long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<DictModel>> GetDictsAsync(long tenantId)
    {
        throw new NotImplementedException();
    }

    public Task<DictItemModel> GetItemAsync(long tenantId, long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<DictItemModel>> GetItemsAsync(long tenantId)
    {
        throw new NotImplementedException();
    }

    public Task<List<DictItemModel>> GetItemsAsync(long tenantId, long dictId)
    {
        throw new NotImplementedException();
    }

    public Task RefreshAsync()
    {
        throw new NotImplementedException();
    }

    public Task RefreshAysnc(long tenantId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveDictAsysnc(long tenantId, long id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveItemAsysnc(long tenantId, long id)
    {
        throw new NotImplementedException();
    }

    public Task<DictModel> UpdateDictAsync(long tenantId, DictModel dictModel)
    {
        throw new NotImplementedException();
    }

    public Task<DictModel> UpdateItemAsync(long tenantId, DictItemModel dictItemModel)
    {
        throw new NotImplementedException();
    }
}

