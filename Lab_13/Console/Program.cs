using EventLib;
using Lib;

class Program
{
    public static void Main(String[] args)
    {
        var tree1 = new NewAssessmentTree<Assessment>();
        var tree2 = new NewAssessmentTree<Assessment>();
        var journal1 = new Journal();
        var journal2 = new Journal();

        tree1.CollectionCountChanged += new NewAssessmentTreeHandler(journal1.CollectionCountChanged);
        tree1.CollectionReferenceChanged += new NewAssessmentTreeHandler(journal1.CollectionReferenceChanged);
        tree2.CollectionCountChanged += new NewAssessmentTreeHandler(journal2.CollectionCountChanged);
        tree2.CollectionReferenceChanged += new NewAssessmentTreeHandler(journal2.CollectionReferenceChanged);

        Assessment[] objects = null;
        Request.RandomInitObjects(ref objects!, 5);

        Console.WriteLine("List of assessments that will be added to the trees");
        foreach (var obj in objects)
        {
            Console.WriteLine(obj);
        }
        tree1.AddRange(objects);
        tree2.AddRange(objects);

        tree1.Remove(objects[3]);
        tree2.Remove(objects[2]);

        Console.WriteLine(journal1 + "\n\n" + journal2);

        Console.WriteLine("Tree 1");
        tree1.ConsolePrintTree();
        Console.WriteLine("Tree 2");
        tree2.ConsolePrintTree();
    }
}
