namespace CollectionLib;
using Lib;

public class NodeTree<T>
    where T : IComparable, ICloneable, IInit
{
    public T Data { get; set; }
    public NodeTree<T> Left { get; set; }
    public NodeTree<T> Right { get; set; }
    public int Height { get; set; }
    public NodeTree(T data)
    {
        this.Data = data;
        this.Left = null;
        this.Right = null;
        this.Height = 1;
    }
    public NodeTree(NodeTree<T> node)
    {
        this.Data = (T)node.Data.Clone();
        this.Left = node.Left;
        this.Right = node.Right;
        this.Height = node.Height;
    }
    public override string ToString() => Data.ToString();
}
