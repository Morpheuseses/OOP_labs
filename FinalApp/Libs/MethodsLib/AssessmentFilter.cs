using System;
using System.Collections.Generic;
using System.Linq;
using Lib;

namespace MethodsLib;
public static class AssessmentFilter
{
    public static IEnumerable<Assessment> ByTitleContains(IEnumerable<Assessment> items, string substring)
    {
        return items.Where(a => a.Title.Contains(substring, StringComparison.OrdinalIgnoreCase));
    }

    public static IEnumerable<Test> OnlyTests(IEnumerable<Assessment> items)
    {
        return items.Where(a => a.GetType() == typeof(Test)).Cast<Test>();
    }

    public static IEnumerable<Exam> OnlyExams(IEnumerable<Assessment> items)
    {
        return items.Where(a => a.GetType() == typeof(Exam)).Cast<Exam>();
    }

    public static IEnumerable<FinalExam> OnlyFinals(IEnumerable<Assessment> items)
    {
        return items.Where(a => a.GetType() == typeof(FinalExam)).Cast<FinalExam>();
    }
}
