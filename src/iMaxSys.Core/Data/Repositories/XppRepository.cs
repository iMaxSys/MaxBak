//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: XppRepository.cs
//摘要: 应用仓储
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Core.Common;
using iMaxSys.Core.Data.EFCore;
using iMaxSys.Core.Data.Entities;
using iMaxSys.Core.Models;
using iMaxSys.Max.Caching;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.Options;

namespace iMaxSys.Core.Data.Repositories;

/// <summary>
/// 应用仓储
/// </summary>
public class XppRepository : CoreRepository<Xpp>, IXppRepository
{
    private const string TAG = "x";
    private const string TAG_XPP = "x";
    private const string TAG_SNS = "s";

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    /// <param name="option"></param>
    /// <param name="cacheFactory"></param>
    public XppRepository(CoreContext context, IMapper mapper, IOptions<MaxOption> option, ICacheFactory cacheFactory) : base(context, mapper, option, cacheFactory)
    {
    }

    /// <summary>
    /// 获取信用信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<XppModel> GetXppAsync(long id)
    {
        //取缓存
        XppModel? model = await Cache.GetAsync<XppModel>(GetXppKey(id), _global);

        //为空则刷新
        if (model is null)
        {
            model = await RefreshXppAsync(id);
        }

        return model;
    }

    public async Task<XppSnsModel> GetSnsAsync(long id)
    {
        //取缓存
        XppSnsModel? model = await Cache.GetAsync<XppSnsModel>(GetSnsKey(id), _global);

        //为空则刷新
        if (model is null)
        {
            model = await RefreshSnsAsync(id);
        }

        return model;
    }

    /// <summary>
    /// 刷新全部应用信息
    /// </summary>
    /// <returns></returns>
    public async Task RefreshAsync()
    {
        var xpps = await AllAsync(include: x => x.Include(y => y.XppSnses), disableTracking: true);
        foreach (var item in xpps)
        {
            await RefreshXppAsync(item);
            await RefreshSnsesAsync(item.XppSnses);
        }
    }

    /// <summary>
    /// 刷新应用信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<XppModel> RefreshXppAsync(long id)
    {
        var xpp = await FirstOrDefaultAsync(x => x.Id == id);
        return await RefreshXppAsync(xpp);
    }

    /// <summary>
    /// 刷新应用信息
    /// </summary>
    /// <param name="xpp"></param>
    /// <returns></returns>
    public async Task<XppModel> RefreshXppAsync(Xpp? xpp)
    {
        if (xpp is null)
        {
            throw new MaxException(ResultCode.XppIsInvalid);
        }
        var model = Mapper.Map<XppModel>(xpp);
        await Cache.SetAsync(GetXppKey(xpp.Id), model, new TimeSpan(0, Option.Identity.Expires, 0), _global);

        return model;
    }

    /// <summary>
    /// 刷新应用社交信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<XppSnsModel> RefreshSnsAsync(long id)
    {
        var xpp = await FirstOrDefaultAsync(x => x.XppSnses != null && x.XppSnses.Any(y => y.Id == id), null, x => x.Include(y => y.XppSnses));

        if (xpp is null)
        {
            throw new MaxException(ResultCode.XppSnsIsInvalid);
        }

        var xppSns = xpp?.XppSnses?.FirstOrDefault(x => x.Id == id);

        return await RefreshSnsAsync(xppSns);
    }

    /// <summary>
    /// 刷新应用社交信息
    /// </summary>
    /// <param name="xppSnses"></param>
    /// <returns></returns>
    public async Task<XppSnsModel> RefreshSnsAsync(XppSns? xppSns)
    {
        if (xppSns is null)
        {
            throw new MaxException(ResultCode.XppSnsIsInvalid);
        }

        var model = Mapper.Map<XppSnsModel>(xppSns);
        await Cache.SetAsync(GetSnsKey(xppSns.Id), model, new TimeSpan(0, Option.Identity.Expires, 0), _global);
        return model;
    }

    /// <summary>
    /// 刷新应用社交信息
    /// </summary>
    /// <param name="xppSnses"></param>
    /// <returns></returns>
    public async Task RefreshSnsesAsync(ICollection<XppSns>? xppSnses)
    {
        if (xppSnses is not null)
        {
            foreach (var xppSns in xppSnses)
            {
                await RefreshSnsAsync(xppSns);
            }
        }
    }

    /// <summary>
    /// 获取xpp key
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private string GetXppKey(long id) => $"{TAG}{Cache.Separator}{TAG_XPP}{Cache.Separator}{id}";

    /// <summary>
    /// 获取sns key
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private string GetSnsKey(long id) => $"{TAG}{Cache.Separator}{TAG_SNS}{Cache.Separator}{id}";
}

