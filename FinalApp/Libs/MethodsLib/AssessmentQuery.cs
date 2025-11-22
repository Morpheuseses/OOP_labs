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

    public static IEnumerable<(Type Type, int Count)> CountBySubclass(IEnumerable<Assessment> items)
    {
        var query = from a in items
                    group a by a.GetType() into g
                    select (Type: g.Key, Count: g.Count());
        return query;
    }
}
