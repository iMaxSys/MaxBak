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

namespace iMaxSys.Max.Collection;

/// <summary>
/// PagedList
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedList<T> : IPagedList<T>
{
    /// <summary>
    /// Gets the page index (current).
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// Gets the page size.
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// Gets the total count of the list of type <typeparamref name="T"/>
    /// </summary>
    public int Total { get; }

    /// <summary>
    /// Gets the total pages.
    /// </summary>
    public int TotalPages { get; }

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
    public IList<T> Items { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}" /> class.
    /// </summary>
    public PagedList() => Items = new List<T>();

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}" /> class.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="index">The index of the page.</param>
    /// <param name="size">The size of the page.</param>
    public PagedList(IEnumerable<T> source, int index, int size)
    {
        if (source is IQueryable<T> querable)
        {
            Index = index;
            Size = size;
            Total = querable.Count();
            TotalPages = Total / Size;
            Items = querable.Skip((Index * Size)).Take(Size).ToList();
        }
        else
        {
            Index = index;
            Size = size;
            Total = source.Count();
            TotalPages = Total / Size;
            Items = source.Skip((Index * Size)).Take(Size).ToList();
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}" /> class.
    /// </summary>
    /// <param name="current"></param>
    /// <param name="index"></param>
    /// <param name="size"></param>
    /// <param name="total"></param>
    /// <param name="totalPages"></param>
    public PagedList(IEnumerable<T> current, int index, int size, int total)
    {
        Index = index;
        Size = size;
        Total = total;
        TotalPages = Total / Size;
        Items = current.ToList();
    }
}