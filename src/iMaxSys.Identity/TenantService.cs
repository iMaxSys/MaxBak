//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: TenantService.cs
//摘要: 租户服务
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------
/*
using System.Threading.Tasks;

using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using iMaxSys.Max.Data;
using iMaxSys.Max.Options;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Data.EFCore;

using DbTenant = iMaxSys.Core.Data.Models.Tenant;

namespace iMaxSys.Identity
{
    /// <summary>
    /// 租户服务
    /// </summary>
    public class TenantService : Service, ITenantService
    {
        public TenantService(IMapper mapper, IOptions<MaxOption> option, IIdentityCache cache, IUnitOfWork<MaxIdentityContext> unitOfWork) : base(mapper, option, cache, unitOfWork)
        {
        }

        /// <summary>
        /// 获取租户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ITenant> GetAsync(long id)
        {
            string key = GetTenantKey(id);
            bool exists = await KeyExistsAsync(key);

            ITenant tenant;
            if (exists)
            {
                tenant = await GetCacheAsync<ITenant>(key);
            }
            else
            {
                //DbTenant dbTenant = await _unitOfWork.GetRepository<DbTenant>().GetFirstOrDefaultAsync(predicate: x => x.Id == id, disableTracking: true);
                DbTenant dbTenant = await _unitOfWork.GetRepo<DbTenant>().GetAsync(id);
                tenant = _mapper.Map<ITenant>(dbTenant);
                await SetCacheAsync(key, tenant);
            }
            return tenant;
        }

        /// <summary>
        /// 新增租户
        /// </summary>
        /// <param name="tenantModel"></param>
        /// <returns></returns>
        public async Task<ITenant> AddAsync(TenantModel tenantModel)
        {
            DbTenant dbTenant = new DbTenant();
            SetDbTenant(tenantModel, dbTenant);
            //await _unitOfWork.GetRepository<DbTenant>().InsertAsync(dbTenant);
            await _unitOfWork.GetRepo<DbTenant>().AddAsync(dbTenant);
            await _unitOfWork.SaveChangesAsync();
            ITenant tenant = _mapper.Map<ITenant>(dbTenant);
            await SetCacheAsync(GetTenantKey(tenant.Id), tenant);
            return tenant;
        }

        /// <summary>
        /// 更新租户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tenantModel"></param>
        /// <returns></returns>
        public async Task<ITenant> UpdateAsync(TenantModel tenantModel)
        {
            DbTenant dbTenant = await _unitOfWork.GetRepo<DbTenant>().GetAsync(tenantModel.Id);
            SetDbTenant(tenantModel, dbTenant);
            await _unitOfWork.GetRepo<DbTenant>().UpdateAsync(dbTenant);
            await _unitOfWork.SaveChangesAsync();
            ITenant tenant = _mapper.Map<ITenant>(dbTenant);
            await SetCacheAsync(GetTenantKey(tenant.Id), tenant);
            return tenant;
        }

        /// <summary>
        /// 移除租户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tenantModel"></param>
        /// <returns></returns>
        public async Task RemoveAsync(long id)
        {
            var tenant = await _unitOfWork.GetRepo<DbTenant>().GetAsync(id);
            await _unitOfWork.GetRepo<DbTenant>().RemoveAsync(tenant);
            await _unitOfWork.SaveChangesAsync();
            await RemoveCacheAsync(GetTenantKey(id));
        }

        /// <summary>
        /// 设置DbTenant
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dbTenant"></param>
        private void SetDbTenant(TenantModel model, DbTenant dbTenant)
        {
            dbTenant.Name = model.Name ?? dbTenant.Name;
            dbTenant.Alias = model.Alias ?? dbTenant.Alias;
            dbTenant.Description = model.Description ?? dbTenant.Description;
            dbTenant.Start = model.Start ?? dbTenant.Start;
            dbTenant.End = model.End ?? dbTenant.End;
            dbTenant.Status = model.Status ?? dbTenant.Status;
        }
    }
}
*/