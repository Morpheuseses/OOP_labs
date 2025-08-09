using CollectionLib;
using Lib;

namespace Console;

class Program
{
    public static void Main(string[] args)
    {
        BinarySearchTree<Assessment> btr = new BinarySearchTree<Assessment>();
        Assessment a = new Assessment();
        a.RandomInit();
        int count = 10;
        Assessment[]? objects = null;
        Request.RandomInitObjects(ref objects, count);
        foreach (var item in objects)
        {
            item.ShowVirt();
        }
        btr.AddRange(objects);

        btr.ConsolePrintTree();

    }
}
