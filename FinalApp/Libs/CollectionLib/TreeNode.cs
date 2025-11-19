using System;
using Lib;

namespace CollectionLib
{
    [Serializable]
    public class TreeNode<T>
        where T : IComparable, ICloneable, IInit
    {
        public T Data { get; set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }

        public TreeNode(T data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        public TreeNode(TreeNode<T> node)
        {
            Data = (T)node.Data.Clone();
            Left = node.Left;
            Right = node.Right;
        }

        public override string ToString() => $"{Data}";
    }
}
