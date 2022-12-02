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

using iMaxSys.Core.Data.EFCore;
using iMaxSys.Core.Models;
using iMaxSys.Data;
using iMaxSys.Max.Options;

namespace iMaxSys.Core.Services;

/// <summary>
/// DictService
/// </summary>
public class DictService : IDictService
{
    private readonly IMapper _mapper;
    private readonly MaxOption _option;
    private readonly IUnitOfWork _unitOfWork;

    public DictService(IMapper mapper, IOptions<MaxOption> option, IUnitOfWork<CoreContext, CoreReadOnlyContext> unitOfWork)
    {
        _mapper = mapper;
        _option = option.Value;
        _unitOfWork = unitOfWork;
    }

    public Task<DictModel> AddDictAsync(DictModel dictModel)
    {
        throw new NotImplementedException();
    }

    public Task<DictItemModel> AddItemAsync(DictItemModel dictItemModel)
    {
        throw new NotImplementedException();
    }

    public Task<DictModel> GetDictAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<DictModel>> GetDictsAsync(long tenantId)
    {
        throw new NotImplementedException();
    }

    public Task<DictItemModel> GetItemAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<DictItemModel>> GetItemsAsync(long tenantId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveDictAsysnc(long id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveItemAsysnc(long id)
    {
        throw new NotImplementedException();
    }

    public Task<DictModel> UpdateDictAsync(DictModel dictModel)
    {
        throw new NotImplementedException();
    }

    public Task<DictModel> UpdateItemAsync(DictItemModel dictItemModel)
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
}

