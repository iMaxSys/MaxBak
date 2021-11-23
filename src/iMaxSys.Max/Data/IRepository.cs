//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IRepository.cs
//摘要: IRepository 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Collection;
using iMaxSys.Max.Data.Entities;
using iMaxSys.Max.Data.Specifications;
using Microsoft.AspNetCore.Routing.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace iMaxSys.Max.Data
{
    public interface IRepository<T> where T : Entity
    {
        /// <summary>
        /// Code
        /// </summary>
        int Code { get; }

        /// <summary>
        /// AutoCommit
        /// </summary>
        bool AutoCommit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ExistAsync(Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddAsync(params T[] entities);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddAsync(IEnumerable<T> entities);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(long Id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量软删除
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(IQueryable<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync(IQueryable<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateAsync(IQueryable<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量更新使用UpdateRange
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateAsync(params T[] entities);

        /// <summary>
        /// 批量更新使用UpdateRange
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 局部更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity, params Expression<Func<T, object>>[] properties);

        /// <summary>
        /// 局部更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity, params string[] properties);

        /// <summary>
        /// 计算数量
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

        /// <param name="id"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        Task<T?> GetAsync(long id, bool noTracking = true);

        /// <summary>
        /// FirstOrDefaultAsync
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(ISpecification<T> spec);

        /// <summary>
        /// LastOrDefaultAsync
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<T?> LastOrDefaultAsync(ISpecification<T> spec);

        /// <summary>
        /// GetAllAsync
        /// </summary>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// GetAllAsync
        /// </summary>
        Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> selector);
        /// <summary>
        /// GetAllAsync
        /// </summary>
        Task<List<TResult>> GetAllAsync<TResult>(ISpecification<T, TResult> spec);

        /// <summary>
        /// GetListAsync
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(ISpecification<T> spec);

        /// <summary>
        /// GetListAsync
        /// </summary>
        Task<List<TResult>> GetListAsync<TResult>(ISpecification<T, TResult> spec);

        /// <summary>
        /// GetPagedListAsync
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<IPagedList<T>> GetPagedListAsync(ISpecification<T> spec);

        /// <summary>
        /// GetPagedListAsync
        /// </summary>
        Task<IPagedList<TResult>> GetPagedListAsync<TResult>(ISpecification<T, TResult> spec);
    }
}
