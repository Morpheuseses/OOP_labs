using System.Linq;
using System.Collections.Generic;
using Lib;
using CollectionLib;

namespace ExtensionMethodsLib;

public static class ExtensionMethods
{
    public static BinarySearchTree<Assessment> SortAssessments(this BinarySearchTree<Assessment> tree, Func<Assessment, int> sortByFunc)
    {
        var comparer = Comparer<Assessment>.Create(
                        (assessment1, assessment2) => sortByFunc(assessment1).CompareTo(assessment2)
                        );

        var newTree = new BinarySearchTree<Assessment>(comparer);

        foreach (var assessment in tree)
        {
            try
            {
                newTree.Add(assessment);
            }
            catch (Exception e)
            {
                Console.WriteLine("This assessment is already in Assessment");
            }
        }
        return newTree;
    }
    public static IEnumerable<Assessment> SelectAssessments(this BinarySearchTree<Assessment> tree, Func<Assessment, bool> selectRule)
    {
        var selection = tree.Where(selectRule);
        return selection;
    }
    public static int CountAssessments(this BinarySearchTree<Assessment> tree, Func<Assessment, bool> selectRule)
    {
        var selection = tree.Where(selectRule);
        return selection.Count();

    }
}
