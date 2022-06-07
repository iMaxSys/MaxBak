using System;

namespace iMaxSys.Max.Collection;


/// <summary>
/// Tree
/// </summary>
public class Tree : TreeNode
{
    /// <summary>
    /// 根标识
    /// </summary>
    const string TAG_ROOT = "root";

    /// <summary>
    /// 节点集(平面集)
    /// </summary>
    private List<TreeNode>? _nodeSet;

    /// <summary>
    /// 总深度
    /// </summary>
    public int TotalDepth { get; } = 0;

    /// <summary>
    /// 节点集(平面集)
    /// </summary>
    public List<TreeNode>? NodeSet { get => _nodeSet; }

    /// <summary>
    /// 节点存储(平面存储)
    /// </summary>
    //public List<TreeStore>? StoreSet { get; }

    /// <summary>
    /// 构造
    /// </summary>
    public Tree() : this(0, TAG_ROOT)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    public Tree(long id, string name) : base(id, name)
    {
        TreeNode root = new(id, name);
        Stores.Add(root);

        //事件绑定
        Nodes.AddNodeEvent += Nodes_AddNodeEvent;
        Nodes.InsertNodeEvent += Nodes_InsertNodeEvent;
        Nodes.RemoveNodeEvent += Nodes_RemoveNodeEvent;
        Nodes.ClearNodeEvent += Nodes_ClearNodeEvent;
    }

    /// <summary>
    /// Add
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Nodes_AddNodeEvent(object? sender, AddNodeEventArgs e)
    {
        TreeNode? last = Stores.Where(x => x.Parent?.Id == e.Parent.Id).LastOrDefault();
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
            e.Current._lv = e.Parent.Lv;
            e.Current._rv = e.Parent.Lv + 1;
            e.Current._index = e.Parent.Index + 1;
            e.Current._depth = e.Parent.Depth + 1;
            e.Current._previous = e.Parent;
            e.Current._next = e.Parent;
            e.Parent._next = e.Current;
        }

        //加入当前节点
        Stores.Add(e.Current);

        //后续节点+2
        Stores.Where(x => x.Rv > e.Current.Rv && x.Lv > 0).ForEach(x => { x._lv += 2; x._rv += 2; x._index += 1; });

        //刷新根节点
        RefreshRoot();
    }

    /// <summary>
    /// Remove
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Nodes_RemoveNodeEvent(object? sender, RemoveNodeEventArgs e)
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

    private void Nodes_InsertNodeEvent(object? sender, InsertNodeEventArgs e)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 移动节点
    /// </summary>
    /// <param name="target"></param>
    /// <param name="current"></param>
    /// <param name="before"></param>
    public void Insert(TreeNode target, TreeNode current, bool before = true)
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
    public void Move(TreeNode target, TreeNode current, bool before = true)
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
    private void Nodes_ClearNodeEvent(object? sender, ClearNodeEventArgs e)
    {
        Stores.RemoveRange(e.Current.Index, e.Current.Nodes.Count);

        //后续节点-countx2
        Stores.Where(x => x.Lv > e.Current.Rv).ForEach(x => { x._lv -= e.Current.Nodes.Count * 2; x._rv -= e.Current.Nodes.Count * 2; });
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
        this._lv = 0;
        Stores[0]._rv = AllNodesCount * 2 - 1;
        this._rv = Stores[0]._rv;
    }
}

/// <summary>
/// 树形节点
/// </summary>
public class TreeNode
{
    internal int _lv = 0;
    internal int _rv = 1;
    internal int _depth = 0;
    internal int _index = 0;

    internal TreeNode? _parent;
    internal TreeNode? _previous;
    internal TreeNode? _next;

    private List<TreeNode>? _stores;

    /// <summary>
    /// 节点集
    /// </summary>
    protected List<TreeNode> Stores { get => _parent?.Stores ?? (_stores ??= new()); }

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
    public TreeNode? Parent { get => _parent; }

    /// <summary>
    /// Previous
    /// </summary>
    public TreeNode? Previous { get => _previous; }

    /// <summary>
    /// Next
    /// </summary>
    public TreeNode? Next { get => _next; }

