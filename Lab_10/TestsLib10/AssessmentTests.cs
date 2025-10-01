using Lib;

namespace TestsLib10;

public static class AssessmentTests
{
    [Fact]
    public static void AssessmentCreateAssessmentParameters()
    {
        string title = "Mathematics";
        int duration = 10000;
        DateTime date = DateTime.Now.AddSeconds(1);

        var assessment = new Assessment(title, date, duration);

        Assert.Equal(title, assessment.Title);
        Assert.Equal(duration, assessment.DurationSeconds);
        Assert.Equal(date, assessment.Date);
    }
    [Fact]
    public static void AssessmentCorrectGetFieldString()
    {
        var assessment = new Assessment();
        assessment.RandomInit();
        string expected = "------------------------------------\n"
                + $"Object Type: {assessment.GetType().Name}\n"
                + $"Title: {assessment.Title}\n"
                + $"Date: {assessment.Date}\n"
                + $"Duration: {assessment.DurationSeconds / 3600}h, {assessment.DurationSeconds / 60 % 60}m, {assessment.DurationSeconds % 60}s\n";

        var actual = assessment.ToString();

        Assert.Equal(expected, actual);
    }
    [Fact]
    public static void EqualShouldReturnTrueForEqualObjects()
    {
        var date = DateTime.Now.AddSeconds(1);
        var assessment1 = new Assessment("Math", date, 10000);
        var assessment2 = new Assessment("Math", date, 10000);

        bool result = assessment1.Equals(assessment2);

        Assert.True(result);
    }
    [Fact]
    public static void GetHashCodeIsNotNull()
    {
        var date = DateTime.Now.AddSeconds(1);
        var assessment = new Assessment("Math", date, 10000);

        int hash = assessment.GetHashCode();

        Assert.NotNull(hash);
    }
    [Fact]
    public static void CloneShouldReturnClonedObjectWithSameValues()
    {
        var assessment = new Assessment();
        assessment.RandomInit();

        var clone = (Assessment)assessment.Clone();

        Assert.Equal(assessment.Title, clone.Title);
        Assert.Equal(assessment.Date, clone.Date);
        Assert.Equal(assessment.DurationSeconds, clone.DurationSeconds);
        Assert.Equal(assessment.Marks, clone.Marks);
    }
    [Fact]
    public static void CopyShouldReturnShallowCopyObjectWithSameValues()
    {
        var assessment = new Assessment();
        assessment.RandomInit();

        var clone = (Assessment)assessment.ShallowCopy();

        Assert.Equal(assessment.Title, clone.Title);
        Assert.Equal(assessment.Date, clone.Date);
        Assert.Equal(assessment.DurationSeconds, clone.DurationSeconds);
        Assert.Equal(assessment.Marks, clone.Marks);
    }
    [Fact]
    public static void CompareToShouldReturnNegativeNumberWhenNameOfOtherobjectIsGreater()
    {
        var assessment1 = new Assessment("AbstractMath", DateTime.Now.AddSeconds(1), 10000);
        var assessment2 = new Assessment("DiscreteMath", DateTime.Now.AddSeconds(1), 20000);

        int result = assessment1.CompareTo(assessment2);

        Assert.True(result < 0);
    }
}
