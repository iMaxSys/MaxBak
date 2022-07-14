//----------------------------------------------------------------
//Copyright (C) 2016-2025 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: Tree.cs
//摘要: 树 
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2018-03-07
//----------------------------------------------------------------


using iMaxSys.Max.Common.Enums;

namespace iMaxSys.Max.Collection.Trees;

//=======================范型树============================

public static class TreeExtensions
{
	/// <summary>
	/// 平面化节点
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="nodes"></param>
	/// <param name="childrenSelector"></param>
	/// <returns></returns>
	public static IEnumerable<ITree<T>> Flatten<T>(this IEnumerable<ITree<T>> nodes, Func<ITree<T>, IEnumerable<ITree<T>>> childrenSelector) where T : ITreeNode
	{
		return nodes.Concat(nodes.SelectMany(c => childrenSelector(c).Flatten(childrenSelector)));
	}

	/// <summary>
	/// 平面化数据
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="nodes"></param>
	/// <param name="childrenSelector"></param>
	/// <returns></returns>
	public static IEnumerable<T> FlattenData<T>(this IEnumerable<ITree<T>> nodes, Func<ITree<T>, IEnumerable<ITree<T>>> childrenSelector) where T : ITreeNode
	{
		return nodes.Select(x => x.Data).Concat(nodes.SelectMany(c => childrenSelector(c).FlattenData(childrenSelector).Select(x => x)));
	}

	/// <summary> Converts given list to tree. </summary>
	/// <typeparam name="T">Custom data type to associate with tree node.</typeparam>
	/// <param name="items">The collection items.</param>
	/// <param name="parentSelector">Expression to select parent.</param>
	public static ITree<T>? ToTree<T>(this IEnumerable<T> items, Func<T, T, bool> parentSelector) where T : ITreeNode
	{
		if (items == null) throw new ArgumentNullException(nameof(items));
		var lookup = items.ToLookup(item => items.FirstOrDefault(parent => parentSelector(parent, item)), child => child);
		return Tree<T>.FromLookup(lookup!);
	}
}

/// <summary>
/// 树形接口
/// </summary>
public interface ITreeNode
{
	/// <summary>
	/// Id
	/// </summary>
	public long Id { get; set; }

	/// <summary>
	/// ParentId
	/// </summary>
	public long? ParentId { get; set; }

	/// <summary>
	/// TenantId
	/// </summary>
	public long TenantId { get; set; }

	/// <summary>
	/// XppId
	/// </summary>
	public long XppId { get; set; }

	/// <summary>
	/// Name
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// 左值
	/// </summary>
	public int Lv { get; set; }

	/// <summary>
	/// 右值
	/// </summary>
	public int Rv { get; set; }

	/// <summary>
	/// 索引/序号
	/// </summary>
	public int Index { get; set; }

	/// <summary>
	/// Level
	/// </summary>
	public int Level { get; set; }

	/// <summary>
	/// IsLeaf
	/// </summary>
	public bool IsRoot { get; set; }

	/// <summary>
	/// IsLeaf
	/// </summary>
	public bool IsLeaf { get; set; }

	/// <summary>
	/// Type
	/// </summary>
	public int Type { get; set; }

	/// <summary>
	/// Code
	/// </summary>
	public string? Code { get; set; }

	/// <summary>
	/// QuickCode
	/// </summary>
	public string? QuickCode { get; set; }

	/// <summary>
	/// Value
	/// </summary>
	public string? Value { get; set; }

	/// <summary>
	/// 别名
	/// </summary>
	public string? Alias { get; set; }

	/// <summary>
	/// Description
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Style
	/// </summary>
	public string? Style { get; set; }

	/// <summary>
	/// SelectedStyle
	/// </summary>
	public string? SelectedStyle { get; set; }

	/// <summary>
	/// Icon
	/// </summary>
	public string? Icon { get; set; }

	/// <summary>
	/// SelectedIcon
	/// </summary>
	public string? SelectedIcon { get; set; }

	/// <summary>
	/// Ext
	/// </summary>
	public string? Ext { get; set; }

	/// <summary>
	/// 状态
	/// </summary>
	public Status Status { get; set; }
}

/// <summary>
/// 部门模型
/// </summary>
public abstract class TreeView : ITreeNode
{
	/// <summary>
	/// Id
	/// </summary>
	public long Id { get; set; }

	/// <summary>
	/// ParentId
	/// </summary>
	public long? ParentId { get; set; }

	/// <summary>
	/// TenantId
	/// </summary>
	public long TenantId { get; set; }

	/// <summary>
	/// XppId
	/// </summary>
	public long XppId { get; set; }

	/// <summary>
	/// Name
	/// </summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// 左值
	/// </summary>
	public int Lv { get; set; }

	/// <summary>
	/// 右值
	/// </summary>
	public int Rv { get; set; }

	/// <summary>
	/// 索引/序号
	/// </summary>
	public int Index { get; set; }

	/// <summary>
	/// Level
	/// </summary>
	public int Level { get; set; }

	/// <summary>
	/// IsLeaf
	/// </summary>
	public bool IsRoot { get; set; }

	/// <summary>
	/// IsLeaf
	/// </summary>
	public bool IsLeaf { get; set; }

	/// <summary>
	/// Type
	/// </summary>
	public int Type { get; set; }

	/// <summary>
	/// Code
	/// </summary>
	public string? Code { get; set; }

	/// <summary>
	/// QuickCode
	/// </summary>
	public string? QuickCode { get; set; }

	/// <summary>
	/// Value
	/// </summary>
	public string? Value { get; set; }

	/// <summary>
	/// 别名
	/// </summary>
	public string? Alias { get; set; }

	/// <summary>
	/// Description
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Style
	/// </summary>
	public string? Style { get; set; }

	/// <summary>
	/// SelectedStyle
	/// </summary>
	public string? SelectedStyle { get; set; }

	/// <summary>
	/// Icon
	/// </summary>
	public string? Icon { get; set; }

	/// <summary>
	/// SelectedIcon
	/// </summary>
	public string? SelectedIcon { get; set; }

	/// <summary>
	/// Ext
	/// </summary>
	public string? Ext { get; set; }

	/// <summary>
	/// Status
	/// </summary>
	public Status Status { get; set; }
}

