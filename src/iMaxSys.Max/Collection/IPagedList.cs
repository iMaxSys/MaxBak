//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IPagedList.cs
//摘要: IPagedList 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using System.Collections.Generic;

namespace iMaxSys.Max.Collection
{
    /// <summary>
    /// IPagedList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedList<T>
    {
        /// <summary>
        /// Gets the page index (current).
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets the page size.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Gets the total count of the list of type <typeparamref name="T"/>
        /// </summary>
        int Total { get; }

        /// <summary>
        /// Gets the total pages.
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// Gets the has previous page.
        /// </summary>
        /// <value>The has previous page.</value>
        bool HasPrevious { get; }

        /// <summary>
        /// Gets the has next page.
        /// </summary>
        /// <value>The has next page.</value>
        bool HasNext { get; }

        /// <summary>
        /// Gets the current page items.
        /// </summary>
        IList<T> Items { get; }
    }
}
