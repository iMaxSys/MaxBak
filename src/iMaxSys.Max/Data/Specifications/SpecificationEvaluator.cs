
using iMaxSys.Max.Collection;
using iMaxSys.Max.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace iMaxSys.Max.Data.Specifications
{
    public class SpecificationEvaluator<T> where T : Entity
    {
        /// <summary>
        /// 全部查询
        /// </summary>
        /// <param name="inputQuery"></param>
        /// <param name="specification"></param>
        /// <returns></returns>
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = Query(inputQuery, specification);

            // Apply paging if enabled
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Index * specification.Size).Take(specification.Size);
            }
            return query;
        }

        /// <summary>
        /// 全部查询分页
        /// </summary>
        /// <param name="inputQuery"></param>
        /// <param name="specification"></param>
        /// <returns></returns>
        public async static Task<IPagedList<T>> GetPagedList(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = Query(inputQuery, specification);

            return await query.ToPagedListAsync(specification.Index, specification.Size);
        }

        
        /// <summary>
        /// 部分筛选查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="inputQuery"></param>
        /// <param name="specification"></param>
        /// <returns></returns>
        public static IQueryable<TResult> GetQuery<TResult>(IQueryable<T> inputQuery, ISpecification<T, TResult> specification)
        {
            if (specification.Selector == null)
            {
                throw new ArgumentNullException(nameof(specification.Selector));
            }

            var query = Query(inputQuery, specification);

            // Apply paging if enabled
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Index * specification.Size).Take(specification.Size);
            }
            var result = query.Select(specification.Selector);

            return result;
        }

        /// <summary>
        /// 部分筛选查询分页
        /// </summary>
        /// <param name="inputQuery"></param>
        /// <param name="specification"></param>
        /// <returns></returns>
        public async static Task<IPagedList<TResult>> GetPagedList<TResult>(IQueryable<T> inputQuery, ISpecification<T, TResult> specification)
        {
            var query = Query(inputQuery, specification);
            var result = query.Select(specification.Selector);
            //if (specification.GroupBy != null)
            //{
            //    var sadf = query.GroupBy(specification.GroupBy).Select(specification.SelectorGroupby);
            //    query = query.GroupBy(specification.GroupBy).Select(specification.SelectorGroupby);

            //}
            return await result.ToPagedListAsync(specification.Index, specification.Size);
        }


        private static IQueryable<T> Query(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            if (specification.NoTracking)
            {
                query = query.AsNoTracking();
            }

            // modify the IQueryable using the specification's criteria expression
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Includes all expression-based includes
            query = specification.Includes.Aggregate(query,
                                    (current, include) => current.Include(include));

            // Include any string-based include statements
            query = specification.IncludeStrings.Aggregate(query,
                                    (current, include) => current.Include(include));

            // Apply ordering if expressions are set
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            return query;
        }
    }
}