/// <summary>
/// 树形接口
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ITree<T> where T : ITreeNode
{
	/// <summary>
	/// 数据
	/// </summary>
	T Data { get; }

	/// <summary>
	/// IsRoot
	/// </summary>
	bool IsRoot { get; }

	/// <summary>
	/// IsLeaf
	/// </summary>
	bool IsLeaf { get; }

	/// <summary>
	/// Level
	/// </summary>
	int Level { get; }

	/// <summary>
	/// 父节点
	/// </summary>
	[JsonIgnore]
	ITree<T>? Parent { get; }

	/// <summary>
	/// 子节点集
	/// </summary>
	LinkedList<ITree<T>> Children { get; }

	/// <summary>
	/// 获取父节点集
	/// </summary>
	/// <returns></returns>
	List<T> GetParents();

	/// <summary>
	/// 平面化树形节点
	/// </summary>
	/// <returns></returns>
	IEnumerable<ITree<T>> Flatten();

	/// <summary>
	/// 平面化树形数据
	/// </summary>
	/// <returns></returns>
	IEnumerable<T> FlattenData();

	/// <summary>
	/// ToJson
	/// </summary>
	/// <returns></returns>
	string ToJson();
}

/// <summary>
/// 树
/// </summary>
/// <typeparam name="T"></typeparam>
public class Tree<T> : ITree<T> where T : ITreeNode
{
	/// <summary>
	/// 数据
	/// </summary>
	public T Data { get; }

	/// <summary>
	/// 父节点
	/// </summary>
	[JsonIgnore]
	public ITree<T>? Parent { get; private set; }

	/// <summary>
	/// 子节点
	/// </summary>
	public LinkedList<ITree<T>> Children { get; }

	/// <summary>
	/// 是否是根节点
	/// </summary>
	public bool IsRoot => Parent == null;

	/// <summary>
	/// 是否是叶节点
	/// </summary>
	public bool IsLeaf => Children.Count == 0;

	/// <summary>
	/// 级数
	/// </summary>
	public int Level => Parent?.Level + 1 ?? 0;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="data"></param>
	public Tree(T data)
	{
		Children = new LinkedList<ITree<T>>();
		Data = data;
	}

	/// <summary>
	/// 加载子节点
	/// </summary>
	/// <param name="lookup"></param>
	private void LoadChildren(ILookup<T, T> lookup)
	{
		foreach (var data in lookup[Data])
		{
			var child = new Tree<T>(data) { Parent = this };
			Children.AddLast(child);
			child.LoadChildren(lookup);
		}
	}

	/// <summary>
	/// 生成树
	/// </summary>
	/// <param name="lookup"></param>
	/// <returns></returns>
	public static Tree<T>? FromLookup(ILookup<T, T> lookup)
	{
		var rootData = lookup.FirstOrDefault(x => x.Key is null);
		if (rootData is not null)
		{
			var root = new Tree<T>(rootData.First());
			root.LoadChildren(lookup);
			return root;
		}
		else
		{
			return null;
		}
	}

	/// <summary>
	/// 获取父节点集
	/// </summary>
	/// <returns></returns>
	public List<T> GetParents()
	{
		ITree<T> node = this;
		List<T> parentNodes = new();
		while (true)
		{
			if (node?.Parent is not null)
			{
				parentNodes.Add(node.Parent.Data);
				node = node.Parent;
			}
			else
			{
				break;
			}
		}
		return parentNodes;
	}

	/// <summary>
	/// 平面化树形节点
	/// </summary>
	/// <returns></returns>
	public IEnumerable<ITree<T>> Flatten()
	{
		List<ITree<T>> list = new() { this };
		return list.Concat(Children.Flatten(x => x.Children));
	}

	/// <summary>
	/// 平面化树形数据
	/// </summary>
	/// <returns></returns>
	public IEnumerable<T> FlattenData()
	{
		List<T> list = new() { this.Data };
		return list.Concat(Children.FlattenData(x => x.Children));
	}

	/// <summary>
	/// ToJson
	/// </summary>
	/// <returns></returns>
	public string ToJson()
	{
		JsonSerializerOptions options = new()
		{
			ReferenceHandler = ReferenceHandler.IgnoreCycles,
			WriteIndented = true
		};

		return JsonSerializer.Serialize(this, options);
	}
}

//=======================范型树============================

/// <summary>
/// 左右值Tree
/// </summary>
public class LrTree : LrTreeNode
{
	/// <summary>
	/// 根标识
	/// </summary>
	const string TAG_ROOT = "root";

	/// <summary>
	/// 节点集(平面集)
	/// </summary>
	//private readonly List<TreeNode>? _nodeSet;

	///// <summary>
	///// 总深度
	///// </summary>
	//public int TotalDepth { get; } = 0;

	///// <summary>
	///// 节点集(平面集)
	///// </summary>
	//[JsonIgnore]
	//public List<TreeNode>? NodeSet { get => _nodeSet; }

	/// <summary>
	/// 节点存储(平面存储)
	/// </summary>
	//public List<TreeStore>? StoreSet { get; }

	/// <summary>
	/// 构造
	/// </summary>
	public LrTree() : this(0, TAG_ROOT)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="id"></param>
	/// <param name="name"></param>
	public LrTree(long id, string name) : base(id, name)
	{
		Stores.Add(this);
	}
}

/// <summary>
/// 树形节点
/// </summary>
public class LrTreeNode
{
	internal int _lv = 0;
	internal int _rv = 1;
	internal int _depth = 0;
	internal int _index = 0;

	internal LrTreeNode? _parent;
	internal LrTreeNode? _previous;
	internal LrTreeNode? _next;

	private List<LrTreeNode>? _stores;

	/// <summary>
	/// 节点集
	/// </summary>
	protected List<LrTreeNode> Stores { get => _parent?.Stores ?? (_stores ??= new()); }

	/// <summary>
	/// Id
	/// </summary>
	public long Id { get; set; }

	/// <summary>
	/// Index
	/// </summary>
	public int Index { get => _index; }

	/// <summary>
	/// 左值
	/// </summary>
	public int Lv { get => _lv; }

	/// <summary>
	/// 右值
	/// </summary>
	public int Rv { get => _rv; }

	/// <summary>
	/// IsLeaf
	/// </summary>
	public bool IsLeaf { get => NodesCount == 0; }

	/// <summary>
	/// Depth
	/// </summary>
	public int Depth { get => _depth = _parent?._depth + 1 ?? 0; }

	/// <summary>
	/// Code
	/// </summary>
	public string? Code { get; set; }

	/// <summary>
	/// QuickCode
	/// </summary>
	public string? QuickCode { get; set; }

	/// <summary>
	/// Value
	/// </summary>
	public string? Value { get; set; }

