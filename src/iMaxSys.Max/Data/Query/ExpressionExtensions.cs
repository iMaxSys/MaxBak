using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace iMaxSys.Max.Data.Query
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                                    Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }

		public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
		{
			if (!condition)
			{
				return source;
			}
			return source.Where(predicate);
		}

		public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, int, bool>> predicate)
		{
			if (!condition)
			{
				return source;
			}
			return source.Where(predicate);
		}

		public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
		{
			if (!condition)
			{
				return source;
			}
			return source.Where(predicate);
		}

		public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
		{
			if (!condition)
			{
				return source;
			}
			return source.Where(predicate);
		}

		public static Expression<Func<T, bool>> WhereIf<T>(this Expression<Func<T, bool>> source, bool condition, Expression<Func<T, bool>> predicate)
		{
			if (!condition)
			{
				return source;
			}
			return source.And(predicate);
		}
	}
}
