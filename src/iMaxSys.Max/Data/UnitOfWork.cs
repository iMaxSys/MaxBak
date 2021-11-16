//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: UnitOfWork.cs
//摘要: UnitOfWork 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using iMaxSys.Max.Data.EFCore;
using iMaxSys.Max.Data.Entities;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace iMaxSys.Max.Data
{
    /// <summary>
    /// UnitOfWork
    /// </summary>
    /// <typeparam name="T">DbContext</typeparam>
    public class UnitOfWork<T> : IUnitOfWork<T> where T : DbContext
    {
        private readonly T _context;
        private readonly IServiceProvider _serviceProvider;
        //private Dictionary<Type, object> _repositories;

        /// <summary>
        /// Gets the db context.
        /// </summary>
        /// <returns>The instance of type <typeparamref name="TContext"/>.</returns>
        public T DbContext => _context;

        public UnitOfWork(T context, IServiceProvider serviceProvider)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _serviceProvider = serviceProvider;
        }

        //public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        //{
        //    await _context.AddAsync<TEntity>(entity);
        //}

        public DbSet<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public IRepository<TEntity> GetRepo<TEntity>() where TEntity : Entity
        {
            try
            {
                //var repo = _serviceProvider.GetServices<IRepository<TEntity>>();
                var repo = _serviceProvider.GetServices(typeof(IRepository<TEntity>));
                return _serviceProvider.GetServices<IRepository<TEntity>>().FirstOrDefault(x => x.Code == _context.GetType().GetHashCode());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        //public async Task<int> SaveChangesAsync()
        //{
        //	return await _context.SaveChangesAsync();
        //}

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(IUnitOfWork[] unitOfWorks, CancellationToken cancellationToken = default)
        {
            using var ts = new TransactionScope();
            var count = 0;
            foreach (var unitOfWork in unitOfWorks)
            {
                count += await unitOfWork.SaveChangesAsync();
            }

            count += await SaveChangesAsync();

            ts.Complete();

            return count;
        }
    }
}