	/// <summary>
	/// Name
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// 别名
	/// </summary>
	public string? Alias { get; set; }

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
	/// Parent
	/// </summary>
	[JsonIgnore]
	public LrTreeNode? Parent { get => _parent; }

	/// <summary>
	/// Previous
	/// </summary>
	[JsonIgnore]
	public LrTreeNode? Previous { get => _previous; }

	/// <summary>
	/// Next
	/// </summary>
	[JsonIgnore]
	public LrTreeNode? Next { get => _next; }

	/// <summary>
	/// 子节点集
	/// </summary>
	public LrTreeNodeCollection Nodes { get; }

	/// <summary>
	/// 子节点数(直接子节点)
	/// </summary>
	public int NodesCount { get => Nodes?.Count ?? 0; }

	/// <summary>
	/// 所有节点
	/// </summary>
	[JsonIgnore]
	public LrTreeNodeCollection? AllNodes { get; }

	/// <summary>
	/// 所有子节点数(包括直接和间接)
	/// </summary>
	public int AllNodesCount { get; } = 0;

	/// <summary>
	/// 所有叶节点
	/// </summary>
	[JsonIgnore]
	public LrTreeNodeCollection? AllLeaves { get; }

	/// <summary>
	/// 所有叶节点数(包括直接和间接)
	/// </summary>
	public int AllLeavesCount { get; } = 0;

	/// <summary>
	/// Status
	/// </summary>
	public int Status { get; set; } = 1;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="id">id</param>
	/// <param name="name">名称</param>
	public LrTreeNode(long id, string name) : this(id, name, null)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="id">id</param>
	/// <param name="name">名称</param>
	/// <param name="actiion">actiion</param>
	public LrTreeNode(long id, string name, string? action = null) : this(id, name, action, null)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="id">id</param>
	/// <param name="name">名称</param>
	/// <param name="action">action</param>
	/// <param name="value">描述</param>
	public LrTreeNode(long id, string name, string? action = null, string? value = null) : this(id, name, action, value, null)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="id"></param>
	/// <param name="name"></param>
	/// <param name="action"></param>
	/// <param name="value"></param>
	/// <param name="description"></param>
	/// <param name="alias"></param>
	/// <param name="code"></param>
	/// <param name="quickCode"></param>
	/// <param name="data"></param>
	/// <param name="style"></param>
	/// <param name="icon"></param>
	/// <param name="selectedIcon"></param>
	/// <param name="ext"></param>
	public LrTreeNode(long id, string name, string? action = null, string? value = null, string? description = null, string? alias = null, string? code = null, string? quickCode = null, string? data = null, string? style = null, string? icon = null, string? selectedIcon = null, string? ext = null)
	{
		Id = id;
		Name = name;
		Action = action;
		Value = value;
		Description = description;
		Alias = alias;
		Code = code;
		QuickCode = quickCode;
		Data = data;
		Style = style;
		Icon = icon;
		SelectedIcon = selectedIcon;
		Ext = ext;

		Nodes = new LrTreeNodeCollection(this);

		//事件绑定
		Nodes.AddNodeEvent += Nodes_AddNodeEvent;
		Nodes.InsertNodeEvent += Nodes_InsertNodeEvent;
		Nodes.RemoveNodeEvent += Nodes_RemoveNodeEvent;
		Nodes.ClearNodeEvent += Nodes_ClearNodeEvent;
	}

	/// <summary>
	/// 设置父节点
	/// </summary>
	/// <param name="parent"></param>
	internal void SetParent(LrTreeNode parent)
	{
		_parent = parent;
		_stores = parent._stores;
	}

	/// <summary>
	/// 设置节点
	/// </summary>
	/// <param name="parent"></param>
	/// <param name="target"></param>
	/// <param name="offset"></param>
	//internal void Set(TreeNode? parent, TreeNode? target, int offset)
	//{
	//    _parent = parent;
	//    _index =
	//}

	/// <summary>
	/// Add
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void Nodes_AddNodeEvent(object? sender, AddLrNodeEventArgs e)
	{
		LrTreeNode? last = Stores.Where(x => x.Parent?.Id == e.Parent.Id).LastOrDefault();
		//有同级子节点
		if (last is not null)
		{
			e.Current._lv = last.Rv + 1;
			e.Current._rv = e.Current.Lv + 1;
			e.Current._previous = last;
			e.Current._parent = last.Parent;
			e.Current._next = last.Next;
			e.Current._index = last.Index + 1;
			e.Current._depth = last.Depth;
			last._next = e.Current;
		}
		else
		{
			e.Current._lv = e.Parent.Lv + 1;
			e.Current._rv = e.Current.Lv + 1;
			e.Current._index = e.Parent.Index + 1;
			e.Current._depth = e.Parent.Depth + 1;
			e.Current._previous = e.Parent;
			e.Current._next = e.Parent;
			e.Parent._next = e.Current;
		}

		e.Parent._rv = e.Current._rv + 1;

		//加入当前节点
		Stores.Add(e.Current);

		//后续节点+2
		Stores.Where(x => x.Lv > e.Current.Rv && x.Lv > 0).ForEach(x => { x._lv += 2; x._rv += 2; x._index += 1; });

		//刷新根节点
		RefreshRoot();
	}

	/// <summary>
	/// Remove
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void Nodes_RemoveNodeEvent(object? sender, RemoveLrNodeEventArgs e)
	{
		//当前节点左右差
		int co = e.Current.Rv - e.Current.Lv + 1;
		//当前节点总数(自身+所有子节点)
		int cc = co / 2;

		//移除本身和子节点
		Stores.Where(x => x.Lv >= e.Current.Lv && x.Rv <= e.Current.Rv).ForEach(x => Stores.Remove(x));

		//后续节点-co
		Stores.Where(x => x.Lv > e.Current.Rv).ForEach(x => { x._lv -= co; x._rv -= co; x._index -= cc; });

		RefreshRoot();
	}

