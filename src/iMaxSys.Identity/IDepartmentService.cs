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
using iMaxSys.Data.Services;
using iMaxSys.Identity.Data.Entities;

namespace iMaxSys.Identity;

/// <summary>
/// 部门服务接口
/// </summary>
public interface IDepartmentService : ITreeService<Department, DepartmentModel>, IDependency
{
}
