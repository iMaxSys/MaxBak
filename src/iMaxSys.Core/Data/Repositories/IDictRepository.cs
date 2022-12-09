﻿//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IDictRepository.cs
//摘要: 字典仓储接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.Entities;
using iMaxSys.Data.Repositories;
using iMaxSys.Core.Models;
using iMaxSys.Core.Data.Entities;

namespace iMaxSys.Core.Data.Repositories;

/// <summary>
/// 字典仓储接口
/// </summary>
public interface IDictRepository : ICoreRepository<Dict>
{
    /// <summary>
    /// 获取租户所有字典
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    Task<List<Dict>> AllAsync(long tenantId);

    /// <summary>
    /// 获取租户字典列表
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    Task<List<Dict>> AllAsync(long tenantId, string text);

    /// <summary>
    /// 获取租户字典
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Dict> GetAsync(long tenantId, long id);

    /// <summary>
    /// 获取租户字典项
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<DictItem> GetItemAsync(long tenantId, long id);

    /// <summary>
    /// 获取租户字典项列表
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictId"></param>
    /// <returns></returns>
    Task<List<DictItem>?> GetItemsAsync(long tenantId, long dictId);

    /// <summary>
    /// 获取租户字典项列表
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictId"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    Task<List<DictItem>?> GetItemsAsync(long tenantId, long dictId, string text);

    /// <summary>
    /// 新增字典
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictModel"></param>
    /// <returns></returns>
    Task<DictModel> AddAsync(long tenantId, DictModel dictModel);

    /// <summary>
    /// 新增字典项
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictModel"></param>
    /// <returns></returns>
    Task<DictItemModel> AddItemAsync(long tenantId, DictModel dictModel);

    /// <summary>
    /// 移除字典
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveAsync(long tenantId, long id);

    /// <summary>
    /// 移除字典项
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveItemAsync(long tenantId, long id);

    /// <summary>
    /// 更新字典
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictModel"></param>
    /// <returns></returns>
    Task<DictModel> UpdateAsync(long tenantId, DictModel dictModel);

    /// <summary>
    /// 更新字典项
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictItemModel"></param>
    /// <returns></returns>
    Task<DictItemModel> UpdateItemAsync(long tenantId, DictItemModel dictItemModel);

    /// <summary>
    /// refresh
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    Task<Dict> RefreshAsync(long tenantId);

    /// <summary>
    /// refresh
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="dictId"></param>
    /// <returns></returns>
    Task<Dict> RefreshAsync(long tenantId, long dictId);
}