	private void Nodes_InsertNodeEvent(object? sender, InsertLrNodeEventArgs e)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// 移动节点
	/// </summary>
	/// <param name="target"></param>
	/// <param name="current"></param>
	/// <param name="before"></param>
	public void Insert(LrTreeNode target, LrTreeNode current, bool before = true)
	{
		int lv = target.Lv;
		int rv = target.Rv;
		int index = target.Index;

		if (before)
		{
			//替换
			current._lv = target._lv;
			current._rv = target._rv;
			current._index = target._index;
			current._parent = target._parent;
			//后续节点+2

			//后续节点递增
			Stores.Where(x => x.Lv >= target.Lv && x.Lv > 0).ForEach(x => { x._lv += 2; x._rv += 2; x._index++; });

			current._lv = lv;
			current._rv = rv;
			current._index = index;
			current._previous = target.Previous;
			current._depth = target.Depth;
			current._next = target;
			target._previous!._next = current;
			target._previous = current;

			//加入当前节点
			Stores.Add(current);

			RefreshRoot();
		}
		else
		{
			current._lv = target.Rv + 1;
			current._rv = current.Lv + 1;
			current._previous = target;
			current._parent = target.Parent;
			current._next = target.Next;
			current._index = target.Index + 1;
			current._depth = target.Depth;
			target._next = current;
			target._next._previous = current;

			//加入当前节点
			Stores.Add(current);

			//后续节点+2
			Stores.Where(x => x.Rv > current.Rv && x.Lv > 0).ForEach(x => { x._lv += 2; x._rv += 2; x._index += 1; });

			//刷新根节点
			RefreshRoot();
		}
	}

	/// <summary>
	/// 移动节点
	/// </summary>
	/// <param name="target"></param>
	/// <param name="current"></param>
	/// <param name="before"></param>
	public void Move(LrTreeNode target, LrTreeNode current, bool before = true)
	{
		//当前节点左右差
		int co = current.Rv - current.Lv + 1 + (before ? 0 : -2);
		//当前节点总数(自身+所有子节点)
		int cc = co / 2;
		//lv偏移量
		int lo = current.Lv - target.Lv + (before ? 0 : -2);
		//节点偏移量
		int no = (current.Lv - target.Lv + 1) / 2 - (before ? 0 : 1);
		//深度偏移量
		int eo = current.Depth - target.Depth;

		if (before)
		{
			//区间节点-右节点和索引
			Stores.Where(x => (before ? x.Lv >= target.Lv : x.Lv > target.Rv) && x.Rv < current.Lv).ForEach(x => { x._rv += co; });

			//区间节点-左节点和索引
			Stores.Where(x => (before ? x.Lv >= target.Lv : x.Lv > target.Rv) && x.Lv < current.Lv).ForEach(x => { x._lv += co; x._index += cc; });

			//自身及子节点
			Stores.Where(x => x.Lv >= current.Lv && x.Rv <= current.Rv).ForEach(x => { x._lv -= lo; x._rv -= lo; x._index -= no; x._depth -= eo; });
		}
		current._parent = target._parent;
		current._depth = target._depth;
	}

	/// <summary>
	/// Clear
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void Nodes_ClearNodeEvent(object? sender, ClearLrNodeEventArgs e)
	{
		int count = e.Current.Nodes.Count;
		Stores.RemoveRange(e.Current.Index, e.Current.Nodes.Count);

		//获取下一节点
		var next = Stores.FirstOrDefault(x => x.Lv == e.Current.Rv + 1);
		e.Current._next = next;
		next!._parent = e.Current;

		Stores.Where(x => x.Lv > e.Current.Lv && x.Rv < e.Current.Rv).ForEach(x => Stores.Remove(x));

		//后续节点-countx2
		Stores.Where(x => x.Lv > 0 && x.Lv > e.Current.Rv).ForEach(x => { x._lv -= count * 2; x._rv -= count * 2; x._index -= count; });

		RefreshRoot();
	}

	public string ToJson()
	{
		JsonSerializerOptions options = new()
		{
			ReferenceHandler = ReferenceHandler.IgnoreCycles,
			WriteIndented = true
		};

		return JsonSerializer.Serialize(this, options);
	}

	/// <summary>
	/// 刷新根节点
	/// </summary>
	private void RefreshRoot()
	{
		Stores[0]._lv = 0;
		Stores[0]._rv = Stores.Count * 2 - 1;
	}
}

/// <summary>
/// 树形节点集合
/// </summary>
public class LrTreeNodeCollection : IList<LrTreeNode>
{
	/// <summary>
	/// 节点集
	/// </summary>
	private readonly List<LrTreeNode> _list;

	/// <summary>
	/// _parent
	/// </summary>
	private readonly LrTreeNode _parent;

	/// <summary>
	/// Parent
	/// </summary>
	public LrTreeNode Parent { get => _parent; }

	/// <summary>
	/// 节点数
	/// </summary>
	public int Count => _list?.Count ?? 0;

	/// <summary>
	/// IsReadOnly
	/// </summary>
	bool ICollection<LrTreeNode>.IsReadOnly => false;

