//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: CoreService.cs
//摘要: 核心服务
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-29
//----------------------------------------------------------------

using iMaxSys.Data;
using iMaxSys.Core.Models;
using iMaxSys.Core.Data.EFCore;
using iMaxSys.Core.Data.Entities;

using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.DependencyInjection;
using iMaxSys.Core.Common;
using iMaxSys.Core.Data.Repositories;

namespace iMaxSys.Core.Services;

/// <summary>
/// 核心服务接口
/// </summary>
public class CoreService : ICoreService
{
    private readonly IMapper _mapper;
    private readonly MaxOption _option;
    private readonly IXppRepository _xppRepository;

    public CoreService(IMapper mapper, IOptions<MaxOption> option, IXppRepository xppRepository)
    {
        _mapper = mapper;
        _option = option.Value;
        _xppRepository = xppRepository;
    }

    /// <summary>
    /// 获取xpp
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<XppModel> GetXppAsync(long id)
    {
        return await _xppRepository.GetXppAsync(id);
    }

    /// <summary>
    /// 获取xppSns
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<XppSnsModel> GetXppSnsAsync(long id)
    {
        return await _xppRepository.GetSnsAsync(id);
    }

    /// <summary>
    /// 刷新缓存
    /// </summary>
    /// <returns></returns>
    public async Task RefreshAsync()
    {
        await _xppRepository.RefreshAsync();
    }
}