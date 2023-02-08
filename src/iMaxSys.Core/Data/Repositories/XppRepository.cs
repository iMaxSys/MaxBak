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

using iMaxSys.Max;
using iMaxSys.Max.Caching;
using iMaxSys.Max.Options;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Core.Common;
using iMaxSys.Core.Models;
using iMaxSys.Core.Data.EFCore;
using DbXpp = iMaxSys.Core.Data.Entities.Xpp;
using DbXppSns = iMaxSys.Core.Data.Entities.XppSns;

namespace iMaxSys.Core.Data.Repositories;

/// <summary>
/// 应用仓储
/// </summary>
public class XppRepository : CoreReadOnlyRepository<DbXpp>, IXppRepository
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
    public XppRepository(CoreReadOnlyContext context, IMapper mapper, IOptions<MaxOption> option, ICacheFactory cacheFactory) : base(context, mapper, option, cacheFactory)
    {
    }

    /// <summary>
    /// 获取信用信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Xpp> GetXppAsync(long id)
    {
        //取缓存
        Xpp? xpp = await Cache.GetAsync<Xpp>(GetXppKey(id), _global);

        //为空则刷新
        if (xpp is null)
        {
            xpp = await RefreshXppAsync(id);
        }

        return xpp;
    }

    public async Task<XppSns> GetSnsAsync(long id)
    {
        //取缓存
        XppSns? xppSns = await Cache.GetAsync<XppSns>(GetSnsKey(id), _global);

        //为空则刷新
        if (xppSns is null)
        {
            xppSns = await RefreshSnsAsync(id);
        }

        return xppSns;
    }

    /// <summary>
    /// 刷新全部应用信息
    /// </summary>
    /// <returns></returns>
    public async Task RefreshAsync()
    {
        var xpps = await AllAsync(include: x => x.Include(y => y.XppSnses));
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
    public async Task<Xpp> RefreshXppAsync(long id)
    {
        var xpp = await FirstOrDefaultAsync(x => x.Id == id);
        return await RefreshXppAsync(xpp);
    }

    /// <summary>
    /// 刷新应用信息
    /// </summary>
    /// <param name="xpp"></param>
    /// <returns></returns>
    public async Task<Xpp> RefreshXppAsync(DbXpp? dbXpp)
    {
        if (dbXpp is null)
        {
            throw new MaxException(ResultCode.XppIsInvalid);
        }
        var xpp = Mapper.Map<Xpp>(dbXpp);
        await Cache.SetAsync(GetXppKey(xpp.Id), xpp, new TimeSpan(0, Option.Identity.Expires, 0), _global);

        return xpp;
    }

    /// <summary>
    /// 刷新应用社交信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<XppSns> RefreshSnsAsync(long id)
    {
        var dbXpp = await FirstOrDefaultAsync(x => x.XppSnses != null && x.XppSnses.Any(y => y.Id == id), null, x => x.Include(y => y.XppSnses));

        if (dbXpp is null)
        {
            throw new MaxException(ResultCode.XppSnsIsInvalid);
        }

        var dbXppSns = dbXpp?.XppSnses?.FirstOrDefault(x => x.Id == id);

        return await RefreshSnsAsync(dbXppSns);
    }

    /// <summary>
    /// 刷新应用社交信息
    /// </summary>
    /// <param name="xppSnses"></param>
    /// <returns></returns>
    public async Task<XppSns> RefreshSnsAsync(DbXppSns? dbXppSns)
    {
        if (dbXppSns is null)
        {
            throw new MaxException(ResultCode.XppSnsIsInvalid);
        }

        var xppSns = Mapper.Map<XppSns>(dbXppSns);
        await Cache.SetAsync(GetSnsKey(xppSns.Id), xppSns, new TimeSpan(0, Option.Identity.Expires, 0), _global);
        return xppSns;
    }

    /// <summary>
    /// 刷新应用社交信息
    /// </summary>
    /// <param name="xppSnses"></param>
    /// <returns></returns>
    public async Task RefreshSnsesAsync(ICollection<DbXppSns>? dbXppSnses)
    {
        if (dbXppSnses is not null)
        {
            foreach (var dbXppSns in dbXppSnses)
            {
                await RefreshSnsAsync(dbXppSns);
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

