using EventAppLib;
using Lib;
using CollectionLib;

class Program
{
    public static void Main(string[] args)
    {
        NewAssessmentTree tree = new NewAssessmentTree();
        Assessment[] objects = new Assessment[10];
        Request.RandomInitObjects(ref objects, 10);
        tree.AddRange(objects);
        tree.ConsolePrintTree();
    }
}
