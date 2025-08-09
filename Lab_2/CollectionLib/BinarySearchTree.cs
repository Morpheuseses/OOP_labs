using System;
using System.Collections;
using System.Collections.Generic;
using Lib;

namespace CollectionLib;

// TODO: Fix add to tree (probably problem with CompareTo implementation, but maybe its only class problem)
public class BinarySearchTree<T> : ICollection<T>, IEnumerable<T>, ICloneable
    where T : IComparable, ICloneable, IInit
{
    public NodeTree<T> RootNode { get; set; }

    public int Count => throw new NotImplementedException();

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
        this.RootNode = btr.RootNode;
    }

    private int GetHeight(NodeTree<T> node)
    {
        return node is not null ? node.Height : 0;
    }
    private int Bfactor(NodeTree<T> node)
    {
        var left = node.Left;
        var right = node.Right;
        return GetHeight(right) - GetHeight(left);
    }
    private void SetHeight(ref NodeTree<T> node)
    {
        var hLeft = GetHeight(node);
        var hRight = GetHeight(node);
        node.Height = (hLeft > hRight ? hLeft : hRight) + 1;
    }

    private NodeTree<T> RotateRight(ref NodeTree<T> node)
    {
        NodeTree<T> nodeLeft = node.Left;
        node.Left = nodeLeft.Right;
        nodeLeft.Right = node.Right;
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
    }
    public NodeTree<T> Add(NodeTree<T>? node, T data)
    {
        if (node == null) return new NodeTree<T>(data);
        if (data.CompareTo(node.Data) < 0)
            node.Left = Add(node.Left, data);
        else
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
    public void ConsolePrintTree(NodeTree<T> node, string indent, bool isLast)
    {
        if (node is null)
            return;
        Console.Write(indent);
        if (isLast)
        {
            Console.Write("\u2514\u2500\u2500 ");
            indent += "    "; // 4 chars
        }
        else
        {
            Console.Write("\u251C\u2500\u2500 ");
            indent += "\u2502   "; // 4 chars
        }
        string toPrint = node.Data.ToString().Replace('\n', ' ');
        toPrint = toPrint.Replace("-", "");
        Console.WriteLine(toPrint);
        var children = new List<NodeTree<T>>();
        if (node.Left != null)
            children.Add(node.Left);
        if (node.Right != null)
            children.Add(node.Right);
        for (int i = 0; i < children.Count; i++)
            ConsolePrintTree(children[i], indent, i == children.Count - 1);
    }
    public void Find()
    {

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
        return null;
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
    public bool Contains(T item)
    {
        throw new NotImplementedException();
    }
    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }
    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string? ToString()
    {
        return base.ToString();
    }
}
