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

namespace iMaxSys.Core.Services;

/// <summary>
/// 核心服务接口
/// </summary>
public class CoreService : ICoreService
{
    private readonly IMapper _mapper;
    private readonly MaxOption _option;
    private readonly IUnitOfWork _unitOfWork;

    public CoreService(IMapper mapper, IOptions<MaxOption> option, IUnitOfWork<CoreContext, CoreReadOnlyContext> unitOfWork)
    {
        _mapper = mapper;
        _option = option.Value;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// 获取xpp
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<XppModel> GetXppAsync(long id)
    {
        Xpp? xpp = await _unitOfWork.GetReadOnlyRepository<Xpp>().FindAsync(id);

        if (xpp is null)
        {
            throw new MaxException(ResultCode.XppIsInvalid);
        }

        return _mapper.Map<XppModel>(xpp);
    }

    /// <summary>
    /// 获取xppSns
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<XppSnsModel> GetXppSnsAsync(long id)
    {
        XppSns? xppSns = await _unitOfWork.GetReadOnlyRepository<XppSns>().FirstOrDefaultAsync(x => x.Id == id, null, x => x.Include(y => y.Xpp));

        if (xppSns is null)
        {
            throw new MaxException(ResultCode.XppSnsIsInvalid);
        }

        return _mapper.Map<XppSnsModel>(xppSns);
    }

    /// <summary>
    /// 刷新缓存
    /// </summary>
    /// <returns></returns>
    public async Task RefreshAsync()
    {

    }
}