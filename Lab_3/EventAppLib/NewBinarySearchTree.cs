using CollectionLib;
using Lib;

namespace EventAppLib;

public class NewBinarySearchTree<T> : BinarySearchTree<T>
    where T : IComparable, ICloneable, IInit
{
    public NewBinarySearchTree() : base()
    {

    }
    public NewBinarySearchTree(int capacity) : base(capacity)
    {

    }
    public NewBinarySearchTree(BinarySearchTree<T> btr) : base(btr)
    {

    }
}
