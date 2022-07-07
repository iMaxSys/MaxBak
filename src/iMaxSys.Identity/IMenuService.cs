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


using iMaxSys.Max.DependencyInjection;
using iMaxSys.Data.Services;
using iMaxSys.Identity.Data.Entities;
using iMaxSys.Identity.Models;
using iMaxSys.Max.Collection.Trees;

namespace iMaxSys.Identity;

/// <summary>
/// 菜单服务接口
/// </summary>
public interface IMenuService : ITreeService<Menu, MenuModel>, IDependency
{
    /// <summary>
    /// 获取获取角色菜单
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<ITree<Menu>?> GetAsync(long tenantId, long xppId, long roleId);
}