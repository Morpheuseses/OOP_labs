namespace Lib;

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
    public Assessment(Assessment assessment)
    {
        this.Title = assessment.Title;
        this.Date = assessment.Date;
        this.DurationSeconds = assessment.DurationSeconds;
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
                Console.WriteLine("Oops, you wrote somethin wrong! Remember - date must be later that exact moment of time. Try again");
            }
        }
        this.DurationSeconds = Input.InputMessageInt($"Write down a duration of {this.GetType().Name}(must be more than 0 seconds less than 6 hours):");
    }
    public void RandomInit()
    {

    }
    public void Show()
    {
        Console.WriteLine("------------------------------------");
        Console.WriteLine($"Object Type: {this.GetType().Name}");
        Console.WriteLine($"Title: {this.title}");
        Console.WriteLine($"Date: {this.date}");
        Console.WriteLine($"Duration: {this.durationSeconds / 3600}h, {this.durationSeconds / 60 % 60}m, {this.durationSeconds % 60}s");
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
            if (value > 1)
                this.numberOfQuestions = value;
            else
                throw new Exception($"{this.GetType().Name}'s number of questions should last more than 0 seconds");
        }
    }

    public Test() : base()
    {
        this.NumberOfQuestions = 0;
    }
    public Test(string title, DateTime date, int duration, int numberOfquestions)
        : base(title, date, duration)
    {
        this.NumberOfQuestions = numberOfquestions;
    }
}
public class Exam : Test
{
    public Exam() : base()
    {

    }
}
public class FinalExam
{
    public string GraduationLevel;
    public FinalExam() : base()
    {
        this.GraduationLevel = "Bachelor's degree";
    }
}
