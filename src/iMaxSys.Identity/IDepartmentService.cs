//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: IDepartmentService.cs
//摘要: 部门服务接口
//说明: 
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-15
//----------------------------------------------------------------

using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.DependencyInjection;
using iMaxSys.Identity.Models;

namespace iMaxSys.Identity;

/// <summary>
/// 部门服务接口
/// </summary>
public interface IDepartmentService : IDependency
{
    /// <summary>
    /// 获取部门
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<DepartmentModel> GetAsync(long tenantId, long id);

    /// <summary>
    /// 添加部门
    /// </summary>
    /// <param name="tenantId">租户id</param>
    /// <param name="parentId">父节点id</param>
    /// <param name="model">部门</param>
    /// <returns></returns>
    Task<DepartmentModel> AddAsync(long tenantId, long? parentId, DepartmentModel model);

    /// <summary>
    /// 移除部门
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveAsync(long tenantId, long id);

    /// <summary>
    /// 移动部门
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="currentId"></param>
    /// <param name="targetId"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    Task<DepartmentModel> MoveAsync(long tenantId, long currentId, long targetId, NodePosition position);
}
