//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: PagedList.cs
//摘要: PagedList 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace iMaxSys.Max.Collection
{
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
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            var items = await source.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

            var pagedList = new PagedList<T>()
            {
                Index = pageIndex,
                Size = pageSize,
                Total = count,
                Items = items,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            return pagedList;
        }

        /// <summary>
        /// ToPagedListAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static IPagedList<T> ToPagedList<T>(this List<T> source, int pageIndex, int pageSize, int count, int totalPages)
        {
            return new PagedList<T>()
            {
                Index = pageIndex,
                Size = pageSize,
                Total = count,
                Items = source,
                TotalPages = totalPages
            };
        }

        ///// <summary>
        ///// ToPagedListAsync
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="source"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="cancellationToken"></param>
        ///// <returns></returns>
        //public static async Task<IPagedList<TResult>> ToPagedListAsync<T,TResult>(this IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        //{
        //    var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
        //    var items = await source.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

        //    var pagedList = new PagedList<T>()
        //    {
        //        Index = pageIndex,
        //        Size = pageSize,
        //        Total = count,
        //        Items = items,
        //        TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        //    };

        //    return pagedList;
        //}

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
}
