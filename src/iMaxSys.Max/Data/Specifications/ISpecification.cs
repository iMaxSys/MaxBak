//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ISpecification.cs
//摘要: 查询规范 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Collections.Generic;

using iMaxSys.Max.Data.Query;
using System.Linq;

namespace iMaxSys.Max.Data.Specifications
{
    /// <summary>
    /// 查询规范接口
    /// </summary>
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        Specification<T> ApplyTracking();
        Specification<T> ApplyPaging(int index, int size);
        Specification<T> AddInclude(Expression<Func<T, object>> includeExpression);
        Specification<T> AddIncludes<TProperty>(Func<IncludeAggregator<T>, IIncludeQuery<T, TProperty>> includeGenerator);
        Specification<T> ApplyOrderBy(Expression<Func<T, object>> orderByExpression);
        Specification<T> ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression);
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, object>> GroupBy { get; }
        //Expression<Func<IGrouping<object, T>, int, T>> SelectorGroupby { get; }

        bool NoTracking { get; }
        int Index { get; }
        int Size { get; }
        bool IsPagingEnabled { get; }
    }
    public interface ISpecification<T, TResult> : ISpecification<T>
    {
        Expression<Func<T, TResult>> Selector { get; }
        ISpecification<T, TResult> ApplySelector(Expression<Func<T, TResult>> selector);
    }
}