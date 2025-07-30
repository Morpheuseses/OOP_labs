namespace Lib;

public enum GraduationLevel
{
    Bachelor = 1,
    Master,
    PhD
}
// test, Assessment, exam, final exam
public class Assessment
{
    string title;
    DateTime date;
    int durationSeconds;
    public string Title
    {
        get { return title; }
        set
        {
            if (value.Length > 0)
                this.title = value;
            else
                throw new Exception($"{this.GetType().Name}'s title length should have at least one symbol");
        }
    }
    public DateTime Date
    {
        get { return this.date; }
        set
        {
            if (value > DateTime.Now)
                this.date = value;
            else
                throw new Exception($"{this.GetType().Name}'s date must later than exact moment");
        }
    }
    public int DurationSeconds
    {
        get { return this.durationSeconds; }
        set
        {
            if (value > 0 && value < 21600)
                this.durationSeconds = value;
            else
                throw new Exception($"{this.GetType().Name}'s duration should last more than 0 seconds and less than 6 hours");
        }
    }

    public Assessment()
    {
        this.Title = "None";
        this.Date = DateTime.Now.AddSeconds(1);
        this.DurationSeconds = 1;
    }
    public Assessment(string title, DateTime date, int duration)
    {
        this.Title = title;
        this.Date = date;
        this.DurationSeconds = DurationSeconds;
    }
    public Assessment(Assessment other)
    {
        this.Title = other.Title;
        this.Date = other.Date;
        this.DurationSeconds = other.DurationSeconds;
    }
    public void Init()
    {
        Console.WriteLine($"Manual initialization of {this.GetType().Name}:");
        this.Title = Input.InputMessageString($"Write down the title of the {this.GetType().Name}:");
        bool isInitialized = false;
        while (!isInitialized)
        {
            Console.WriteLine($"Write down day, month and year of {this.GetType().Name}(with numbers):");
            int day = Input.InputMessageInt("day: ");
            int month = Input.InputMessageInt("month: ");
            int year = Input.InputMessageInt("year: ");
            Console.WriteLine($"Write down the time of {this.GetType().Name}(with numbers in 24h format)");
            int hour = Input.InputMessageInt("hour: ");
            int minutes = Input.InputMessageInt("minutes: ");

            try
            {
                this.Date = new DateTime(year: year, month: month, day: day, hour, minutes, 0);
                isInitialized = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Oops, you wrote something wrong! Remember, date must be later that exact moment of time. Try again");
            }
        }
        this.DurationSeconds = Input.InputMessageInt($"Write down a duration of {this.GetType().Name}(must be more than 0 seconds less than 6 hours):");
    }
    public void RandomInit()
    {
        var rand = new Random();

        string[] subjects = { "Math", "OOP", "Physics", "Engineering",
            "Programming", "SystemModeling", "DiscreteMath", "ComputerGraphics" };

        this.Date = DateTime.Now.AddHours(rand.Next(0, 24)).AddMinutes(rand.Next(0, 60)).AddDays(rand.Next(0, 31)).AddMonths(rand.Next(0, 12)).AddYears(rand.Next(0, 3));
        this.DurationSeconds = rand.Next(1, 21600);
        this.Title = subjects[rand.Next(subjects.Length)];

    }
    public void Show()
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine($"Object Type: {this.GetType().Name}");
        Console.WriteLine($"Title: {this.title}");
        Console.WriteLine($"Date: {this.date}");
        Console.WriteLine($"Duration: {this.durationSeconds / 3600}h, {this.durationSeconds / 60 % 60}m, {this.durationSeconds % 60}s");
    }
    public override bool Equals(object? obj)
    {
        if (obj is not Assessment other)
            return false;
        else
            return this.Date == other.Date && this.DurationSeconds == other.DurationSeconds && this.Title == other.Title;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(this.Date, this.Title, this.DurationSeconds);
    }
}

public class Test : Assessment
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
    public new void Init()
    {
        base.Init();
        this.NumberOfQuestions = Input.InputMessageInt($"Write down the number of questions for {this.GetType().Name}");
    }
    public new void RandomInit()
    {
        base.RandomInit();
        var rand = new Random();
        this.NumberOfQuestions = rand.Next(1, 50);
    }
    public new void Show()
    {
        base.Show();
        Console.WriteLine($"Number of questions: {this.numberOfQuestions}");
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), this.NumberOfQuestions);
    }
    public override bool Equals(object? obj)
    {
        if (obj is not Test other)
            return false;
        return base.Equals(obj) && this.NumberOfQuestions == other.NumberOfQuestions;
    }
}

public class Exam : Test
{
    int numberOfWrittenQuestions;
    public int NumberOfWrittenQuestions
    {
        get { return this.numberOfWrittenQuestions; }
        set
        {
            if (value > 0 && value < this.NumberOfQuestions)
                this.numberOfWrittenQuestions = value;
            else
                throw new Exception($"{this.GetType().Name}'s number of written questions should be more than 0 and less than number of all questions");
        }
    }
    public Exam() : base()
    {
        this.NumberOfWrittenQuestions = 1;
    }
    public Exam(string title, DateTime date, int duration, int numberOfQuestions, int numberOfWrittenQuestions) : base(title, date, duration, numberOfQuestions)
    {
        this.NumberOfWrittenQuestions = numberOfWrittenQuestions;
    }
    public Exam(Exam other) : base(other)
    {
        this.NumberOfWrittenQuestions = other.NumberOfWrittenQuestions;
    }
    public new void Init()
    {
        base.Init();
        this.NumberOfWrittenQuestions = Input.InputMessageInt("Write down the number of written questions");
    }
    public new void RandomInit()
    {
        base.RandomInit();
        var rand = new Random();
        this.NumberOfWrittenQuestions = rand.Next(1, this.NumberOfQuestions);
    }
    public new void Show()
    {
        base.Show();
        Console.WriteLine($"Number of written questions: {this.NumberOfWrittenQuestions}");
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), this.NumberOfQuestions);
    }
    public override bool Equals(object? obj)
    {
        if (obj is not Exam other)
            return false;
        return base.Equals(obj) && this.NumberOfWrittenQuestions == other.NumberOfWrittenQuestions;
    }
}

public class FinalExam : Exam
{
    public GraduationLevel GraduationLevel { get; set; }
    public FinalExam() : base()
    {
        this.GraduationLevel = GraduationLevel.Bachelor;
    }
    public FinalExam(string title, DateTime date, int duration, int numberOfQuestions, int numberOfWrittenQuestions, GraduationLevel graduationLevel) : base(title, date, duration, numberOfQuestions, numberOfWrittenQuestions)
    {
        this.GraduationLevel = graduationLevel;
    }
    public FinalExam(FinalExam other) : base(other)
    {
        this.GraduationLevel = other.GraduationLevel;
    }
    public void Init(string title, DateTime date, int duration, int numberOfQuestions, int numberOfWrittenQuestions, GraduationLevel graduationLevel)
    {
        this.GraduationLevel = graduationLevel;
    }
    public new void RandomInit()
    {
        base.RandomInit();
        var rand = new Random();
        var values = GraduationLevel.GetValues(typeof(GraduationLevel));
        this.GraduationLevel = (GraduationLevel)values.GetValue(rand.Next())!;
    }
    public new void Show()
    {
        base.Show();
        Console.WriteLine($"Graduation Level");
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), this.GraduationLevel);
    }
    public override bool Equals(object? obj)
    {
        if (obj is not FinalExam other)
            return false;
        return base.Equals(obj) && this.GraduationLevel == other.GraduationLevel;
    }
}
