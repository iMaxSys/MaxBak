//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: PagedListExtension.cs
//摘要: 分页扩展 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2021-11-24
//----------------------------------------------------------------

namespace iMaxSys.Max.Collection;

/// <summary>
/// IQueryablePagedListExtension
/// </summary>
public static class PagedListExtension
{
    /// <summary>
    /// ToPagedListAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="index"></param>
    /// <param name="size"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int index, int size, CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
        var items = await source.Skip(index * size).Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);
        return new PagedList<T>(items, index, size, count);
    }

    /// <summary>
    /// Converts the specified source to <see cref="IPagedList{T}"/> by the specified <paramref name="pageIndex"/> and <paramref name="pageSize"/>.
    /// </summary>
    /// <typeparam name="T">The type of the source.</typeparam>
    /// <param name="source">The source to paging.</param>
    /// <param name="pageIndex">The index of the page.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="indexFrom">The start index value.</param>
    /// <returns>An instance of the inherited from <see cref="IPagedList{T}"/> interface.</returns>
    public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int index, int size) => new PagedList<T>(source, index, size);
}
