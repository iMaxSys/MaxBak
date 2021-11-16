
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
//日期：2018-03-07
//----------------------------------------------------------------

using System.Linq;
using System.Collections.Generic;

namespace iMaxSys.Max.Algorithm.Collection
{
    public interface ITreeNode
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public string Style { get; set; }

        public string Data { get; set; }

        public string Action { get; set; }

        public string Ext { get; set; }

        public int Status { get; set; }

        public List<ITreeNode> Nodes { get; set; }
    }

    public class TreeNode : ITreeNode
    {
        public long Id { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Icon { get; set; }

        public string? Style { get; set; }

        public string? Data { get; set; }

        public string? Action { get; set; }

        public string? Ext { get; set; }

        public int Status { get; set; }

        public List<ITreeNode>? Nodes { get; set; }
    }

    /// <summary>
    /// 树型结构数据存储
    /// </summary>
    public interface ITreeStore
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public string Style { get; set; }

        public int Lv { get; set; }

        public int Rv { get; set; }

        public int Deep { get; set; }

        public string Data { get; set; }

        public string Action { get; set; }

        public string Ext { get; set; }

        public int Status { get; set; }
    }

    /// <summary>
    /// 树型结构数据存储
    /// </summary>
    public class TreeStore : ITreeStore
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public string Style { get; set; }

        public int Lv { get; set; }

        public int Rv { get; set; }

        public int Deep { get; set; }

        public string Data { get; set; }

        public string Action { get; set; }

        public string Ext { get; set; }

        public int Status { get; set; } = 1;
    }

    public class Tree
    {
        public static ITreeNode GetTree(List<ITreeStore> stores)
        {
            var store = stores.FirstOrDefault(x => x.Lv == 0);
            ITreeNode tree = new TreeNode
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
                Status = store.Status
            };
            SetNodes(stores, tree);
            return tree;
        }

        private static void SetNodes(List<ITreeStore> stores, ITreeNode node)
        {
            //获取当前节点的下一层子节点
            ITreeStore current = stores.FirstOrDefault(x => x.Id == node.Id);
            var list = stores.Where(x => x.Deep == current.Deep + 1 && x.Lv > current.Lv && x.Rv < current.Rv).OrderBy(x => x.Lv);

            if (list.Count() > 0)
            {
                node.Nodes = new List<ITreeNode>();
                foreach (var item in list)
                {
                    ITreeNode n = new TreeNode
                    {
                        Id = item.Id,
                        Code = item.Code,
                        Name = item.Name,
                        Description = item.Description,
                        Icon = item.Icon,
                        Style = item.Style,
                        Data = item.Data,
                        Action = item.Action,
                        Ext = item.Ext,
                        Status = item.Status,
                    };
                    SetNodes(stores, n);
                    node.Nodes.Add(n);
                }
            }
        }

        /// <summary>
        /// 获取树型结构数据存储
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public static List<ITreeStore> GetStore(ITreeNode tree)
        {
            List<ITreeStore> stores = new List<ITreeStore>();
            SetNodeStore(stores, tree, 0, 0);
            return stores;
        }

        /// <summary>
        /// 获取树型结构数据存储
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static List<ITreeStore> GetStore(List<ITreeNode> nodes)
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

        private static int SetNodeStore(List<ITreeStore> stores, ITreeNode node, int lv, int deep)
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

            foreach (var item in node.Nodes)
            {
                rv = SetNodeStore(stores, item, rv, deep + 1) + 1;
            }

            store.Rv = rv;

            return rv;
        }
    }
}
