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

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="option"></param>
    /// <param name="unitOfWork"></param>
    public DictService(IMapper mapper, IOptions<MaxOption> option, IUnitOfWork<CoreContext, CoreReadOnlyContext> unitOfWork)
    {
        _mapper = mapper;
        _option = option.Value;
        _unitOfWork = unitOfWork;
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

        await repository.RefreshAsync(tenantId, dict);

        return dictModel;


        //var repository = _unitOfWork.GetRepository<Dict>();

        ////重名判断
        //bool hasName = await repository.AnyAsync(x => x.TenantId == tenantId && x.Name == dictModel.Name);
        //if (hasName)
        //{
        //    throw new MaxException(ResultCode.DictExists);
        //}

        //Dict dict = _mapper.Map<Dict>(dictModel);
        //dict.TenantId = tenantId;
        //await repository.AddAsync(dict);
        //await _unitOfWork.SaveChangesAsync();

        //await repository.RefreshAsync(tenantId, dict);

        //dictModel.Id = dict.Id;

        //return dictModel;
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

        await repository.RefreshAsync(tenantId, dictId);

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
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveItemAsysnc(long tenantId, long id)
    {
        var repository = _unitOfWork.GetRepository<DictItem>();

        //字典判断
        DictItem? dictItem = await repository.FirstOrDefaultAsync(x => x.TenantId == tenantId && x.Id == id);
        DictItem? x = await repository.FirstOrDefaultAsync();

        if (dictItem is null)
        {
            throw new MaxException(ResultCode.DictItemIdIsInvalid);
        }

        if (!dictItem.Editable)
        {
            throw new MaxException(ResultCode.DictItemCantRemove);
        }

        repository.Delete(dictItem);
        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.GetCustomRepository<IDictRepository>().RefreshAsync(tenantId, dictItem.DictId);
    }

    public async Task UpdateDictAsync(long tenantId, DictModel dictModel)
    {
        await _unitOfWork.GetCustomRepository<IDictRepository>().UpdateAsync(tenantId, dictModel);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateItemAsync(long tenantId, DictItemModel dictItemModel)
    {
        await _unitOfWork.GetCustomRepository<IDictRepository>().UpdateItemAsync(tenantId, dictItemModel);
        await _unitOfWork.SaveChangesAsync();
    }
}

