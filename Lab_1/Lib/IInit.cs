using System.Collections;

namespace Lib;

public interface IInit
{
    void Init();
    void RandomInit();
}

public class SortbyDuration : IComparer
{
    public int Compare(object obj1, object obj2)
    {
        if (obj1 is null || obj2 is null)
            return 1;

        Assessment assessment1 = (Assessment)obj1;
        Assessment assessment2 = (Assessment)obj2;

        if (assessment1.DurationSeconds > assessment2.DurationSeconds)
            return 1;
        else if (assessment1.DurationSeconds < assessment2.DurationSeconds)
            return -1;
        return 0;
    }
}
