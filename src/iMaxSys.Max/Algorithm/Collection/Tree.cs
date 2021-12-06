//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Tree.cs
//摘要: 树型数据结构操作类
//说明:   
//
//当前：1.0
//作者：陶剑扬
//日期：2021-11-16
//----------------------------------------------------------------

namespace iMaxSys.Max.Algorithm.Collection;

#region TreeNode

/// <summary>
/// ITreeNode
/// </summary>
public interface ITreeNode
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Index
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// IsLeaf
    /// </summary>
    public bool IsLeaf { get; set; }

    /// <summary>
    /// Deep
    /// </summary>
    public int Deep { get; set; }

    /// <summary>
    /// Code
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Icon
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// SelectedIcon
    /// </summary>
    public string? SelectedIcon { get; set; }

    /// <summary>
    /// Style
    /// </summary>
    public string? Style { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    public string? Data { get; set; }

    /// <summary>
    /// Action
    /// </summary>
    public string? Action { get; set; }

    /// <summary>
    /// Ext
    /// </summary>
    public string? Ext { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Parent
    /// </summary>
    public ITreeNode? Parent { get; set; }

    /// <summary>
    /// node collection
    /// </summary>
    public IList<ITreeNode>? Nodes { get; set; }
}

/// <summary>
/// TreeNode
/// </summary>
public class TreeNode : ITreeNode
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Index
    /// </summary>
    public int Index { get; set; } = 0;

    /// <summary>
    /// IsLeaf
    /// </summary>
    public bool IsLeaf { get; set; } = true;

    /// <summary>
    /// Deep
    /// </summary>
    public int Deep { get; set; } = 0;

    /// <summary>
    /// Code
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Icon
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// SelectedIcon
    /// </summary>
    public string? SelectedIcon { get; set; }

    /// <summary>
    /// Style
    /// </summary>
    public string? Style { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    public string? Data { get; set; }

    /// <summary>
    /// Action
    /// </summary>
    public string? Action { get; set; }

    /// <summary>
    /// Ext
    /// </summary>
    public string? Ext { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    public int Status { get; set; } = 1;

    /// <summary>
    /// Parent
    /// </summary>
    public ITreeNode? Parent { get; set; }

    /// <summary>
    /// node collection
    /// </summary>
    public IList<ITreeNode>? Nodes { get; set; }
}

/// <summary>
/// 树型结构数据存储
/// </summary>
public interface ITreeStore
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Index
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// IsLeaf
    /// </summary>
    public bool IsLeaf { get; set; }

    /// <summary>
    /// Code
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Icon
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// SelectedIcon
    /// </summary>
    public string? SelectedIcon { get; set; }

    /// <summary>
    /// Style
    /// </summary>
    public string? Style { get; set; }

    /// <summary>
    /// Left value
    /// </summary>
    public int Lv { get; set; }

    /// <summary>
    /// Right value
    /// </summary>
    public int Rv { get; set; }

    /// <summary>
    /// Deep
    /// </summary>
    public int Deep { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    public string Data { get; set; }

    /// <summary>
    /// Action
    /// </summary>
    public string Action { get; set; }

    /// <summary>
    /// Ext
    /// </summary>
    public string Ext { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 树型结构数据存储
/// </summary>
public class TreeStore : ITreeStore
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Index
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// IsLeaf
    /// </summary>
    public bool IsLeaf { get; set; }

    /// <summary>
    /// Code
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Icon
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// SelectedIcon
    /// </summary>
    public string? SelectedIcon { get; set; }

    /// <summary>
    /// Style
    /// </summary>
    public string? Style { get; set; }

    /// <summary>
    /// Left value
    /// </summary>
    public int Lv { get; set; }

    /// <summary>
    /// Right value
    /// </summary>
    public int Rv { get; set; }

    /// <summary>
    /// Deep
    /// </summary>
    public int Deep { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    public string? Data { get; set; }

    /// <summary>
    /// Action
    /// </summary>
    public string? Action { get; set; }

    /// <summary>
    /// Ext
    /// </summary>
    public string? Ext { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    public int Status { get; set; }
}

#endregion

#region Tree

/// <summary>
/// Tree
/// </summary>
public class Tree
{
    const string TAG_ROOT = "root";

    /// <summary>
    /// root
    /// </summary>
    private ITreeNode? _root;

    /// <summary>
    /// 节点集(平面集)
    /// </summary>
    private IList<ITreeNode>? _nodeSet;

    /// <summary>
    /// 根/树结构
    /// </summary>
    public ITreeNode? Root { get => _root; }

    /// <summary>
    /// 深度
    /// </summary>
    public int Deep { get; } = 0;

    /// <summary>
    /// 索引数
    /// </summary>
    public int IndexCount { get; } = 0;

    /// <summary>
    /// 节点数
    /// </summary>
    public int NodeCount { get; } = 0;

    /// <summary>
    /// 叶节点数
    /// </summary>
    public int LeafCount { get; } = 0;

    /// <summary>
    /// 节点集(平面集)
    /// </summary>
    public IList<ITreeNode>? NodeSet { get => _nodeSet ??= GetAllNodes(); }

    /// <summary>
    /// 节点存储(平面存储)
    /// </summary>
    public IList<ITreeStore>? Stores { get; }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="stores">节点存储集</param>
    public Tree(IList<ITreeStore> stores)
    {
        if (stores.Count > 0)
        {
            Stores = stores;
            _root = GetRoot(stores);
            IndexCount = stores.Count;
        }
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="root">根节点</param>
    public Tree(ITreeNode root)
    {
        if (root.Nodes?.Count > 0)
        {
            _root = root;
            Stores = GetStore(root);
            IndexCount = Stores.Count;
        }
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="stores">树形存储集合</param>
    /// <returns></returns>
    private ITreeNode? GetRoot(IList<ITreeStore> stores)
    {
        //获取root store
        var store = stores.FirstOrDefault(x => x.Lv == 0);
        if (store != null)
        {
            _root = MakeNode(store);
            SetNodes(stores, _root);
            return Root;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 设置子节点
    /// </summary>
    /// <param name="stores">树形存错集合</param>
    /// <param name="node">节点</param>
    private void SetNodes(IList<ITreeStore> stores, ITreeNode node)
    {
        //获取当前节点的下一层子节点
        ITreeStore? current = stores.FirstOrDefault(x => x.Id == node.Id);

        if (current != null)
        {
            //获取所有子节点
            var list = stores.Where(x => x.Deep == current.Deep + 1 && x.Lv > current.Lv && x.Rv < current.Rv).OrderBy(x => x.Lv);
            if (list != null && list.Any())
            {
                node.Nodes = new List<ITreeNode>();
                foreach (var store in list)
                {
                    ITreeNode n = MakeNode(store);
                    n.Parent = node;
                    n.Deep = node.Deep + 1;
                    SetNodes(stores, n);
                    node.Nodes.Add(n);
                }
            }
        }
    }

    /// <summary>
    /// 生成节点
    /// </summary>
    /// <param name="store">树形存储</param>
    /// <returns></returns>
    private static ITreeNode MakeNode(ITreeStore store)
    {
        return new TreeNode
        {
            Id = store.Id,
            Index = store.Index,
            IsLeaf = store.IsLeaf,
            Code = store.Code,
            Name = store.Name,
            Description = store.Description,
            Icon = store.Icon,
            SelectedIcon = store.SelectedIcon,
            Style = store.Style,
            Data = store.Data,
            Action = store.Action,
            Ext = store.Ext,
            Status = store.Status,
        };
    }

    /// <summary>
    /// 生成存错
    /// </summary>
    /// <param name="store">树形节点</param>
    /// <returns></returns>
    private static ITreeStore MakeStore(ITreeNode node)
    {
        return new TreeStore
        {
            Id = node.Id,
            Index = node.Index,
            IsLeaf = node.IsLeaf,
            Code = node.Code,
            Name = node.Name,
            Description = node.Description,
            Icon = node.Icon,
            SelectedIcon = node.SelectedIcon,
            Style = node.Style,
            Data = node.Data,
            Action = node.Action,
            Ext = node.Ext,
            Status = node.Status,
        };
    }

    /// <summary>
    /// 获取树型结构数据存储
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public IList<ITreeStore> GetStore(ITreeNode root)
    {
        IList<ITreeStore> stores = new List<ITreeStore>();
        SetNodeStore(stores, root);
        return stores;
    }

    /// <summary>
    /// 设置节点存储
    /// </summary>
    /// <param name="stores"></param>
    /// <param name="node"></param>
    /// <param name="lv"></param>
    /// <param name="deep"></param>
    /// <returns></returns>
    private int SetNodeStore(IList<ITreeStore> stores, ITreeNode node, int lv = 0, int deep = 0)
    {
        int rv = lv + 1;

        ITreeStore store = MakeStore(node);
        store.Lv = lv;
        store.Deep = deep;

        stores.Add(store);

        if (node.Nodes != null)
        {
            foreach (var item in node.Nodes)
            {
                rv = SetNodeStore(stores, item, rv, deep + 1) + 1;
            }
        }

        store.Rv = rv;

        return rv;
    }

    /// <summary>
    /// IList<ITreeNode>
    /// </summary>
    private IList<ITreeNode>? GetAllNodes()
    {
        SetAllNodes();
        return _nodeSet;
    }

    /// <summary>
    /// 设置所有节点
    /// </summary>
    /// <returns></returns>
    private void SetAllNodes()
    {
        if (Root?.Nodes != null && Root!.Nodes.Count > 0)
        {
            _nodeSet = new List<ITreeNode>();
            CopyNodes(Root.Nodes, _nodeSet);
        }
    }

    /// <summary>
    /// 设置所有节点
    /// 传参和Add是基于性能考虑
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    private void CopyNodes(IList<ITreeNode> source, IList<ITreeNode> target)
    {
        foreach (var item in source)
        {
            target.Add(item);
            if (item.Nodes != null && item.Nodes.Any())
            {
                CopyNodes(item.Nodes, target);
            }
        }
    }

    /// <summary>
    /// 新增同级节点(id=0为增加第一级节点)
    /// </summary>
    /// <param name="index">索引位置</param>
    /// <param name="treeNode">节点</param>
    public void Add(long id, ITreeNode treeNode)
    {
        //获取目标节点
        var node = _nodeSet?.FirstOrDefault(x => x.Id == id);
        if (node != null)
        {
            //操作tree, 先找到父节点, 再获取子集插入
            if (node.Parent != null)
            {
                treeNode.Deep = node.Deep;
                treeNode.Parent = node.Parent;
                node.Parent.Nodes!.Insert(node.Parent.Nodes!.IndexOf(node) + 1, treeNode);
            }
            else
            {
                throw new Exception($"节点:{id}无父节点");
            }
        }
        else
        {
            throw new Exception($"无效的目标节点:{id}");
        }
    }



    /// <summary>
    /// 新增子节点
    /// </summary>
    /// <param name="id"></param>
    /// <param name="treeNode"></param>
    public void AddSub(long id, ITreeNode treeNode)
    {
        if (id > 0)
        {
            //获取目标节点
            var node = _nodeSet?.FirstOrDefault(x => x.Id == id);
            if (node != null)
            {
                treeNode.Deep = node.Deep + 1;
                treeNode.Parent = node;

                node.Nodes ??= new List<ITreeNode>();
                node.Nodes.Add(treeNode);
            }
            else
            {
                throw new Exception($"无法新增子节点, 无效的目标节点:{id}");
            }
        }
        else
        {
            _root ??= new TreeNode
            {
                Name = TAG_ROOT,
                Nodes = new List<ITreeNode>()
            };

            treeNode.Deep = _root.Deep + 1;
            treeNode.Parent = _root;
            _root.Nodes = new List<ITreeNode> { treeNode };
        }
    }

    /// <summary>
    /// 移除节点(包含子节点无法删除)
    /// </summary>
    /// <param name="index"></param>
    public void Remove(long id)
    {
        //获取目标节点
        var node = _nodeSet?.FirstOrDefault(x => x.Id == id);
        if (node != null)
        {
            if (node.Nodes?.Count > 0)
            {
                throw new Exception($"无法删除! 目标节点:{id}下还有子节点.");
            }
            else
            {
                node.Parent!.Nodes!.Remove(node);
            }
        }
        else
        {
            throw new Exception($"无法删除! 无效的目标节点:{id}.");
        }
    }

    /// <summary>
    /// 移动节点
    /// </summary>
    /// <param name="id"></param>
    /// <param name="index"></param>
    public void Move(int id, int index)
    {

    }
}

#endregion