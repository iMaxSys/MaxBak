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

namespace iMaxSys.Max.Algorithm.Collection
{
    #region ITreeNode

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
        /// Node collection
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

    /// <summary>
    /// Tree
    /// </summary>
    public class Tree
    {
        /// <summary>
        /// 根
        /// </summary>
        public ITreeNode? Root { get; set; }

        /// <summary>
        /// 深度
        /// </summary>
        public int Deep { get; set; } = 0;

        /// <summary>
        /// 索引数
        /// </summary>
        public int IndexCount { get; set; } = 0;

        /// <summary>
        /// 节点数
        /// </summary>
        public int NodeCount { get; set; } = 0;

        /// <summary>
        /// 叶节点数
        /// </summary>
        public int LeafCount { get; set; } = 0;

        /// <summary>
        /// 节点集
        /// </summary>
        public IEnumerable<ITreeNode>? NodeSet { get { return _nodeSet ??= GetAll(); } }

        /// <summary>
        /// 节点存储
        /// </summary>
        public IList<ITreeStore>? Stores { get; }

        /// <summary>
        /// 节点集
        /// </summary>
        private IEnumerable<ITreeNode>? _nodeSet;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="stores">节点存储集</param>
        public Tree(IList<ITreeStore> stores)
        {
            if (stores.Count > 0)
            {
                Stores = stores;
                Root = GetRoot(stores);
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
                Root = root;
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
                Root = MakeNode(store);
                SetNodes(stores, Root);
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
                var list = stores.Where(x => x.Deep == current.Deep + 1 && x.Lv > current.Lv && x.Rv < current.Rv).OrderBy(x => x.Lv);

                foreach (var store in list)
                {
                    node.Nodes = new List<ITreeNode>();
                    ITreeNode n = MakeNode(store);
                    SetNodes(stores, n);
                    node.Nodes.Add(n);
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
        /// 获取所有节点
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ITreeNode>? GetAll()
        {
            return Root?.Nodes != null && Root!.Nodes.Count > 0 ? GetAll(Root!.Nodes) : null;
        }

        /// <summary>
        /// 获取所有节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private IEnumerable<ITreeNode> GetAll(IEnumerable<ITreeNode> nodes)
        {
            IEnumerable<ITreeNode> list = new List<ITreeNode>();

            foreach (var item in nodes)
            {
                list = list.Append(item);
                if (item.Nodes != null && item.Nodes.Any())
                {
                    list = list.Concat(GetAll(item.Nodes));
                }
                //加完直接点后是否要清楚nodes属性
                //item.Nodes.Clear();
            }
            return list;
        }

        /// <summary>
        /// 新增节点
        /// </summary>
        /// <param name="index">索引位置</param>
        /// <param name="treeNode">节点</param>
        public void Add(int index, ITreeNode treeNode)
        {

        }

        /// <summary>
        /// 新增子节点
        /// </summary>
        /// <param name="index"></param>
        /// <param name="treeNode"></param>
        public void AddSub(int index, ITreeNode treeNode)
        {
        }

        /// <summary>
        /// 移除节点
        /// </summary>
        /// <param name="index"></param>
        public void Remove(int index)
        {

        }

        /// <summary>
        /// 移动节点
        /// </summary>
        /// <param name="index"></param>
        /// <param name="indexy"></param>
        public void Move(int index, int indexy)
        {

        }
    }
}
