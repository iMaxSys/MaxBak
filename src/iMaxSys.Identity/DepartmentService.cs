// ----------------------------------------------------------------
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

using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Data;
using iMaxSys.Data.Services;
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Data.Entities;
using iMaxSys.Identity.Common;
using iMaxSys.Identity.Data.Repositories;
using iMaxSys.Max.Collection.Trees;

namespace iMaxSys.Identity;

public class DepartmentService : TreeService<Department, DepartmentModel>, IDepartmentService
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="unitOfWork"></param>
    public DepartmentService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }
}