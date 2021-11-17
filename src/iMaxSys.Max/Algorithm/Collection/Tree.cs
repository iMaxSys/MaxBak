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

//https://www.sohu.com/a/347132712_468635

using System.Linq;
using System.Collections.Generic;

namespace iMaxSys.Max.Algorithm.Collection
{
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

    /// <summary>
    /// Tree
    /// </summary>
    public class Tree
    {
        /// <summary>
        /// 深度
        /// </summary>
        public int Deep { get; set; } = 0;

        /// <summary>
        /// 节点数
        /// </summary>
        public int NodeCount { get; set; } = 0;

        /// <summary>
        /// 叶节点数
        /// </summary>
        public int LeafCount { get; set; } = 0;

        /// <summary>
        /// 节点
        /// </summary>
        public IList<ITreeNode>? Nodes { get; set; }

        /// <summary>
        /// 节点存储
        /// </summary>
        public IList<ITreeStore>? Stores { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="stores">节点存储集</param>
        public Tree(IList<ITreeStore>? stores)
        {
            if (stores?.Count > 0)
            {
                Stores = stores;
                Nodes = GetTree(stores)?.Nodes;
            }
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="stores">节点集</param>
        public Tree(IList<ITreeNode> nodes)
        {
            Nodes = nodes;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="stores">树形存储集合</param>
        /// <returns></returns>
        public ITreeNode? GetTree(IList<ITreeStore> stores)
        {
            var store = stores.FirstOrDefault(x => x.Lv == 0);
            if (store != null)
            {
                ITreeNode root = MakeNode(store);
                SetNodes(stores, root);
                return root;
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
        private ITreeNode MakeNode(ITreeStore store)
        {
            return new TreeNode
            {
                Id = store.Id,
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
        /// 获取树型结构数据存储
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public IList<ITreeStore> GetStore(ITreeNode tree)
        {
            List<ITreeStore> stores = new();
            SetNodeStore(stores, tree, 0, 0);
            return stores;
        }

        /// <summary>
        /// 获取树型结构数据存储
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public IList<ITreeStore> GetStore(IList<ITreeNode> nodes)
        {
            ITreeNode tree = new TreeNode
            {
                Name = "root",
                Description = "",
                Nodes = new List<ITreeNode>()
            };
            tree.Nodes = nodes;
            return GetStore(tree);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stores"></param>
        /// <param name="node"></param>
        /// <param name="lv"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        private int SetNodeStore(IList<ITreeStore> stores, ITreeNode node, int lv, int deep)
        {
            int rv = lv + 1;

            ITreeStore store = new TreeStore()
            {
                Id = node.Id,
                Code = node.Code,
                Name = node.Name,
                Description = node.Description,
                Icon = node.Icon,
                Style = node.Style,
                Lv = lv,
                Deep = deep,
                Data = node.Data,
                Action = node.Action,
                Ext = node.Ext,
                Status = node.Status
            };

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
    }
}
