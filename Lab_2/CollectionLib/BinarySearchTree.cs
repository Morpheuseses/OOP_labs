using System;
using System.Collections;
using System.Collections.Generic;
using Lib;

namespace CollectionLib;

// TODO:    Make Comparer for elements
//          Should implement Remove() maybe...
public class BinarySearchTree<T> : ICollection<T>, IEnumerable<T>, ICloneable
    where T : IComparable, ICloneable, IInit
{
    public NodeTree<T> RootNode { get; set; }

    public int Count { get; private set; }

    public bool IsReadOnly => false;

    public BinarySearchTree()
    {
        RootNode = null;
    }
    public BinarySearchTree(int capacity)
    {
        RootNode = null;
    }
    public BinarySearchTree(BinarySearchTree<T> btr)
    {
        this.RootNode = CloneNode(btr.RootNode);
        this.Count = btr.Count;
    }
    private NodeTree<T> CloneNode(NodeTree<T> node)
    {
        if (node is null)
            return null;
        return new NodeTree<T>(node)
        {
            Left = CloneNode(node.Left),
            Right = CloneNode(node.Right)
        };
    }
    private void PreOrder(Action<NodeTree<T>> operation)
    {
        var list = new List<NodeTree<T>>();
        PreOrder(RootNode, operation);
    }
    private void PreOrder(NodeTree<T> node, Action<NodeTree<T>> operation)
    {
        if (node is null)
            return;
        operation(node);
        PreOrder(node.Left, operation);
        PreOrder(node.Right, operation);
    }
    public int GetHeight()
    {
        return GetHeight(RootNode);
    }
    private int GetHeight(NodeTree<T> node)
    {
        if (node == null)
            return 0;
        int leftHeight = GetHeight(node.Left);
        int rightHeight = GetHeight(node.Right);

        return Math.Max(leftHeight, rightHeight) + 1;
    }
    private int GetNodeHeight(NodeTree<T> node)
    {
        return node is not null ? node.Height : 0;
    }
    private int Bfactor(NodeTree<T> node)
    {
        var left = node.Left;
        var right = node.Right;
        return GetNodeHeight(right) - GetNodeHeight(left);
    }
    private void SetHeight(ref NodeTree<T> node)
    {
        int hLeft = GetNodeHeight(node);
        int hRight = GetNodeHeight(node);
        //node.Height = (hLeft > hRight ? hLeft : hRight) + 1;
        node.Height = Math.Max(hLeft, hRight) + 1;
    }
    private NodeTree<T> RotateRight(ref NodeTree<T> node)
    {
        NodeTree<T> nodeLeft = node.Left;
        node.Left = nodeLeft.Right;
        nodeLeft.Right = node;
        SetHeight(ref node);
        SetHeight(ref nodeLeft);
        return nodeLeft;
    }
    private NodeTree<T> RotateLeft(ref NodeTree<T> node)
    {
        NodeTree<T> nodeRight = node.Right;
        node.Right = nodeRight.Left;
        nodeRight.Left = node;
        SetHeight(ref node);
        SetHeight(ref nodeRight);
        return nodeRight;
    }
    private NodeTree<T> BalanceTree(ref NodeTree<T> node)
    {
        SetHeight(ref node);
        if (Bfactor(node) == 2)
        {
            if (Bfactor(node.Right) < 0)
            {
                var nodeRight = node.Right;
                node.Right = RotateRight(ref nodeRight);
            }
            return RotateLeft(ref node);
        }
        if (Bfactor(node) == -2)
        {
            if (Bfactor(node.Left) > 0)
            {
                var nodeLeft = node.Left;
                node.Left = RotateLeft(ref nodeLeft);
            }
            return RotateRight(ref node);
        }
        return node;
    }
    public void Add(T data)
    {
        RootNode = Add(RootNode, data);
        Count++;
    }
    public NodeTree<T> Add(NodeTree<T>? node, T data)
    {
        if (node == null)
            return new NodeTree<T>(data);
        int compResult = data.CompareTo(node.Data);
        if (compResult < 0) //  && data.GetType() == node.Data.GetType()
            node.Left = Add(node.Left, data);
        else if (compResult != 0)
            node.Right = Add(node.Right, data);

        return BalanceTree(ref node);
    }
    public void AddRange(T[] objects)
    {
        foreach (T item in objects)
        {
            Add(item);
        }
    }
    public void ConsolePrintTree()
    {
        ConsolePrintTree(RootNode, "", true);
    }
    public string GetConsoleTreeString()
    {
        return GetConsoleTreeString(RootNode, "", true);
    }
    public string GetConsoleTreeString(NodeTree<T> node, string indent, bool isLast)
    {
        if (node is null)
            return "";
        var sb = new System.Text.StringBuilder();
        sb.Append(indent);
        sb.Append(isLast ? "\u2514\u2500\u2500 " : "\u251C\u2500\u2500 ");
        string toPrint = node.Height + " " + node.Data.ToString()
                        .Replace("\n", "").Replace("-", "") + "\n";
        sb.Append(toPrint);
        var children = new List<NodeTree<T>>();
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
    public void ConsolePrintTree(NodeTree<T> node, string indent, bool isLast)
    {
        Console.WriteLine(this.GetConsoleTreeString());
    }
    public bool Remove(T data)
    {
        return false;
    }
    public object ShallowCopy()
    {
        return (BinarySearchTree<T>)this.MemberwiseClone();
    }
    public object Clone()
    {
        return new BinarySearchTree<T>(this);
    }
    public void Clear()
    {
        RootNode = null;
    }
    public override bool Equals(object obj)
    {
        return obj is BinarySearchTree<T> tree &&
               EqualityComparer<NodeTree<T>>.Default.Equals(RootNode, tree.RootNode);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(RootNode);
    }
    public NodeTree<T> FindNode(T data, NodeTree<T> startNode)
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
    private IEnumerable<T>? PreOrder(NodeTree<T>? node)
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
