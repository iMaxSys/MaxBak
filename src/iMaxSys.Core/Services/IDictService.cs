//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IDictService.cs
//摘要: 字典服务接口 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-29
//----------------------------------------------------------------

using iMaxSys.Core.Models;
using iMaxSys.Max.DependencyInjection;

namespace iMaxSys.Core.Services;

/// <summary>
/// IDictService
/// </summary>
public interface IDictService : IDependency
{
	/// <summary>
	/// 获取字典
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	Task<DictModel> GetDictAsync(long id);

    /// <summary>
    /// 获取字典列表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<List<DictModel>> GetDictsAsync(long tenantId);

    /// <summary>
    /// 新增字典
    /// </summary>
    /// <param name="dictModel"></param>
    /// <returns></returns>
    Task<DictModel> AddDictAsync(DictModel dictModel);

    /// <summary>
    /// 移除字典
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveDictAsysnc(long id);

    /// <summary>
    /// 修改字典
    /// </summary>
    /// <param name="dictModel"></param>
    /// <returns></returns>
    Task<DictModel> UpdateDictAsync(DictModel dictModel);

    /// <summary>
	/// 获取字典项
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	Task<DictItemModel> GetItemAsync(long id);

    /// <summary>
    /// 获取字典项列表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<List<DictItemModel>> GetItemsAsync(long tenantId);

    /// <summary>
    /// 新增字典项
    /// </summary>
    /// <param name="dictItemModel"></param>
    /// <returns></returns>
    Task<DictItemModel> AddItemAsync(DictItemModel dictItemModel);

    /// <summary>
    /// 移除字典
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveItemAsysnc(long id);

    /// <summary>
    /// 修改字典
    /// </summary>
    /// <param name="dictItemModel"></param>
    /// <returns></returns>
    Task<DictModel> UpdateItemAsync(DictItemModel dictItemModel);

    /// <summary>
    /// 刷新字典
    /// </summary>
    /// <returns></returns>
    Task RefreshAsync();

    /// <summary>
    /// 刷新租户字典
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    Task RefreshAysnc(long tenantId);
}

