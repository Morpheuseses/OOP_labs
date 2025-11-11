using System;
using System.Collections;
using System.Collections.Generic;
using Lib;

namespace CollectionLib;

// TODO:    Make Comparer for elements
//          Should implement Capacity (real count < capacity = count*2 or > count)
public class BinarySearchTree<T> : ICollection<T>, IEnumerable<T>, ICloneable
    where T : IComparable, ICloneable, IInit
{
    public TreeNode<T> RootNode { get; set; }
    public int Count { get; private set; }
    public int Length { get => this.Count; }
    public bool IsReadOnly => false;
    public IComparer<T> Comparer { get; private set; }
    public BinarySearchTree()
    {
        RootNode = null;
    }
    //public BinarySearchTree(int capacity)
    //{
    //    RootNode = null;
    //}
    public BinarySearchTree(IComparer<T>? comparer)
    {
        this.Comparer = comparer ?? Comparer<T>.Default;
    }
    public BinarySearchTree(IEnumerable<T> collection)
    {
        RootNode = null;
        AddRange(collection);
    }
    public BinarySearchTree(BinarySearchTree<T> btr) : this(btr.Comparer)
    {
        if (btr.Count != 0)
        {
            this.RootNode = CloneNode(btr.RootNode);
            this.Count = btr.Count;
        }
    }
    protected TreeNode<T> CloneNode(TreeNode<T> node)
    {
        if (node is null)
            return null;
        return new TreeNode<T>(node)
        {
            Left = CloneNode(node.Left),
            Right = CloneNode(node.Right)
        };
    }
    public virtual int GetHeight()
    {
        return GetHeight(RootNode);
    }
    private int GetHeight(TreeNode<T> node)
    {
        if (node == null)
            return 0;
        int leftHeight = GetHeight(node.Left);
        int rightHeight = GetHeight(node.Right);

        return Math.Max(leftHeight, rightHeight) + 1;
    }
    private int GetNodeHeight(TreeNode<T> node)
    {
        return node is not null ? node.Height : 0;
    }
    private int Bfactor(TreeNode<T> node)
    {
        var left = node.Left;
        var right = node.Right;
        return GetNodeHeight(right) - GetNodeHeight(left);
    }
    private void SetHeight(TreeNode<T> node)
    {
        int hLeft = GetNodeHeight(node.Left);
        int hRight = GetNodeHeight(node.Right);
        //node.Height = (hLeft > hRight ? hLeft : hRight) + 1;
        node.Height = Math.Max(hLeft, hRight) + 1;
    }
    protected TreeNode<T> RotateRight(TreeNode<T> node)
    {
        TreeNode<T> nodeLeft = node.Left;
        node.Left = nodeLeft.Right;
        nodeLeft.Right = node;
        SetHeight(node);
        SetHeight(nodeLeft);
        return nodeLeft;
    }
    protected TreeNode<T> RotateLeft(TreeNode<T> node)
    {
        TreeNode<T> nodeRight = node.Right;
        node.Right = nodeRight.Left;
        nodeRight.Left = node;
        SetHeight(node);
        SetHeight(nodeRight);
        return nodeRight;
    }
    protected TreeNode<T> BalanceTree(TreeNode<T> node)
    {
        SetHeight(node);
        if (Bfactor(node) == 2)
        {
            if (Bfactor(node.Right) < 0)
            {
                node.Right = RotateRight(node.Right);
            }
            return RotateLeft(node);
        }
        if (Bfactor(node) == -2)
        {
            if (Bfactor(node.Left) > 0)
            {
                node.Left = RotateLeft(node.Left);
            }
            return RotateRight(node);
        }
        return node;
    }
    public virtual void Add(T data)
    {
        RootNode = Add(RootNode, data);
        Count++;
    }
    public TreeNode<T> Add(TreeNode<T>? node, T data)
    {
        if (node == null)
            return new TreeNode<T>(data);
        int compResult = data.CompareTo(node.Data);
        if (compResult < 0) //  && data.GetType() == node.Data.GetType()
            node.Left = Add(node.Left, data);
        else if (compResult != 0)
            node.Right = Add(node.Right, data);

        return BalanceTree(node);
    }
    public virtual void AddRange(T[] objects)
    {
        foreach (T item in objects)
        {
            Add(item);
        }
    }
    public virtual void AddRange(IEnumerable<T> objects)
    {
        foreach (var item in objects)
        {
            Add(item);
        }
    }
    public virtual void ConsolePrintTree()
    {
        ConsolePrintTree(RootNode, "", true);
    }
    public string GetConsoleTreeString()
    {
        return GetConsoleTreeString(RootNode, "", true);
    }
    public string GetConsoleTreeString(TreeNode<T> node, string indent, bool isLast)
    {
        if (node is null)
            return "";
        var sb = new System.Text.StringBuilder();
        sb.Append(indent);
        sb.Append(isLast ? "\u2514\u2500\u2500 " : "\u251C\u2500\u2500 ");
        string toPrint = " " + node.Data.ToString()
                        .Replace("\n", "|").Replace("-", "") + "\n";
        sb.Append(toPrint);
        var children = new List<TreeNode<T>>();
        if (node.Left != null)
            children.Add(node.Left);
        if (node.Right != null)
            children.Add(node.Right);
        for (int i = 0; i < children.Count; i++)
            sb.Append(
                GetConsoleTreeString(
                    children[i],
                    indent + (isLast ? "    " : "\u2502   "),
                    i == children.Count - 1)
            );
        return sb.ToString();
    }
    public void ConsolePrintTree(TreeNode<T> node, string indent, bool isLast)
    {
        Console.WriteLine(this.GetConsoleTreeString());
    }
    public virtual bool Remove(T data)
    {
        RootNode = Remove(data, RootNode);
        if (this.Contains(data))
            return false;
        Count--;
        return true;
    }
    private TreeNode<T> Remove(T data, TreeNode<T> node)
    {
        if (node is null)
            return null;
        var comparison = node.Data.CompareTo(data);
        if (comparison > 0)
            node.Left = Remove(data, node.Left);
        else if (comparison < 0)
            node.Right = Remove(data, node.Right);
        else
        {
            TreeNode<T> left = node.Left;
            TreeNode<T> right = node.Right;
            if (right is null)
                return left;
            TreeNode<T> min = FindMin(right);
            min.Right = RemoveMin(right);
            min.Left = left;
            return BalanceTree(min);
        }
        return BalanceTree(node);
    }
    private TreeNode<T> FindMin(TreeNode<T> node)
    {
        return node.Left is not null ? FindMin(node.Left) : node;
    }
    private TreeNode<T> RemoveMin(TreeNode<T> node)
    {
        if (node.Left is null)
            return node.Right;
        node.Left = RemoveMin(node.Left);
        return BalanceTree(node);
    }
    public object ShallowCopy()
    {
        return (BinarySearchTree<T>)this.MemberwiseClone();
    }
    public object Clone()
    {
        return new BinarySearchTree<T>(this);
    }
    public virtual void Clear()
    {
        RootNode = null;
    }
    public override bool Equals(object? obj)
    {
        if (obj is not BinarySearchTree<T> other)
            return false;
        return Equals(other);
    }
    public bool Equals(BinarySearchTree<T>? other)
    {
        if (other is null)
            return false;
        var enumerator = GetEnumerator();
        var otherEnumerator = other.GetEnumerator();
        while (enumerator.MoveNext() && otherEnumerator.MoveNext())
            if (!enumerator.Current.Equals(otherEnumerator.Current))
                return false;
        return true;
    }
    public override int GetHashCode()
    {
        var hashCode = 0;
        foreach (var item in this)
            hashCode += item.GetHashCode();
        return hashCode;
    }
    public TreeNode<T> FindNode(T data, TreeNode<T> startNode)
    {
        if (startNode is null)
            return null;
        int result = data.CompareTo(startNode.Data);
        if (result == 0)
            return startNode;
        else if (result < 0)
            return FindNode(data, startNode.Left);
        else
            return FindNode(data, startNode.Right);
    }
    public bool Contains(T item)
    {
        return FindNode(item, RootNode) is not null;
    }
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));
        if (arrayIndex < 0 || arrayIndex >= array.Length)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        if (array.Length - arrayIndex < Count)
            throw new ArgumentException("There is no enough space in target array!");
        foreach (var value in this)
            array[arrayIndex++] = value;
    }
    private IEnumerable<T>? PreOrder(TreeNode<T>? node)
    {
        if (node != null)
        {
            yield return node.Data;
            foreach (var item in PreOrder(node.Left))
            {
                yield return item;
            }
            foreach (var item in PreOrder(node.Right))
            {
                yield return item;
            }
        }
    }
    public IEnumerator<T> GetEnumerator()
    {
        return PreOrder(RootNode).GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public override string? ToString()
    {
        if (Count == 0)
            return "None";
        else
            return GetConsoleTreeString();
    }
}
