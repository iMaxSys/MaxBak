using AutoMapper;

using iMaxSys.Data;
using iMaxSys.Data.Entities;
using iMaxSys.Identity.Common;
using iMaxSys.Max.Collection.Trees;
using iMaxSys.Max.Exceptions;

namespace iMaxSys.Identity;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T">实体类型</typeparam>
/// <typeparam name="M">返回类型</typeparam>
/// <typeparam name="R">仓储类型</typeparam>
public abstract class TreeService<T, M, R> where T : Entity, ITreeNode, new() where M : TreeView
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="unitOfWork">uow</param>
    public TreeService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// 添加部门
    /// </summary>
    /// <param name="tenantId">租户id</param>
    /// <param name="parentId">父节点id</param>
    /// <param name="model">模型</param>
    /// <returns></returns>
    public async Task<M> AddAsync(long tenantId, long? parentId, M model)
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
                IsRoot = true,
                IsLeaf = false
            };

            //生成当前节点实体模型
            current = MakeChild(tenantId, parent, model);
            current.Index = 0;

            //保存根节点和当前节点,此处由于导航属性的问题,无法使用级联保存,所以使用分别保存
            await repository.AddAsync(parent);
            await repository.AddAsync(current);
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

                current = MakeChild(tenantId, parent, model);
                current.Index = await repository.MaxAsync(x => x.ParentId == parent.Id, x => x.Index) + 1;
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
    /// 生成子节点
    /// </summary>
    /// <param name="tenantId">租户Id</param>
    /// <param name="parent">父节点</param>
    /// <param name="model">当前节点模型</param>
    /// <returns>数据模型</returns>
    private T MakeChild(long tenantId, T parent, M model)
    {
        T current = _mapper.Map<T>(model);

        if (model.Id > 0)
        {
            current.Id = model.Id;
        }

        current.TenantId = tenantId;
        current.ParentId = parent.Id;
        current.IsLeaf = true;
        current.Level = parent.Level + 1;

        return current;
    }
}