	/// <summary>
	/// 索引器
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	/// <exception cref="NullReferenceException"></exception>
	/// <exception cref="IndexOutOfRangeException"></exception>
	public LrTreeNode this[int index]
	{
		get
		{
			if (_list == null || _list.Count == 0)
			{
				throw new NullReferenceException();
			}
			if (index > _list.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			return _list[index];
		}
		set
		{
			if (_list == null || _list.Count == 0)
			{
				throw new NullReferenceException();
			}
			if (index > _list.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			_list[index] = value;
		}
	}

	/// <summary>
	/// 新增节点事件
	/// </summary>
	public event EventHandler<AddLrNodeEventArgs>? AddNodeEvent;

	/// <summary>
	/// 插入节点事件
	/// </summary>
	public event EventHandler<InsertLrNodeEventArgs>? InsertNodeEvent;

	/// <summary>
	/// 移除节点事件
	/// </summary>
	public event EventHandler<RemoveLrNodeEventArgs>? RemoveNodeEvent;

	/// <summary>
	/// 移动节点事件
	/// </summary>
	public event EventHandler<MoveLrNodeEventArgs>? MoveNodeEvent;

	/// <summary>
	/// 清空节点事件
	/// </summary>
	public event EventHandler<ClearLrNodeEventArgs>? ClearNodeEvent;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="parent">父节点</param>
	public LrTreeNodeCollection(LrTreeNode parent)
	{
		_parent = parent;
		_list = new();
	}

	/// <summary>
	/// OnAddNodeEvent
	/// </summary>
	/// <param name="e"></param>
	protected virtual void OnAddEvent(AddLrNodeEventArgs e)
	{
		AddNodeEvent?.Invoke(this, e);
	}

	/// <summary>
	/// OnInsertNodeEvent
	/// </summary>
	/// <param name="e"></param>
	protected virtual void OnInsertEvent(InsertLrNodeEventArgs e)
	{
		InsertNodeEvent?.Invoke(this, e);
	}

	/// <summary>
	/// OnRemoveNodeEvent
	/// </summary>
	/// <param name="e"></param>
	protected virtual void OnRemoveEvent(RemoveLrNodeEventArgs e)
	{
		RemoveNodeEvent?.Invoke(this, e);
	}

	/// <summary>
	/// OnMoveNodeEvent
	/// </summary>
	/// <param name="e"></param>
	protected virtual void OnMoveEvent(MoveLrNodeEventArgs e)
	{
		MoveNodeEvent?.Invoke(this, e);
	}

	/// <summary>
	/// OnClearNodeEvent
	/// </summary>
	/// <param name="e"></param>
	protected virtual void OnClearEvent(ClearLrNodeEventArgs e)
	{
		ClearNodeEvent?.Invoke(this, e);
	}

	/// <summary>
	/// add node
	/// </summary>
	/// <param name="item"></param>
	public void Add(LrTreeNode treeNode)
	{
		treeNode.SetParent(this.Parent);
		_list.Add(treeNode);
		OnAddEvent(new AddLrNodeEventArgs(_parent, treeNode));
	}

	/// <summary>
	/// insert node
	/// </summary>
	/// <param name="target"></param>
	/// <param name="current"></param>
	/// <param name="before"></param>
	public void Insert(LrTree target, LrTreeNode current, bool before = true)
	{
		current.SetParent(this.Parent);
		_list.Insert(target.Index, current);
		OnInsertEvent(new InsertLrNodeEventArgs(target, current, before));
	}

	/// <summary>
	/// Remove
	/// </summary>
	/// <param name="current"></param>
	public void Remove(LrTreeNode current)
	{
		_list.Remove(current);
		OnRemoveEvent(new RemoveLrNodeEventArgs(current));
	}

	/// <summary>
	/// move node
	/// </summary>
	/// <param name="treeNode"></param>
	public void Move(LrTreeNode target, LrTreeNode current, bool before = true)
	{
		//先删后插
		_list.Remove(current);
		OnMoveEvent(new MoveLrNodeEventArgs(target, current, before));
	}

	/// <summary>
	/// Clear
	/// </summary>
	public void Clear()
	{
		_list.Clear();
		OnClearEvent(new ClearLrNodeEventArgs(Parent));
	}

	/// <summary>
	/// 是否包含节点
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public bool Contains(LrTreeNode item) => _list.Any(x => x.Id == item.Id);

	/// <summary>
	/// CopyTo
	/// </summary>
	/// <param name="array"></param>
	/// <param name="arrayIndex"></param>
	public void CopyTo(LrTreeNode[] array, int arrayIndex)
	{
		_list.CopyTo(array, arrayIndex);
	}

	public IEnumerator<LrTreeNode> GetEnumerator() => _list.GetEnumerator();

	public int IndexOf(LrTreeNode item) => _list.IndexOf(item);

	public void Insert(int index, LrTreeNode item)
	{
		throw new NotImplementedException();
	}

	public void RemoveAt(int index)
	{
		throw new NotImplementedException();
	}

	bool ICollection<LrTreeNode>.Remove(LrTreeNode item)
	{
		throw new NotImplementedException();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		throw new NotImplementedException();
	}
}

/// <summary>
/// 新增节点事件参数
/// </summary>
public class AddLrNodeEventArgs : EventArgs
{
	/// <summary>
	/// Parent
	/// </summary>
	public LrTreeNode Parent { get; }

	/// <summary>
	/// Current
	/// </summary>
	public LrTreeNode Current { get; }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="parent"></param>
	/// <param name="current"></param>
	public AddLrNodeEventArgs(LrTreeNode parent, LrTreeNode current)
	{
		Parent = parent;
		Current = current;
	}
}

/// <summary>
/// 插入节点事件参数
/// </summary>
public class InsertLrNodeEventArgs : EventArgs
{
	/// <summary>
	/// Target
	/// </summary>
	public LrTreeNode? Target { get; }

	/// <summary>
	/// 当前节点
	/// </summary>
	public LrTreeNode Current { get; }

	/// <summary>
	/// 插入目标前or后
	/// </summary>
	public bool Before { get; } = true;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="target"></param>
	/// <param name="current"></param>
	/// <param name="before"></param>
	public InsertLrNodeEventArgs(LrTreeNode? target, LrTreeNode current, bool before = true)
	{
		Target = target;
		Current = current;
		Before = before;
	}
}

/// <summary>
/// 移除节点事件参数
/// </summary>
public class RemoveLrNodeEventArgs : EventArgs
{
	/// <summary>
	/// 当前节点
	/// </summary>
	public LrTreeNode Current { get; }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="current"></param>
	public RemoveLrNodeEventArgs(LrTreeNode current)
	{
		Current = current;
	}
}

/// <summary>
/// 移动节点事件参数
/// </summary>
public class MoveLrNodeEventArgs : EventArgs
{
	/// <summary>
	/// 目标节点
	/// </summary>
	public LrTreeNode Target { get; }

	/// <summary>
	/// 当前节点
	/// </summary>
	public LrTreeNode Current { get; }

	/// <summary>
	/// 是否目标前
	/// </summary>
	public bool Before { get; }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="target">目标</param>
	/// <param name="current">当前节点</param>
	public MoveLrNodeEventArgs(LrTreeNode target, LrTreeNode current, bool before = true)
	{
		Target = target;
		Current = current;
		Before = before;
	}
}

/// <summary>
/// 清空节点事件参数
/// </summary>
public class ClearLrNodeEventArgs : EventArgs
{
	/// <summary>
	/// 当前节点
	/// </summary>
	public LrTreeNode Current { get; }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="current">当前节点</param>
	public ClearLrNodeEventArgs(LrTreeNode current)
	{
		Current = current;
	}
}

//================================================================================================

/// <summary>
/// 树
/// </summary>
public class Tree : TreeNode
{
	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="id"></param>
	/// <param name="name"></param>
	public Tree(long id, string name) : base(id, name)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="stores"></param>
	public Tree(List<TreeStore> stores) : this(0, "")
	{
		var store = stores.FirstOrDefault(x => x.ParentId == 0);
		if (store is not null)
		{
			TreeNode root = new(store);
			SetNodes(root, stores);
		}
	}

	/// <summary>
	/// 设置子节点
	/// </summary>
	/// <param name="parent"></param>
	/// <param name="stores"></param>
	private void SetNodes(TreeNode parent, List<TreeStore> stores)
	{
		stores.Where(x => x.ParentId == parent.Id).ForEach(x =>
		{
			TreeNode treeNode = new(x);
			parent.Nodes.Add(treeNode);
			SetNodes(treeNode, stores);
		});
	}
}

/// <summary>
/// ITreeNode
/// </summary>
//public interface ITreeNode
//{
	///// <summary>
	///// Index
	///// </summary>
	//internal int Index { get => _parent?.Nodes.IndexOf(this) ?? 0; }

	///// <summary>
	///// Id
	///// </summary>
	//public long Id { get; set; }

	///// <summary>
	///// Name
	///// </summary>
	//public string? Name { get; set; }

	///// <summary>
	///// ParentId
	///// </summary>
	//public long ParentId { get => _parent?.Id ?? 0; }

	///// <summary>
	///// IsLeaf
	///// </summary>
	//public bool IsLeaf { get => Nodes.Count == 0; }

	///// <summary>
	///// Depth
	///// </summary>
	//public int Depth { get => Parent?.Depth + 1 ?? 0; }

	///// <summary>
	///// Code
	///// </summary>
	//public string? Code { get; set; }

	///// <summary>
	///// QuickCode
	///// </summary>
	//public string? QuickCode { get; set; }

	///// <summary>
	///// Value
	///// </summary>
	//public string? Value { get; set; }

	///// <summary>
	///// 别名
	///// </summary>
	//public string? Alias { get; set; }

	///// <summary>
	///// Description
	///// </summary>
	//public string? Description { get; set; }

	///// <summary>
	///// Style
	///// </summary>
	//public string? Style { get; set; }

	///// <summary>
	///// SelectedStyle
	///// </summary>
	//public string? SelectedStyle { get; set; }

	///// <summary>
	///// Icon
	///// </summary>
	//public string? Icon { get; set; }

	///// <summary>
	///// SelectedIcon
	///// </summary>
	//public string? SelectedIcon { get; set; }

	///// <summary>
	///// Data
	///// </summary>
	//public string? Data { get; set; }

	///// <summary>
	///// Action
	///// </summary>
	//public string? Action { get; set; }

	///// <summary>
	///// Ext
	///// </summary>
	//public string? Ext { get; set; }

	///// <summary>
	///// XppId
	///// </summary>
	//public long XppId { get; set; }

	///// <summary>
	///// TenantId
	///// </summary>
	//public long TenantId { get; set; }

	///// <summary>
	///// Status
	///// </summary>
	//public Common.Enums.Status Status { get; set; } = Common.Enums.Status.Disable;
//}

public class TreeNode
{
	/// <summary>
	/// 父节点
	/// </summary>
	internal TreeNode? _parent;

	/// <summary>
	/// Index
	/// </summary>
	internal int Index { get => _parent?.Nodes.IndexOf(this) ?? 0; }

	/// <summary>
	/// Id
	/// </summary>
	public long Id { get; set; }

	/// <summary>
	/// Name
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// ParentId
	/// </summary>
	public long ParentId { get => _parent?.Id ?? 0; }

	/// <summary>
	/// IsLeaf
	/// </summary>
	public bool IsLeaf { get => Nodes.Count == 0; }

	/// <summary>
	/// Depth
	/// </summary>
	public int Depth { get => Parent?.Depth + 1 ?? 0; }

	/// <summary>
	/// Code
	/// </summary>
	public string? Code { get; set; }

	/// <summary>
	/// QuickCode
	/// </summary>
	public string? QuickCode { get; set; }

	/// <summary>
	/// Value
	/// </summary>
	public string? Value { get; set; }

	/// <summary>
	/// 别名
	/// </summary>
	public string? Alias { get; set; }

	/// <summary>
	/// Description
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Style
	/// </summary>
	public string? Style { get; set; }

	/// <summary>
	/// SelectedStyle
	/// </summary>
	public string? SelectedStyle { get; set; }

	/// <summary>
	/// Icon
	/// </summary>
	public string? Icon { get; set; }

	/// <summary>
	/// SelectedIcon
	/// </summary>
	public string? SelectedIcon { get; set; }

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
	/// XppId
	/// </summary>
	public long XppId { get; set; }

	/// <summary>
	/// TenantId
	/// </summary>
	public long TenantId { get; set; }

	/// <summary>
	/// Status
	/// </summary>
	public Common.Enums.Status Status { get; set; } = Common.Enums.Status.Disable;

	/// <summary>
	/// Parent
	/// </summary>
	[JsonIgnore]
	public TreeNode? Parent { get => _parent; }

	/// <summary>
	/// 子节点集
	/// </summary>
	public TreeNodeCollection Nodes { get; }

	/// <summary>
	/// 移动节点事件
	/// </summary>
	public event EventHandler<MoveNodeEventArgs>? MoveNodeEvent;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="store"></param>
	public TreeNode(TreeStore store) : this(store.Id, store.Name, store.Action, store.Value, store.Description, store.Alias, store.Code, store.QuickCode, store.Data, store.Style, store.SelectedStyle, store.Icon, store.SelectedIcon, store.Ext, store.XppId, store.TenantId, store.Status)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	public TreeNode() : this(0, "", null)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="id">id</param>
	/// <param name="name">名称</param>
	public TreeNode(long id, string name) : this(id, name, null)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="id">id</param>
	/// <param name="name">名称</param>
	/// <param name="actiion">actiion</param>
	public TreeNode(long id, string name, string? action = null) : this(id, name, action, null)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="id">id</param>
	/// <param name="name">名称</param>
	/// <param name="action">action</param>
	/// <param name="value">描述</param>
	public TreeNode(long id, string name, string? action = null, string? value = null) : this(id, name, action, value, null)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="id"></param>
	/// <param name="name"></param>
	/// <param name="action"></param>
	/// <param name="value"></param>
	/// <param name="description"></param>
	/// <param name="alias"></param>
	/// <param name="code"></param>
	/// <param name="quickCode"></param>
	/// <param name="data"></param>
	/// <param name="style"></param>
	/// <param name="icon"></param>
	/// <param name="selectedIcon"></param>
	/// <param name="ext"></param>
	public TreeNode(long id, string name, string? action = null, string? value = null, string? description = null, string? alias = null, string? code = null, string? quickCode = null, string? data = null, string? style = null, string? selectedStyle = null, string? icon = null, string? selectedIcon = null, string? ext = null, long xppId = 0, long tenantId = 0, Status status = Status.Enable)
	{
		SetNode(id, name, action, value, description, alias, code, quickCode, data, style, selectedStyle, icon, selectedIcon, ext, xppId, tenantId, status);
		Nodes = new TreeNodeCollection(this);
	}

	/// <summary>
	/// 使用Store设置节点
	/// </summary>
	/// <param name="store"></param>
	public void SetNode(TreeStore store)
	{
		SetNode(store.Id, store.Name, store.Action, store.Value, store.Description, store.Alias, store.Code, store.QuickCode, store.Data, store.Style, store.SelectedStyle, store.Icon, store.SelectedIcon, store.Ext, store.XppId, store.TenantId, store.Status);
	}

	/// <summary>
	/// 设置节点
	/// </summary>
	/// <param name="id"></param>
	/// <param name="name"></param>
	/// <param name="action"></param>
	/// <param name="value"></param>
	/// <param name="description"></param>
	/// <param name="alias"></param>
	/// <param name="code"></param>
	/// <param name="quickCode"></param>
	/// <param name="data"></param>
	/// <param name="style"></param>
	/// <param name="selectedStyle"></param>
	/// <param name="icon"></param>
	/// <param name="selectedIcon"></param>
	/// <param name="ext"></param>
	/// <param name="xppId"></param>
	/// <param name="tenantId"></param>
	/// <param name="status"></param>
	public void SetNode(long id, string name, string? action = null, string? value = null, string? description = null, string? alias = null, string? code = null, string? quickCode = null, string? data = null, string? style = null, string? selectedStyle = null, string? icon = null, string? selectedIcon = null, string? ext = null, long xppId = 0, long tenantId = 0, Status status = Status.Enable)
	{
		Id = id;
		Name = name;
		Action = action;
		Value = value;
		Description = description;
		Alias = alias;
		Code = code;
		QuickCode = quickCode;
		Data = data;
		Style = style;
		SelectedStyle = selectedStyle;
		Icon = icon;
		SelectedIcon = selectedIcon;
		Ext = ext;
		XppId = xppId;
		TenantId = tenantId;
		Status = status;
	}


	/// <summary>
	/// OnInsertNodeEvent
	/// </summary>
	/// <param name="e"></param>
	protected virtual void OnMoveEvent(MoveNodeEventArgs e)
	{
		MoveNodeEvent?.Invoke(this, e);
	}

	/// <summary>
	/// 设置父节点
	/// </summary>
	/// <param name="parent"></param>
	internal void SetParent(TreeNode? parent)
	{
		//有个有,剥离出愿集合
		Parent?.Nodes.Remove(this);
		_parent = parent;
	}

	/// <summary>
	/// 获取存储列表
	/// </summary>
	/// <returns></returns>
	public List<TreeNode> GetStores()
	{
		List<TreeNode> stores = new();
		SetChildStores(stores, this);
		return stores;
	}

	/// <summary>
	/// 设置子存储
	/// </summary>
	/// <param name="stores"></param>
	/// <param name="treeNode"></param>
	private void SetChildStores(List<TreeNode> stores, TreeNode treeNode)
	{
		stores.AddRange(treeNode.Nodes);
		foreach (var item in treeNode.Nodes)
		{
			SetChildStores(stores, item);
		}
	}

	public string ToJson()
	{
		JsonSerializerOptions options = new()
		{
			ReferenceHandler = ReferenceHandler.IgnoreCycles,
			WriteIndented = true
		};

		return JsonSerializer.Serialize(this, options);
	}

	/// <summary>
	/// 移动节点
	/// </summary>
	/// <param name="target"></param>
	/// <param name="before"></param>
	public void Move(TreeNode target, bool before = true)
	{
		SetParent(target.Parent);
		int index = before ? target.Index : target.Index + 1;
		//插入新集合
		target!.Parent!.Nodes._stores.Insert(index, this);
		//移除当前集合
		this.Nodes._stores.Remove(this);

		OnMoveEvent(new MoveNodeEventArgs(target, this, before));
	}
}

/// <summary>
/// 树形节点集合
/// </summary>
public class TreeNodeCollection : IList<TreeNode>
{
	/// <summary>
	/// 节点集
	/// </summary>
	internal readonly List<TreeNode> _stores;

	/// <summary>
	/// _parent
	/// </summary>
	private readonly TreeNode _parent;

	/// <summary>
	/// Parent
	/// </summary>
	public TreeNode Parent { get => _parent; }

	/// <summary>
	/// 节点数
	/// </summary>
	public int Count => _stores.Count;

	/// <summary>
	/// IsReadOnly
	/// </summary>
	bool ICollection<TreeNode>.IsReadOnly => false;

	/// <summary>
	/// 索引器
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	TreeNode IList<TreeNode>.this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	/// <summary>
	/// 索引器
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public TreeNode this[int index]
	{
		get
		{
			return _stores[index];
		}
	}

	/// <summary>
	/// 新增节点事件
	/// </summary>
	public event EventHandler<AddNodeEventArgs>? AddNodeEvent;

	/// <summary>
	/// 插入节点事件
	/// </summary>
	public event EventHandler<InsertNodeEventArgs>? InsertNodeEvent;

	/// <summary>
	/// 移除节点事件
	/// </summary>
	public event EventHandler<RemoveNodeEventArgs>? RemoveNodeEvent;

	/// <summary>
	/// 清空节点事件
	/// </summary>
	public event EventHandler<ClearNodeEventArgs>? ClearNodeEvent;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="parent">父节点</param>
	public TreeNodeCollection(TreeNode parent)
	{
		_parent = parent;
		_stores = new();
	}

	/// <summary>
	/// OnAddNodeEvent
	/// </summary>
	/// <param name="e"></param>
	protected virtual void OnAddEvent(AddNodeEventArgs e)
	{
		AddNodeEvent?.Invoke(this, e);
	}

	/// <summary>
	/// OnInsertNodeEvent
	/// </summary>
	/// <param name="e"></param>
	protected virtual void OnInsertEvent(InsertNodeEventArgs e)
	{
		InsertNodeEvent?.Invoke(this, e);
	}

	/// <summary>
	/// OnRemoveNodeEvent
	/// </summary>
	/// <param name="e"></param>
	protected virtual void OnRemoveEvent(RemoveNodeEventArgs e)
	{
		RemoveNodeEvent?.Invoke(this, e);
	}

	/// <summary>
	/// OnClearNodeEvent
	/// </summary>
	/// <param name="e"></param>
	protected virtual void OnClearEvent(ClearNodeEventArgs e)
	{
		ClearNodeEvent?.Invoke(this, e);
	}

	/// <summary>
	/// add node
	/// </summary>
	/// <param name="item"></param>
	public void Add(TreeNode treeNode)
	{
		//已包含则返回
		if (this._stores.Contains(treeNode))
		{
			return;
		}
		treeNode.SetParent(this.Parent);
		_stores.Add(treeNode);
		OnAddEvent(new AddNodeEventArgs(_parent, treeNode));
	}

	/// <summary>
	/// Insert
	/// </summary>
	/// <param name="index"></param>
	/// <param name="item"></param>
	public void Insert(int index, TreeNode item)
	{
		Insert(index, item, true);
	}

	/// <summary>
	/// Insert
	/// </summary>
	/// <param name="index"></param>
	/// <param name="item"></param>
	public void Insert(int index, TreeNode item, bool before = true)
	{
		item.Parent?.Nodes.Remove(item);
		item.SetParent(this.Parent);
		_stores.Insert(index, item);
		OnInsertEvent(new InsertNodeEventArgs(index, item, before));
	}

	/// <summary>
	/// Remove
	/// </summary>
	/// <param name="current"></param>
	public bool Remove(TreeNode current)
	{
		var result = _stores.Remove(current);
		OnRemoveEvent(new RemoveNodeEventArgs(current));
		return result;
	}

	/// <summary>
	/// Clear
	/// </summary>
	public void Clear()
	{
		_stores.Clear();
		OnClearEvent(new ClearNodeEventArgs(Parent));
	}

	/// <summary>
	/// 是否包含节点
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public bool Contains(TreeNode item) => _stores.Any(x => x.Id == item.Id);

	/// <summary>
	/// CopyTo
	/// </summary>
	/// <param name="array"></param>
	/// <param name="arrayIndex"></param>
	public void CopyTo(TreeNode[] array, int arrayIndex)
	{
		_stores.CopyTo(array, arrayIndex);
	}

	/// <summary>
	/// IndexOf
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public int IndexOf(TreeNode item) => _stores.IndexOf(item);

	/// <summary>
	/// 按索引移除
	/// </summary>
	/// <param name="index"></param>
	public void RemoveAt(int index)
	{
		TreeNode target = _stores[index];
		Remove(target);
	}

	/// <summary>
	/// GetEnumerator
	/// </summary>
	/// <returns></returns>
	public IEnumerator<TreeNode> GetEnumerator() => _stores.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => _stores.GetEnumerator();
}

/// <summary>
/// 树存储
/// </summary>
public class TreeStore
{
	/// <summary>
	/// Id
	/// </summary>
	public long Id { get; set; }

	/// <summary>
	/// ParentId
	/// </summary>
	public long ParentId { get; set; }

	/// <summary>
	/// Index
	/// </summary>
	public int Index { get; set; }

	/// <summary>
	/// IsLeaf
	/// </summary>
	public bool IsLeaf { get; set; }

	/// <summary>
	/// Depth
	/// </summary>
	public int Depth { get; set; }

	/// <summary>
	/// Code
	/// </summary>
	public string? Code { get; set; }

	/// <summary>
	/// QuickCode
	/// </summary>
	public string? QuickCode { get; set; }

	/// <summary>
	/// Value
	/// </summary>
	public string? Value { get; set; }

	/// <summary>
	/// Name
	/// </summary>
	public string Name { get; set; } = String.Empty;

	/// <summary>
	/// 别名
	/// </summary>
	public string? Alias { get; set; }

	/// <summary>
	/// Description
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Style
	/// </summary>
	public string? Style { get; set; }

	/// <summary>
	/// SelectedStyle
	/// </summary>
	public string? SelectedStyle { get; set; }

	/// <summary>
	/// Icon
	/// </summary>
	public string? Icon { get; set; }

	/// <summary>
	/// SelectedIcon
	/// </summary>
	public string? SelectedIcon { get; set; }

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
	/// XppId
	/// </summary>
	public long XppId { get; set; }

	/// <summary>
	/// TenantId
	/// </summary>
	public long TenantId { get; set; }

	/// <summary>
	/// Status
	/// </summary>
	public Status Status { get; set; } = Status.Enable;
}


/// <summary>
/// 新增节点事件参数
/// </summary>
public class AddNodeEventArgs : EventArgs
{
	/// <summary>
	/// Parent
	/// </summary>
	public TreeNode Parent { get; }

	/// <summary>
	/// Current
	/// </summary>
	public TreeNode Current { get; }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="parent"></param>
	/// <param name="current"></param>
	public AddNodeEventArgs(TreeNode parent, TreeNode current)
	{
		Parent = parent;
		Current = current;
	}
}

/// <summary>
/// 插入节点事件参数
/// </summary>
public class InsertNodeEventArgs : EventArgs
{
	/// <summary>
	/// Parent
	/// </summary>
	public int Index { get; }

	/// <summary>
	/// 当前节点
	/// </summary>
	public TreeNode Current { get; }

	/// <summary>
	/// 插入目标前or后
	/// </summary>
	public bool Before { get; } = true;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="index"></param>
	/// <param name="current"></param>
	/// <param name="before"></param>
	public InsertNodeEventArgs(int index, TreeNode current, bool before = true)
	{
		Index = index;
		Current = current;
		Before = before;
	}
}

/// <summary>
/// 移除节点事件参数
/// </summary>
public class RemoveNodeEventArgs : EventArgs
{
	/// <summary>
	/// 当前节点
	/// </summary>
	public TreeNode Current { get; }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="current"></param>
	public RemoveNodeEventArgs(TreeNode current)
	{
		Current = current;
	}
}

/// <summary>
/// 移动节点事件参数
/// </summary>
public class MoveNodeEventArgs : EventArgs
{
	/// <summary>
	/// 目标节点
	/// </summary>
	public TreeNode Target { get; }

	/// <summary>
	/// 当前节点
	/// </summary>
	public TreeNode Current { get; }

	/// <summary>
	/// 是否目标前
	/// </summary>
	public bool Before { get; }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="target">目标</param>
	/// <param name="current">当前节点</param>
	public MoveNodeEventArgs(TreeNode target, TreeNode current, bool before = true)
	{
		Target = target;
		Current = current;
		Before = before;
	}
}

/// <summary>
/// 清空节点事件参数
/// </summary>
public class ClearNodeEventArgs : EventArgs
{
	/// <summary>
	/// 当前节点
	/// </summary>
	public TreeNode Current { get; }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="current">当前节点</param>
	public ClearNodeEventArgs(TreeNode current)
	{
		Current = current;
	}
}

//实现一个EF接口-->部门、权限

/// <summary>
/// 树存储提高者接口
/// </summary>
public interface ITreeStoreProvider
{
	List<TreeStore> GetStores();

	Task<TreeStore> AddAsync(TreeNode treeNode);

	Task RemoveAsync(TreeNode treeNode);

	Task InsertAsync(TreeNode target, TreeNode source, bool before = true);

	Task UpdateAsync(TreeNode treeNode);
}