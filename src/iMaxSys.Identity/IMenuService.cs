//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IMenuService.cs
//摘要: 菜单服务接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

using System.Threading.Tasks;

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.DependencyInjection;

using iMaxSys.Identity.Models;

namespace iMaxSys.Identity
{
    /// <summary>
    /// 菜单服务接口
    /// </summary>
    public interface IMenuService : IDependency
    {
        /// <summary>
        /// 获取完整菜单
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IMenu> GetFullMenuAsync(long tenantId = 0);

        /// <summary>
        /// 获取菜单项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IMenu> GetMenuItemAsync(long id, long tenantId = 0);

        /// <summary>
        /// 新增菜单项
        /// </summary>
        /// <param name="menuModel">菜单</param>
        /// <param name="targetId">参考Id</param>
        /// <param name="isSub">是否为参考Id子菜单,是为子级,否为同级</param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IMenu> AddMenuItemAsync(MenuModel menuModel, long targetId, bool isSub, long tenantId = 0);

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menuModel"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IMenu> UpdateMenuItemAsync(MenuModel menuModel, long tenantId = 0);

        /// <summary>
        /// 移动菜单项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="targetId">参考Id</param>
        /// <param name="isSub">是否为参考Id子菜单,是为子级,否为同级</param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task MoveMenuItemAsync(long id, long targetId, bool isSub, long tenantId = 0);

        /// <summary>
        /// 移除菜单项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task RemoveMenuItemAsync(long id, long tenantId = 0);
    }
}
