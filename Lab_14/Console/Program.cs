using ExtensionMethodsLib;
using System.Collections.Generic;
using CollectionLib;
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
        var students = Student.CreateStudents();
        FillGradeBook(gradeBook, dictLen, stackLen, students);
        Query1(gradeBook);
        Query2(gradeBook);
        Query3(gradeBook);
        Query4(gradeBook, students);


        int count = 10;
        Assessment[]? tests = null;
        Request.RandomInitObjects(ref tests, count);
        BinarySearchTree<Assessment> tree = new BinarySearchTree<Assessment>(tests);

        ExtMethod1(tree);
        ExtMethod2(tree);
        ExtMethod3(tree);
    }

    static Dictionary<FinalExam, int> FillSemester(int dictLen, IEnumerable<Student> students)
    {
        var semester = new Dictionary<FinalExam, int>();
        for (int i = 0; i < dictLen; i++)
        {
            var finalExam = new FinalExam();
            finalExam.RandomInit();
            finalExam.CreateMarks(students);
            semester.Add(finalExam, rand.Next(3, 6));
        }
        return semester;
    }
    static void FillGradeBook(Stack<Dictionary<FinalExam, int>> gradeBook, int dictLen, int stackLen, IEnumerable<Student> students)
    {
        for (int i = 0; i < stackLen; i++)
        {
            gradeBook.Push(FillSemester(dictLen, students));
        }
    }
    static void Query1(Stack<Dictionary<FinalExam, int>> gradeBook)
    {
        Console.WriteLine("The longest final exam:");
        var maxDuration = (from dict in gradeBook
                           from fexam in dict.Keys
                           select fexam.DurationSeconds).Max();
        var longestExam = (from dict in gradeBook
                           from fexam in dict.Keys
                           where fexam.DurationSeconds == maxDuration
                           select fexam).FirstOrDefault();
        Console.WriteLine("By Linq: " + longestExam.Title + " => " + longestExam.DurationSeconds);

        maxDuration = gradeBook
                        .SelectMany(dict => dict.Keys)
                        .Max(fexam => fexam.DurationSeconds);
        longestExam = gradeBook
                        .SelectMany(dict => dict.Keys)
                        .First(fexam => fexam.DurationSeconds == maxDuration);
        Console.WriteLine("\nBy extension methods: " + longestExam.Title + " => " + longestExam.DurationSeconds);
    }
    static void Query2(Stack<Dictionary<FinalExam, int>> gradeBook)
    {
        Console.WriteLine("Select student except who got 2 for exams:");
        var allStudents = (from dict in gradeBook
                           from fexam in dict.Keys
                           from pair in (fexam.Marks ?? Enumerable.Empty<KeyValuePair<string, int?>>())
                           select pair.Key).Distinct();

        var badStudents = (from dict in gradeBook
                           from fexam in dict.Keys
                           from pair in (fexam.Marks ?? Enumerable.Empty<KeyValuePair<string, int?>>())
                           where pair.Value == 2
                           select pair.Key).Distinct();

        var studentsWithout2 = allStudents.Except(badStudents);

        Console.WriteLine("By Linq: ");
        foreach (var student in studentsWithout2)
            Console.WriteLine("Student: " + student.ToString());

        studentsWithout2 = gradeBook
                                .SelectMany(dict => dict.Keys)
                                .SelectMany(fexam => fexam.Marks
                                    ?? Enumerable.Empty<KeyValuePair<string, int?>>())
                                .GroupBy(pair => pair.Key)
                                .Where(g => g.All(p => p.Value != 2))
                                .Select(g => g.Key)
                                .ToList();

        Console.WriteLine("\nBy extension methods: ");
        foreach (var student in studentsWithout2)
            Console.WriteLine("Student: " + student.ToString());

    }
    static void Query3(Stack<Dictionary<FinalExam, int>> gradeBook)
    {
        Console.WriteLine("Group by Title");

        var groupByTitle = from dict in gradeBook
                           from fexam in dict.Keys
                           group fexam by fexam.Title into g
                           select new
                           {
                               Title = g.Key,
                               Count = g.Count()
                           };
        Console.WriteLine("By Linq: ");
        foreach (var item in groupByTitle)
        {
            Console.WriteLine("Title: " + item.Title + "Count: " + item.Count);
        }
        groupByTitle = gradeBook
                        .SelectMany(dict => dict.Keys)
                        .GroupBy(fexam => fexam.Title)
                        .Select(g => new { Title = g.Key, Count = g.Count() });
        Console.WriteLine("\nBy extension methods: ");
        foreach (var item in groupByTitle)
        {
            Console.WriteLine("Title: " + item.Title + "Count: " + item.Count);
        }
    }
    static void Query4(Stack<Dictionary<FinalExam, int>> gradeBook, IEnumerable<Student> students)
    {
        Console.WriteLine("Students and their marks");

        var studentsGrades = from dict in gradeBook
                             from fexam in dict.Keys
                             from mark in fexam.Marks
                             join student in students on mark.Key equals student.ToString()
                             select new
                             {
                                 Student = student.ToString(),
                                 Exam = fexam.Title,
                                 Grade = mark.Value
                             };
        Console.WriteLine("By Linq: ");
        foreach (var item in studentsGrades)
        {
            Console.WriteLine("Student: " + item.Student + "Exam title: " + item.Exam + "Grade: " + item.Grade);
        }

        var studentsGradesExtMethods = gradeBook
                        .SelectMany(dict => dict.Keys)
                        .SelectMany(fexam => fexam.Marks)
                        .Join(
                            students,
                            mark => mark.Key,
                            student => student.ToString(),
                            (mark, student) => new
                            {
                                Student = student.ToString(),
                                Exam = mark.Value,
                                Grade = mark.Value
                            }
                        ).ToList();
        Console.WriteLine("\nBy extension methods: ");
        foreach (var item in studentsGrades)
        {
            Console.WriteLine("Student: " + item.Student + "Exam title: " + item.Exam + "Grade: " + item.Grade);
        }
    }
    static void ExtMethod1(BinarySearchTree<Assessment> tree)
    {
        Console.WriteLine("Sort by the duration of an assessment(in seconds):");

        var assessments = tree.SortAssessments(t => t.DurationSeconds);

        assessments.ConsolePrintTree();
    }
    static void ExtMethod2(BinarySearchTree<Assessment> tree)
    {
        Console.WriteLine("Count of assessments with subject Math: " + tree.CountAssessments(t => t.Title == "Math").ToString());
    }
    static void ExtMethod3(BinarySearchTree<Assessment> tree)
    {
        Console.WriteLine("Show all assesments with duration less than 4 hours: ");

        var durationLessT4hAssessments = tree.SelectAssessments(t => t.DurationSeconds <= 4 * 60 * 60);

        foreach (var item in durationLessT4hAssessments)
            Console.WriteLine(item.ToString());

    }
}
