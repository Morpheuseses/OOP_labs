using Lib;

namespace CollectionLib;

public class TreeNode<T>
    where T : IComparable, ICloneable, IInit
{
    public T Data { get; set; }
    public TreeNode<T> Left { get; set; }
    public TreeNode<T> Right { get; set; }
    public int Height { get; set; }
    public TreeNode(T data)
    {
        this.Data = data;
        this.Left = null;
        this.Right = null;
        this.Height = 1;
    }
    public TreeNode(TreeNode<T> node)
    {
        this.Data = (T)node.Data.Clone();
        this.Left = node.Left;
        this.Right = node.Right;
        this.Height = node.Height;
    }
    public override string ToString() => Data.ToString();
}