    /// <summary>
    /// 子节点集
    /// </summary>
    public TreeNodeCollection Nodes { get; }

    /// <summary>
    /// 子节点数(直接子节点)
    /// </summary>
    public int NodesCount { get => Nodes?.Count ?? 0; }

    /// <summary>
    /// 所有节点
    /// </summary>
    public TreeNodeCollection? AllNodes { get; }

    /// <summary>
    /// 所有子节点数(包括直接和间接)
    /// </summary>
    public int AllNodesCount { get; } = 0;

    /// <summary>
    /// 所有叶节点
    /// </summary>
    public TreeNodeCollection? AllLeaves { get; }

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
    public TreeNode(long id, string name, string? action = null, string? value = null, string? description = null, string? alias = null, string? code = null, string? quickCode = null, string? data = null, string? style = null, string? icon = null, string? selectedIcon = null, string? ext = null)
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

        Nodes = new TreeNodeCollection(this);
    }

    /// <summary>
    /// 设置父节点
    /// </summary>
    /// <param name="parent"></param>
    internal void SetParent(TreeNode parent)
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
}

/// <summary>
/// 树形节点集合
/// </summary>
public class TreeNodeCollection : IList<TreeNode>
{
    /// <summary>
    /// 节点集
    /// </summary>
    private readonly List<TreeNode> _list;

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
    public int Count => _list?.Count ?? 0;

    /// <summary>
    /// IsReadOnly
    /// </summary>
    bool ICollection<TreeNode>.IsReadOnly => false;

    /// <summary>
    /// 索引器
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public TreeNode this[int index]
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
    /// 移动节点事件
    /// </summary>
    public event EventHandler<MoveNodeEventArgs>? MoveNodeEvent;

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
        _list = new();
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
    /// OnMoveNodeEvent
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnMoveEvent(MoveNodeEventArgs e)
    {
        MoveNodeEvent?.Invoke(this, e);
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
        treeNode.SetParent(this.Parent);
        _list.Add(treeNode);
        OnAddEvent(new AddNodeEventArgs(_parent, treeNode));
    }

    /// <summary>
    /// insert node
    /// </summary>
    /// <param name="target"></param>
    /// <param name="current"></param>
    /// <param name="before"></param>
    public void Insert(Tree target, TreeNode current, bool before = true)
    {
        current.SetParent(this.Parent);
        _list.Insert(target.Index, current);
        OnInsertEvent(new InsertNodeEventArgs(target, current, before));
    }

    /// <summary>
    /// Remove
    /// </summary>
    /// <param name="current"></param>
    public void Remove(TreeNode current)
    {
        _list.Remove(current);
        OnRemoveEvent(new RemoveNodeEventArgs(current));
    }

    /// <summary>
    /// move node
    /// </summary>
    /// <param name="treeNode"></param>
    public void Move(TreeNode target, TreeNode current, bool before = true)
    {
        //先删后插
        _list.Remove(current);
        OnMoveEvent(new MoveNodeEventArgs(target, current, before));
    }

    /// <summary>
    /// Clear
    /// </summary>
    public void Clear()
    {
        _list.Clear();
        OnClearEvent(new ClearNodeEventArgs(Parent));
    }

    /// <summary>
    /// 是否包含节点
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Contains(TreeNode item) => _list.Any(x => x.Id == item.Id);

    /// <summary>
    /// CopyTo
    /// </summary>
    /// <param name="array"></param>
    /// <param name="arrayIndex"></param>
    public void CopyTo(TreeNode[] array, int arrayIndex)
    {
        _list.CopyTo(array, arrayIndex);
    }

    public IEnumerator<TreeNode> GetEnumerator() => _list.GetEnumerator();

    public int IndexOf(TreeNode item) => _list.IndexOf(item);

    public void Insert(int index, TreeNode item)
    {
        throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
        throw new NotImplementedException();
    }

    bool ICollection<TreeNode>.Remove(TreeNode item)
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
    /// Target
    /// </summary>
    public TreeNode? Target { get; }

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
    /// <param name="target"></param>
    /// <param name="current"></param>
    /// <param name="before"></param>
    public InsertNodeEventArgs(TreeNode? target, TreeNode current, bool before = true)
    {
        Target = target;
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