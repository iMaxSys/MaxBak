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
using iMaxSys.Identity.Models;
using iMaxSys.Identity.Data.Entities;
using iMaxSys.Identity.Common;
using iMaxSys.Identity.Data.Repositories;

namespace iMaxSys.Identity;

public class DepartmentService : IDepartmentService
{
    /// <summary>
    /// _unitOfWork
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="unitOfWork"></param>
    public DepartmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// 添加部门
    /// </summary>
    /// <param name="tenantId">租户id</param>
    /// <param name="parentId">父节点id</param>
    /// <param name="model">部门</param>
    /// <returns></returns>
    public async Task<DepartmentModel> AddAsync(long tenantId, long? parentId, DepartmentModel model)
    {
        Department? parent;
        Department current;
        var repository = _unitOfWork.GetRepository<Department>();

        if (parentId is null)
        {
            //生成根
            parent = new();
            parent.IsLeaf = false;

            //生成当前节点实体
            current = MakeDepartment(tenantId, parent, model);
            current.Index = 0;
            parent.Departments = new List<Department>
            {
                current
            };
            await repository.AddAsync(parent);
        }
        else
        {
            parent = await repository.FindAsync(parentId);

            //父级判断
            if (parent is null)
            {
                throw new MaxException(ResultCode.ParentDepartmentIsInvalid);
            }
            else
            {
                //调整父级属性
                if (parent.IsLeaf)
                {
                    parent.IsLeaf = false;
                }
                current = MakeDepartment(tenantId, parent, model);
                current.Index = await repository.CountAsync(x => x.ParentId == parent.Id);
                await repository.AddAsync(current);
            }
        }

        await _unitOfWork.SaveChangesAsync();

        //更改后续节点index

        model.Id = current.Id;
        model.ParentId = parent.Id;
        model.Level = current.Level;
        model.IsLeaf = true;
        return model;
    }

    /// <summary>
    /// 获取部门
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    private async Task<Department> GetDepartmentAsync(long tenantId, long id)
    {
        Department? current = await _unitOfWork.GetRepository<Department>().FirstOrDefaultAsync(x=>x.Id == id &&  x.TenantId == tenantId);

        if (current is null)
        {
            throw new MaxException(ResultCode.DepartmentIsInvalid);
        }
        else
        {
            return current;
        }
    }

    /// <summary>
    /// 获取部门
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public async Task<DepartmentModel> GetAsync(long tenantId, long id)
    {
        var current = await GetDepartmentAsync(tenantId, id);
        return MakeModel(current);
    }

    public async Task<DepartmentModel> MoveAsync(long tenantId, long currentId, long targetId, NodePosition position)
    {
        var repository = _unitOfWork.GetCustomRepository<IDepartmentRepository>();
        var current = await GetDepartmentAsync(tenantId, currentId);
        var target = await GetDepartmentAsync(tenantId, targetId);

        //更新标志
        bool flag = false;

        switch (position)
        {
            case NodePosition.Before:
                current.ParentId = target.ParentId;
                current.Index = target.Index;
                current.Level = target.Level;
                flag = true;
                break;
            case NodePosition.After:
                current.ParentId = target.ParentId;
                current.Index = target.Index + 1;
                current.Level = target.Level;
                flag = true;
                break;
            case NodePosition.Sub:
                current.ParentId = target.Id;
                current.Index = await repository.CountAsync(x => x.ParentId == target.Id);
                current.Level = target.Level + 1;
                break;
            default:
                break;
        }

        if (flag)
        {
            //repository.UpdateIndex(tenantId, current.ParentId, );
        }
    }

    /// <summary>
    /// 移除部门
    /// </summary>
    /// <param name="id">部门Id</param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public async Task RemoveAsync(long id)
    {
        var repostory = _unitOfWork.GetRepository<Department>();

        //子部门判断
        if (repostory.Any(x => x.ParentId == id))
        {
            throw new MaxException(ResultCode.HasChildren);
        }

        //成员判断
        if (_unitOfWork.GetRepository<Member>().Any(x => x.DepartmentId == id))
        {
            throw new MaxException(ResultCode.HasMember);
        }

        _unitOfWork.GetRepository<Department>().Remove(id);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// 生成Department
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="parentId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    private static Department MakeDepartment(long tenantId, Department parent, DepartmentModel model)
    {
        Department department = new();
        if (model.Id > 0)
        {
            department.Id = model.Id;
        }
        department.Name = model.Name;
        department.ParentId = parent.Id;
        department.Type = model.Type;
        department.IsLeaf = true;
        department.Level = parent.Level + 1;
        department.Code = model.Code;
        department.QuickCode = model.QuickCode;
        department.Value = model.Value;
        department.Alias = model.Alias;
        department.Description = model.Description;
        department.Style = model.Style;
        department.SelectedStyle = model.SelectedStyle;
        department.Icon = model.Icon;
        department.SelectedIcon = model.SelectedIcon;
        department.Data = model.Data;
        //department.Action = model.Action;
        department.Ext = model.Ext;
        department.TenantId = tenantId;
        department.Status = model.Status;
        return department;
    }

    /// <summary>
    /// 生成Department
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="parentId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    private static DepartmentModel MakeModel(Department department)
    {
        return new DepartmentModel()
        {
            Id = department.Id,
            Name = department.Name,
            ParentId = department.ParentId ?? 0,
            Type = department.Type,
            Index = department.Index,
            IsLeaf = department.IsLeaf,
            Level = department.Level,
            Code = department.Code,
            QuickCode = department.QuickCode,
            Value = department.Value,
            Alias = department.Alias,
            Description = department.Description,
            Style = department.Style,
            SelectedStyle = department.SelectedStyle,
            Icon = department.Icon,
            SelectedIcon = department.SelectedIcon,
            Data = department.Data,
            Action = department.Action,
            Ext = department.Ext,
            TenantId = department.TenantId,
            Status = department.Status
        };
    }
}