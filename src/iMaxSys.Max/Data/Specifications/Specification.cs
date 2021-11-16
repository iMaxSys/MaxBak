//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Specification.cs
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
    /// Specification
    /// </summary>
    /// <typeparam name = "T" ></ typeparam >
    public class Specification<T> : ISpecification<T>
    {
        public Specification()
        {
        }

        public Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public Expression<Func<T, object>> GroupBy { get; private set; }
        //public Expression<Func<IGrouping<object, T>, T>> SelectorGroupby { get; private set; }

        public bool NoTracking { get; private set; } = true;
        public int Index { get; private set; }
        public int Size { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;


        public virtual Specification<T> ApplyTracking()
        {
            NoTracking = false;
            return this;
        }

        public virtual Specification<T> ApplyPaging(int index, int size)
        {
            Index = index;
            Size = size;
            IsPagingEnabled = true;
            return this;
        }

        public virtual Specification<T> AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
            return this;
        }

        public virtual Specification<T> AddIncludes<TProperty>(Func<IncludeAggregator<T>, IIncludeQuery<T, TProperty>> includeGenerator)
        {
            var includeQuery = includeGenerator(new IncludeAggregator<T>());
            IncludeStrings.AddRange(includeQuery.Paths);
            return this;
        }

        public virtual Specification<T> AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
            return this;
        }



        public virtual Specification<T> ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
            return this;
        }

        public virtual Specification<T> ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
            return this;
        }

        //Not used anywhere at the moment, but someone requested an example of setting this up.
        public virtual Specification<T> ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
            return this;
        }
        //public virtual Specification<T> ApplyGroupBy<Tresult, Tkey>(Expression<Func<Tresult, Tkey>> groupByExpression, Expression<Func<IGrouping<object, T>, int, T>> selectorGroupby)
        //{
        //    GroupBy = groupByExpression;
        //    SelectorGroupby = selectorGroupby;
        //    return this;
        //}

    }

    public class Specification<T, TResult> : Specification<T>, ISpecification<T, TResult>
    {
        public Specification()
        {
        }

        public Specification(Expression<Func<T, bool>> criteria) : base(criteria)
        {
        }
        public Expression<Func<T, TResult>> Selector { get; private set; }

        public virtual ISpecification<T, TResult> ApplySelector(Expression<Func<T, TResult>> selector)
        {
            Selector = selector;
            return this;
        }
    }
}
