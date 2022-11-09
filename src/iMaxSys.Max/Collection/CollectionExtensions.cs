//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: CollectionExtensions.cs
//摘要: 集合扩展
//说明:   
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

namespace iMaxSys.Max.Collection;

/// <summary>
/// 集合扩展
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    ///     Converts all.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="items">The items.</param>
    /// <param name="transformation">The transformation.</param>
    /// <returns></returns>
    public static TResult[] ConvertAll<T, TResult>(this T[] items, Converter<T, TResult> transformation)
    {
        return Array.ConvertAll(items, transformation);
    }

    /// <summary>
    ///     Finds the specified predicate.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <param name="predicate">The predicate.</param>
    /// <returns></returns>
    public static T? Find<T>(this T[] items, Predicate<T> predicate)
    {
        return Array.Find(items, predicate);
    }

    /// <summary>
    ///     Finds all.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <param name="predicate">The predicate.</param>
    /// <returns></returns>
    public static T[] FindAll<T>(this T[] items, Predicate<T> predicate)
    {
        return Array.FindAll(items, predicate);
    }

    /// <summary>
    ///     Fors the each.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <param name="action">The action.</param>
    public static void ForEach<T>(this IEnumerable<T>? items, Action<T> action)
    {
        if (items is null)
        {
            return;
        }

        foreach (T item in items)
        {
            action(item);
        }
    }

    /// <summary>
    /// Checks whether or not collection is null or empty. Assumes colleciton can be safely enumerated multiple times.
    /// </summary>
    /// <param name="this">The this.</param>
    /// <returns>
    ///     <c>true</c> if [is null or empty] [the specified this]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNullOrEmpty(this IEnumerable? @this)
    {
        if (@this is not null)
        {
            return !@this.GetEnumerator().MoveNext();
        }
        return true;
    }
}