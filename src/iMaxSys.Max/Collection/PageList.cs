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
using System.Linq;
using System.Collections.Generic;

namespace iMaxSys.Max.Collection
{
    /// <summary>
    /// PagedList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : IPagedList<T>
    {
        /// <summary>
        /// Gets the page index (current).
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets the page size.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets the total count of the list of type <typeparamref name="T"/>
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets the total pages.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets the has previous page.
        /// </summary>
        /// <value>The has previous page.</value>
        public bool HasPrevious => Index > 0;

        /// <summary>
        /// Gets the has next page.
        /// </summary>
        /// <value>The has next page.</value>
        public bool HasNext => Index < TotalPages - 1;

        /// <summary>
        /// Gets the current page items. 考虑性能问题，不用IList用List
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}" /> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="index">The index of the page.</param>
        /// <param name="size">The size of the page.</param>
        internal PagedList(IEnumerable<T> source, int index, int size)
        {
            if (source is IQueryable<T> querable)
            {
                Index = index;
                Size = size;
                Total = querable.Count();
                TotalPages = (int)Math.Ceiling(Total / (double)Size);
                Items = querable.Skip((Index * Size)).Take(Size).ToList();
            }
            else
            {
                Index = index;
                Size = size;
                Total = source.Count();
                TotalPages = (int)Math.Ceiling(Total / (double)Size);

                Items = source.Skip((Index * Size)).Take(Size).ToList();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}" /> class.
        /// </summary>
        internal PagedList() => Items = new List<T>();
    }
}
