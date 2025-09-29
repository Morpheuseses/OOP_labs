using ExtensionMethodsLib;
using System.Collections.Generic;
using Lib;

namespace Program;
class Program
{
    private static readonly Random rand = new Random();
    public static void Main(String[] args)
    {
        int dictLen = 2; // subject's quantity
        int stackLen = 4; // semester's quantity
        var gradeBook = new Stack<Dictionary<FinalExam, int>>();
        FillGradeBook(gradeBook, dictLen, stackLen);
        Query1(gradeBook);
    }
    static Dictionary<FinalExam, int> FillSemester(int dictLen)
    {
        var semester = new Dictionary<FinalExam, int>();
        for (int i = 0; i < dictLen; i++)
        {
            var finalExam = new FinalExam();
            finalExam.RandomInit();
            semester.Add(finalExam, rand.Next(3, 6));
        }
        return semester;
    }
    static void FillGradeBook(Stack<Dictionary<FinalExam, int>> gradeBook, int dictLen, int stackLen)
    {
        for (int i = 0; i < stackLen; i++)
        {
            gradeBook.Push(FillSemester(dictLen));
        }
    }
    static void Query1(Stack<Dictionary<FinalExam, int>> gradeBook)
    {
        Console.WriteLine("The longest final exam");

        var longestExam = (from dict in gradeBook
                           from fexam in dict.Keys
                           orderby fexam.DurationSeconds descending
                           select fexam).FirstOrDefault();
        if (longestExam is not null)
            Console.WriteLine("By Linq: " + longestExam.Title + " => " + longestExam.DurationSeconds);
        else
            Console.WriteLine("Something is gone wrong or the gradeBook is empty");
    }
    static void Query2()
    {

    }
}
