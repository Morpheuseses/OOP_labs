namespace Lib;
using System.Collections;

public class SortAssessmentByField : IComparer
{
    public int Compare(object? obj1, object? obj2)
    {
        if (obj1 is null || obj2 is null)
            return 0;
        return Compare(obj1, obj2, "title");
    }
    public int Compare(object? obj1, object? obj2, string sortField)
    {
        if (obj1 is null || obj2 is null)
            return 0;

        Assessment assessment1 = (Assessment)obj1;
        Assessment assessment2 = (Assessment)obj2;

        sortField = sortField.ToLower();
        switch (sortField)
        {
            case "durationseconds":
                if (assessment1.DurationSeconds > assessment2.DurationSeconds)
                    return 1;
                else if (assessment1.DurationSeconds < assessment2.DurationSeconds)
                    return -1;
                break;
            case "title":
                if (String.Compare(assessment1.Title, assessment2.Title) > 0)
                    return 1;
                else if (String.Compare(assessment1.Title, assessment2.Title) < 0)
                    return -1;
                break;
            case "datetime":
                if (assessment1.Date > assessment2.Date)
                    return 1;
                else if (assessment1.Date < assessment2.Date)
                    return -1;
                break;
            default:
                throw new Exception("Wrong sort field: There is no field with this name");
        }
        return 0;
    }
}
