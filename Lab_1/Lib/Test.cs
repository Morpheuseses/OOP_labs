namespace Lib;

public class Test : Assessment, IInit, ICloneable, IComparable
{
    int numberOfQuestions;
    public int NumberOfQuestions
    {
        get { return this.numberOfQuestions; }
        set
        {
            if (value > 0)
                this.numberOfQuestions = value;
            else
                throw new Exception($"{this.GetType().Name}'s number of questions should be more than 0");
        }
    }
    public Test() : base()
    {
        this.NumberOfQuestions = 1;
    }
    public Test(string title, DateTime date, int duration, int numberOfquestions)
        : base(title, date, duration)
    {
        this.NumberOfQuestions = numberOfquestions;
    }
    public Test(Test other) : base(other)
    {
        this.NumberOfQuestions = other.NumberOfQuestions;
    }
    protected override string GetFieldsString()
    {
        return base.GetFieldsString()
               + $"Number of questions: {this.NumberOfQuestions}\n";
    }
    public override void Init()
    {
        base.Init();
        this.NumberOfQuestions = Input.InputMessageInt($"Write down the number of questions for {this.GetType().Name}");
    }
    public override void RandomInit()
    {
        base.RandomInit();
        var rand = new Random();
        this.NumberOfQuestions = rand.Next(1, 50);
    }
    public new void Show()
    {
        Console.WriteLine(
                          "------------------------------------\n"
                          + $"Object Type: {this.GetType().Name}\n"
                          + $"Title: {this.Title}\n"
                          + $"Date: {this.Date}\n"
                          + $"Duration: {this.DurationSeconds / 3600}h, {this.DurationSeconds / 60 % 60}m, {this.DurationSeconds % 60}s\n"
                          + $"Number of questions: {this.NumberOfQuestions}"
        );

    }
    public override void ShowVirt()
    {
        Console.WriteLine(GetFieldsString());
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), this.NumberOfQuestions);
    }
    public override bool Equals(object? obj)
    {
        if (obj is Test other)
            return base.Equals(obj) && this.NumberOfQuestions == other.NumberOfQuestions;
        return false;
    }
    //public int CompareTo(object? obj)
    //{
    //    if (obj == null)
    //        return 1;
    //    else
    //    {
    //        base.CompareTo(obj);
    //        var other = (Test)obj;
    //        if (this.NumberOfQuestions > other.NumberOfQuestions)
    //            return 1;
    //        else if (this.NumberOfQuestions < other.NumberOfQuestions)
    //            return -1;
    //        return 0;
    //    }
    //}
    public virtual object ShallowCopy()
    {
        return (Test)this.MemberwiseClone();
    }
    public object Clone()
    {
        var newTest = (Test)this.MemberwiseClone();
        newTest.Students = new List<Student>(this.Students);
        return newTest;
    }
    public override string ToString()
    {
        return this.GetFieldsString();
    }
}
