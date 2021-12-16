//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IRoleService.cs
//摘要: 角色服务接口
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------
/*
using System.Threading.Tasks;

using iMaxSys.Max.Identity.Domain;
using iMaxSys.Max.DependencyInjection;

using iMaxSys.Identity.Models;

namespace iMaxSys.Identity
{
    /// <summary>
    /// 角色服务接口
    /// </summary>
    public interface IRoleService : IDependency
    {
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IRole> GetRoleAsync(long id, long tenantId = 0);

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IRole> AddRoleAsync(RoleModel model, long tenantId = 0);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="accessChain"></param>
        Task<IRole> UpdateRoleAsync(RoleModel model, long tenantId = 0);

        /// <summary>
        /// 移除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveRoleAsync(long id, long tenantId = 0);

        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IMenu> GetRoleMenuAsync(long id, long tenantId = 0);

        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<IMenu> GetRoleMenuAsync(IRole role, long tenantId = 0);

        /// <summary>
        /// 获取完整菜单
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <returns></returns>
        Task<IMenu> GetFullMenuAsync(long tenantId = 0);
    }
}
*/