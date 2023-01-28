//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ITreeService.cs
//摘要: ITreeService 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2022-06-29
//----------------------------------------------------------------

using AutoMapper;

using iMaxSys.Max.Extentions;
using iMaxSys.Max.Exceptions;
using iMaxSys.Max.Common.Enums;
using iMaxSys.Max.Collection.Trees;
using iMaxSys.Data;
using iMaxSys.Data.Common;
using iMaxSys.Data.Entities;
using iMaxSys.Data.Repositories;

namespace iMaxSys.Core.Services;

/// <summary>
/// TreeService
/// </summary>
/// <typeparam name="T">实体类型</typeparam>
/// <typeparam name="M">返回类型</typeparam>
/// <typeparam name="R">仓储类型</typeparam>
public abstract class TreeService<T, M> : ITreeService<T, M> where T : Entity, ITreeNode, new() where M : ITreeNode
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IRepository<T> _repository;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="unitOfWork"></param>
    public TreeService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<T>();
    }

    /// <summary>
    /// 获取节点
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="currentId"></param>
    /// <returns></returns>
    /// <exception cref="MaxException"></exception>
    public virtual async Task<T> GetNodeAsync(long tenantId, long currentId)
    {
        T? target = await _repository.FirstOrDefaultAsync(x => x.TenantId == tenantId && x.Id == currentId);

        if (target is not null)
        {
            return target;
        }
        else
        {
            throw new MaxException(ResultCode.TargetIsInvalid);
        }
    }

    /// <summary>
    /// 获取树
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="xppId"></param>
    /// <returns></returns>
    public virtual async Task<ITree<T>?> GetAsync(long tenantId, long xppId)
    {
        var list = await _repository.AllAsync(x => x.TenantId == tenantId && x.XppId == xppId);
        return list.ToTree((parent, child) => child.ParentId == parent.Id);
    }

    /// <summary>
    /// 插入节点
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="targetId"></param>
    /// <param name="model"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public virtual async Task InsertAsync(long tenantId, long targetId, M model, NodePosition position)
    {
        T target = await GetNodeAsync(tenantId, targetId);
        T current = Make(tenantId, target.XppId, model);
        await MoveAsync(tenantId, target, current, position);
    }

    /// <param name="tenantId"></param>
    /// <param name="targetId"></param>
    /// <param name="currentId"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public virtual async Task MoveAsync(long tenantId, long targetId, long currentId, NodePosition position)
    {
        T current = await GetNodeAsync(tenantId, currentId);
        await MoveAsync(tenantId, targetId, current, position);
    }

    /// <summary>
    /// 移动节点
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="targetId"></param>
    /// <param name="current"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    private async Task MoveAsync(long tenantId, long targetId, T current, NodePosition position)
    {
        T target = await GetNodeAsync(tenantId, targetId);
        await MoveAsync(tenantId, target, current, position);
    }

    private async Task MoveAsync(long tenantId, T target, T current, NodePosition position)
    {
        switch (position)
        {
            case NodePosition.Sub:
                await SetChild(tenantId, target, current);
                break;
            case NodePosition.Before:
                int cindex = target.Index;
                await SetIndexes(tenantId, target.Id, target.Index);
                current.Index = cindex;
                break;
            case NodePosition.After:
                await SetIndexes(tenantId, target.Id, target.Index + 1);
                current.Index = target.Index + 1;
                break;
            default:
                break;
        }

        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// 添加部门
    /// </summary>
    /// <param name="tenantId">租户id</param>
    /// <param name="xppId">xppId</param>
    /// <param name="parentId">父节点id</param>
    /// <param name="model">模型</param>
    /// <returns></returns>
    public virtual async Task<M> AddAsync(long tenantId, long xppId, long? parentId, M model)
    {
        T? parent;
        T current;
        var repository = _unitOfWork.GetRepository<T>();

        if (parentId is null)
        {
            //生成根
            parent = new T
            {
                Id = iMaxSys.Max.IdWorker.NextId(),
                XppId = xppId,
                IsRoot = true,
                IsLeaf = false,
            };

            //生成当前节点实体模型
            current = await MakeChild(tenantId, xppId, parent, model);
            current.Index = 0;

            //保存根节点和当前节点,此处由于导航属性的问题,无法使用级联保存,所以使用分别保存
            await repository.AddAsync(parent);
        }
        else
        {
            parent = await repository.FirstOrDefaultAsync(x => x.TenantId == tenantId && x.Id == parentId);

            //父级判断
            if (parent is null)
            {
                throw new MaxException(ResultCode.ParentIsInvalid);
            }
            else
            {
                //调整父级属性
                if (parent.IsLeaf)
                {
                    parent.IsLeaf = false;
                }

                current = await MakeChild(tenantId, xppId, parent, model);
            }
        }

        await repository.AddAsync(current);

        try
        {
            await _unitOfWork.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            var x = ex;
        }

        model.Id = current.Id;
        model.ParentId = parent.Id;
        model.Level = current.Level;
        model.IsLeaf = true;
        return model;
    }



    /// <summary>
    /// 生成节点
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    private T Make(long tenantId, long xppId, M model)
    {
        T current = _mapper.Map<T>(model);
        current.TenantId = tenantId;
        current.XppId = xppId;
        model.TenantId = tenantId;
        if (model.Id > 0)
        {
            current.Id = model.Id;
        }
        return current;
    }

    /// <summary>
    /// 生成子节点
    /// </summary>
    /// <param name="tenantId">租户Id</param>
    /// <param name="parent">父节点</param>
    /// <param name="model">当前节点模型</param>
    /// <returns>数据模型</returns>
    private async Task<T> MakeChild(long tenantId, long xppId, T parent, M model)
    {
        T current = Make(tenantId, xppId, model);
        await SetChild(tenantId, parent, current);
        return current;
    }

    /// <summary>
    /// SetChild
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="parent"></param>
    /// <param name="current"></param>
    /// <returns></returns>
    private async Task SetChild(long tenantId, T parent, T current)
    {
        current.TenantId = tenantId;
        current.ParentId = parent.Id;
        current.IsLeaf = true;
        current.Level = parent.Level + 1;

        var repository = _unitOfWork.GetRepository<T>();

        try
        {
            bool has = await repository.AnyAsync(x => x.TenantId == tenantId && x.ParentId == parent.Id);


            if (has)
            {
                current.Index = await repository.MaxAsync(x => x.TenantId == tenantId && x.ParentId == parent.Id, x => x.Index) + 1;
            }
        }
        catch (Exception ex)
        {
            var xx = ex;
        }


        await SetChildLevel(tenantId, current);
    }

    /// <summary>
    /// 设置子节点的level
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    private async Task SetChildLevel(long tenantId, T parent)
    {
        var children = await _unitOfWork.GetRepository<T>().AllAsync(x => x.TenantId == tenantId && x.ParentId == parent.Id);

        foreach (var current in children)
        {
            await SetChildLevel(tenantId, current);
        }
    }

    private async Task SetIndexes(long tenantId, long parentId, long start)
    {
        var children = await _unitOfWork.GetRepository<T>().AllAsync(x => x.TenantId == tenantId && x.ParentId == parentId && x.Index >= start);

        foreach (var item in children)
        {
            item.Index += 1;
        }
    }

    public virtual async Task RemoveAsync(long tenantId, long currentId)
    {
        //存在子节点
        bool hasChildren = await _repository.AnyAsync(x => x.TenantId == tenantId && x.ParentId == currentId);

        if (hasChildren)
        {
            throw new MaxException(ResultCode.HasChildren);
        }

        T? current = await _repository.FirstOrDefaultAsync(x => x.TenantId == tenantId && x.Id == currentId);

        if (current is null)
        {
            throw new MaxException(ResultCode.TargetIsInvalid);
        }
        _repository.Remove(current);

        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    public virtual async Task UpdateAsync(long tenantId, M model)
    {
        T target = await GetNodeAsync(tenantId, model.Id);
         _mapper.Map(model, target);
        _repository.Update(target);
        await _unitOfWork.SaveChangesAsync();
    }
}