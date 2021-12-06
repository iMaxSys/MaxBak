//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: EfRepository.cs
//摘要: EfRepository 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using iMaxSys.Max.Collection;
using iMaxSys.Max.Data.Entities;

namespace iMaxSys.Max.Data.EFCore
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public EfRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public int Code => _dbContext.GetType().GetHashCode();

        /// <summary>
        /// 自动提交
        /// </summary>
        public virtual bool AutoCommit { get; set; } = false;

        /// <summary>
        /// ExistAsync
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> ExistAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().AnyAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync();
            }
            return entity;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(params T[] entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            entity.IsDeleted = true;
            _dbContext.Entry(entity).State = EntityState.Modified;
            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(long Id, CancellationToken cancellationToken = default)
        {
            T entity = System.Activator.CreateInstance<T>();
            entity.Id = Id;
            entity.IsDeleted = true;
            this._dbContext.Entry<T>(entity).State = EntityState.Unchanged;
            _dbContext.Entry<T>(entity).Property<object>(m => m.IsDeleted).IsModified = true;
            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        /// <summary>
        /// 批量软删除
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            var entities = _dbContext.Set<T>().Where(expression);

            foreach (var item in entities)
            {
                item.IsDeleted = true;
                _dbContext.Entry(item).State = EntityState.Modified;
            }

            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(IQueryable<T> entities, CancellationToken cancellationToken = default)
        {
            foreach (var item in entities)
            {
                item.IsDeleted = true;
                _dbContext.Entry(item).State = EntityState.Modified;
            }
            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Remove(entity);
            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public virtual async Task RemoveAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().RemoveRange(_dbContext.Set<T>().Where(expression));
            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public virtual async Task RemoveAsync(IQueryable<T> entities, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            //var x = _dbContext.Entry(entity).State;
            _dbContext.Set<T>().Update(entity);
            //_dbContext.Update(entity);
            //if (_dbContext.Entry(entity).State == EntityState.Detached)
            //{
            //    _dbContext.Set<T>().Attach(entity);
            //    var xx = _dbContext.Entry(entity).State;
            //    _dbContext.Entry(entity).State = EntityState.Modified;
            //}

            //_dbContext.Attach(entity);
            //_dbContext.Entry(entity).State = EntityState.Modified;

            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(IQueryable<T> entities, CancellationToken cancellationToken = default)
        {
            foreach (var item in entities)
            {
                _dbContext.Entry(item).State = EntityState.Modified;
            }
            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        /// <summary>
        /// 批量更新使用UpdateRange
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(params T[] entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 批量更新使用UpdateRange
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        /// <summary>
        /// 局部更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(T entity, params Expression<Func<T, object>>[] properties)
        {
            this._dbContext.Entry<T>(entity).State = EntityState.Unchanged;
            foreach (Expression<Func<T, object>> expression in properties)
            {
                _dbContext.Entry<T>(entity).Property<object>(expression).IsModified = true;
            }
            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 局部更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(T entity, params string[] properties)
        {
            this._dbContext.Entry<T>(entity).State = EntityState.Unchanged;
            foreach (string text in properties)
            {
                _dbContext.Entry<T>(entity).Property(text).IsModified = true;
            }
            if (AutoCommit)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 计算数量
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().CountAsync(expression, cancellationToken);
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual async Task<T?> GetAsync(long id, bool noTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<T?> FindAsync(params object[] keyValues)
        {
            return await _dbContext.Set<T>().AllAsync(keyValues).ConfigureAwait(false);
        }

        public async Task<T?> FindAsync(object[] keyValues, bool noTracking = true, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().FindAsync(keyValues, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// FirstOrDefaultAsync
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        /// <summary>
        /// LastOrDefaultAsync
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public virtual async Task<T?> LastOrDefaultAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).LastOrDefaultAsync();
        }

        public virtual async Task<T?> GetSingleOrDefaultAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).SingleOrDefaultAsync();
        }

        public virtual IQueryable<T?> GetFromSql(string sql, params object[] parameters)
        {
            return _dbContext.Set<T>().FromSqlRaw<T>(sql, parameters);
        }
        /// <summary>
        /// GetAllAsync
        /// </summary>
        public virtual async Task<List<T>> GetAllAsync()
        {
            IQueryable<T> query = _dbContext.Set<T>().AsNoTracking();
            return await query.ToListAsync();
        }
        /// <summary>
        /// GetAllAsync
        /// </summary>
        public virtual async Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            IQueryable<TResult> query = _dbContext.Set<T>().AsNoTracking().Select(selector);
            return await query.ToListAsync();
        }
        /// <summary>
        /// GetAllAsync
        /// </summary>
        public virtual async Task<List<TResult>> GetAllAsync<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        /// <summary>
        /// GetListAsync
        /// </summary>
        public virtual async Task<List<T>> GetListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        /// <summary>
        /// GetListAsync
        /// </summary>
        public virtual async Task<List<TResult>> GetListAsync<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        /// <summary>
        /// GetPagedListAsync
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual async Task<IPagedList<T>> GetPagedListAsync(ISpecification<T> spec)
        {
            return await ApplyPagedSpecification(spec);
        }

        /// <summary>
        /// GetPagedListAsync
        /// </summary>
        public virtual async Task<IPagedList<TResult>> GetPagedListAsync<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplyPagedSpecification(spec);
        }

        /// <summary>
        /// ApplySpecification
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            return SpecificationEvaluator<T>.GetQuery(query, spec);
        }
        /// <summary>
        /// ApplySpecification
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            return SpecificationEvaluator<T>.GetQuery(query, spec);
        }
        /// <summary>
        /// ApplyPagedSpecification
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        private async Task<IPagedList<T>> ApplyPagedSpecification(ISpecification<T> spec)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            return await SpecificationEvaluator<T>.GetPagedList(query, spec);
        }

        /// <summary>
        /// ApplyPagedSpecification
        /// </summary>
        private async Task<IPagedList<TResult>> ApplyPagedSpecification<TResult>(ISpecification<T, TResult> spec)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            return await SpecificationEvaluator<T>.GetPagedList(query, spec);
        }
    }
}
