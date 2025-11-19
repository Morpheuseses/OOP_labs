using System;
using System.Collections.Generic;
using System.Linq;
using Lib;

namespace MethodsLib;
public static class AssessmentQuery
{
    public static Assessment MinBy<T>(IEnumerable<Assessment> items, Func<Assessment, T> selector)
        where T : IComparable<T>
    {
        return items.OrderBy(selector).FirstOrDefault();
    }

    public static Assessment MaxBy<T>(IEnumerable<Assessment> items, Func<Assessment, T> selector)
        where T : IComparable<T>
    {
        return items.OrderByDescending(selector).FirstOrDefault();
    }

    public static IEnumerable<Assessment> TopN<T>(
        IEnumerable<Assessment> items,
        int n,
        Func<Assessment, bool> filter,
        Func<Assessment, T> selector)
        where T : IComparable<T>
    {
        return items
            .Where(filter)
            .OrderBy(selector)
            .Take(n);
    }
}
