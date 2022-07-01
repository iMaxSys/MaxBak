//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: DepartmentRepository.cs
//摘要: 部门仓储
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-16
//----------------------------------------------------------------

using iMaxSys.Data.EFCore.Repositories;
using iMaxSys.Identity.Data.EFCore;
using iMaxSys.Identity.Data.Entities;

namespace iMaxSys.Identity.Data.Repositories;

public class DepartmentRepository : EfRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(IdentityContext context) : base(context)
    {
    }

    /// <summary>
    /// 更新索引
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="parentId"></param>
    /// <param name="start"></param>
    /// <param name="step"></param>
    /// <returns></returns>
    //public int UpdateIndex(long tenantId, long parentId, int start, int step)
    //{
    //    return ExecuteSql($"UPDATE department SET index=index+{0} WHERE tenant_id={1} AND parent_id={2} AND index>={3};", step, tenantId, parentId, start);
    //}
}