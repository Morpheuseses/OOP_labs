using Lib;

namespace TestsLib10;


public static class TestTests
{
    [Fact]
    public static void TestCreateTestParameters()
    {
        string title = "Mathematics";
        int duration = 10000;
        DateTime date = DateTime.Now.AddSeconds(1);
        int numberOfQuestions = 10;

        var test = new Test(title, date, duration, numberOfQuestions);

        Assert.Equal(title, test.Title);
        Assert.Equal(duration, test.DurationSeconds);
        Assert.Equal(date, test.Date);
    }
    [Fact]
    public static void TestCorrectGetFieldString()
    {
        var test = new Test();
        test.RandomInit();
        string expected = "------------------------------------\n"
                + $"Object Type: {test.GetType().Name}\n"
                + $"Title: {test.Title}\n"
                + $"Date: {test.Date}\n"
                + $"Duration: {test.DurationSeconds / 3600}h, {test.DurationSeconds / 60 % 60}m, {test.DurationSeconds % 60}s\n"
                + $"Number of questions: {test.NumberOfQuestions}";

        var actual = test.ToString();

        Assert.Equal(expected, actual);
    }
    [Fact]
    public static void EqualSholdReturnTrueForEqualObjects()
    {
        var date = DateTime.Now.AddSeconds(1);
        var test1 = new Test("Math", date, 10000, 10);
        var test2 = new Test("Math", date, 10000, 10);

        bool result = test1.Equals(test2);

        Assert.True(result);
    }
    [Fact]
    public static void GetHashCodeIsNotNull()
    {
        var date = DateTime.Now.AddSeconds(1);
        var test = new Test("Math", date, 10000, 10);

        int hash = test.GetHashCode();

        Assert.NotNull(hash);
    }
    [Fact]
    public static void CloneShouldReturnCloneObjectWithSameValues()
    {
        var test = new Test();
        test.RandomInit();

        var clone = (Test)test.Clone();

        Assert.Equal(test.Title, clone.Title);
        Assert.Equal(test.Date, clone.Date);
        Assert.Equal(test.DurationSeconds, clone.DurationSeconds);
        Assert.Equal(test.Students, clone.Students);
    }
    [Fact]
    public static void CopyShouldReturnShallowCopyObjectWithSameValues()
    {
        var test = new Test();
        test.RandomInit();

        var clone = (Test)test.ShallowCopy();

        Assert.Equal(test.Title, clone.Title);
        Assert.Equal(test.Date, clone.Date);
        Assert.Equal(test.DurationSeconds, clone.DurationSeconds);
        Assert.Equal(test.Students, clone.Students);
    }
    [Fact]
    public static void CompareToShouldReturnNegativeNumberWhenNameOfOtherobjectIsGreater()
    {
        var test1 = new Test("AbstractMath", DateTime.Now.AddSeconds(1), 10000, 10);
        var test2 = new Test("DiscreteMath", DateTime.Now.AddSeconds(1), 20000, 20);

        int result = test1.CompareTo(test2);

        Assert.True(result < 0);
    }
}
