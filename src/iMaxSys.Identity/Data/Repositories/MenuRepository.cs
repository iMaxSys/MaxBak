//----------------------------------------------------------------
//Copyright (C) 2016-2026 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: MenuRepository.cs
//摘要: 菜单仓储
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Identity.Domain;
using iMaxSys.Data.Repositories.EFCore;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Entities;
using DbMenu = iMaxSys.Identity.Data.Entities.Menu;

namespace iMaxSys.Identity.Data.Repositories
{
	/// <summary>
    /// 菜单仓储
    /// </summary>
	public class MenuRepository : EfRepository<DbMenu>, IMenuRepository
	{
        protected const string TAG = "id:";
        protected const string TAG_MENU = "m:";
        protected const string TAG_TENANT = "t:";

        protected const string TAG_TENANT_MENU = $"{TAG}{TAG_MENU}";

        private readonly IIdentityCache _identityCache;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identityCache"></param>
        public MenuRepository(MaxIdentityContext context, IIdentityCache identityCache) : base(context)
        {
            _identityCache = identityCache;
        }

        public async Task<IMenu?> ReadAsync(long xpp, long roleId, long tenantId)
        {
            string key = $"{TAG}{TAG_MENU}{xpp}{tenantId}";

            var menu = await _identityCache.GetAsync<Menu>(key, true);

            if (menu != null)
            {
                return menu;
            }

            var ms = await GetCacheAsync<MenuShadow>(key);
        }

        public Task<IMenu?> GetAsync(long xpp, long roleId)
        {
            throw new NotImplementedException();
        }

        

        public Task RefreshAsync(IRole role, DateTime expires)
        {
            throw new NotImplementedException();
        }
    }
}

