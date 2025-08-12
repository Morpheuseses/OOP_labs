using CollectionLib;
using Lib;

// Maybe should make an cli interface
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
        //        foreach (var item in objects)
        //        {
        //            item.ShowVirt();
        //        }
        //btr.AddRange(objects);
        foreach (var item in objects)
        {
            btr.Add(item);
        }
        btr.ConsolePrintTree();
        btr.Add(a);
        btr.ConsolePrintTree();
        Console.WriteLine(btr.GetHeight());

        var btr2 = new BinarySearchTree<Assessment>(btr);

        btr2.ConsolePrintTree();

        bool isThere = btr2.Contains(a);

        Console.WriteLine(isThere);

        Console.WriteLine(btr.Equals(btr2));

        Test test = new Test()
        {
            Title = "Hehehehhehoeheooehoe"
        };
        btr.Add(test);

        btr.ConsolePrintTree();
        Console.WriteLine(btr.Remove(test));
        btr.ConsolePrintTree();
    }
}
