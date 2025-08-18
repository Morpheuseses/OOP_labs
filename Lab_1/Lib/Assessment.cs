namespace Lib;

// test, Assessment, exam, final exam
// Assessment <- Test <- Exam <- FinalExam
public class Assessment : IInit, ICloneable, IComparable
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
    public List<Student> Students { get; set; }
    public Assessment()
    {
        this.Title = "None";
        this.Date = DateTime.Now.AddSeconds(1);
        this.DurationSeconds = 1;
        this.Students = CreateStudent();
    }
    public Assessment(string title, DateTime date, int duration)
    {
        this.Title = title;
        this.Date = date;
        this.DurationSeconds = duration;
        this.Students = CreateStudent();
    }
    public Assessment(Assessment other)
    {
        this.Title = other.Title;
        this.Date = other.Date;
        this.DurationSeconds = other.DurationSeconds;
        this.Students = other.Students;
    }
    public List<Student> CreateStudent()
    {
        var rand = new Random();
        var students = new List<Student>();
        var length = rand.Next(1, 4);
        for (int i = 0; i < length; i++)
        {
            students.Add(new Student());
        }
        return students;
    }
    public virtual void Init()
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
    public virtual void RandomInit()
    {
        var rand = new Random();

        string[] subjects = { "Math", "OOP", "Physics", "Engineering",
            "Programming", "SystemModeling", "DiscreteMath", "ComputerGraphics" };

        this.Date = DateTime.Now.AddHours(rand.Next(0, 24)).AddMinutes(rand.Next(0, 60)).AddDays(rand.Next(0, 31)).AddMonths(rand.Next(0, 12)).AddYears(rand.Next(0, 3));
        this.DurationSeconds = rand.Next(1, 21600);
        this.Title = subjects[rand.Next(subjects.Length)];
    }
    protected virtual string GetFieldsString()
    {
        return "------------------------------------\n"
                + $"Object Type: {this.GetType().Name}\n"
                + $"Title: {this.Title}\n"
                + $"Date: {this.Date}\n"
                + $"Duration: {this.durationSeconds / 3600}h, {this.durationSeconds / 60 % 60}m, {this.durationSeconds % 60}s\n";
    }
    public void Show()
    {
        Console.WriteLine(
                        "------------------------------------\n"
                        + $"Object Type: {this.GetType().Name}\n"
                        + $"Title: {this.Title}\n"
                        + $"Date: {this.Date}\n"
                        + $"Duration: {this.durationSeconds / 3600}h, {this.durationSeconds / 60 % 60}m, {this.durationSeconds % 60}s\n"
        );
    }
    public virtual void ShowVirt()
    {
        Console.WriteLine(GetFieldsString());
    }
    public override bool Equals(object? obj)
    {
        if (obj is Assessment other)
            return this.Date == other.Date && this.DurationSeconds == other.DurationSeconds && this.Title == other.Title;
        return false;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(this.Date, this.Title, this.DurationSeconds);
    }
    public int CompareTo(object? obj)
    {
        if (obj == null)
            return 1;
        else
        {
            var other = (Assessment)obj;

            if (String.Compare(this.Title, other.Title) > 0)
                return 1;
            else if (String.Compare(this.Title, other.Title) < 0)
                return -1;
            if (DateTime.Compare(this.Date, other.Date) > 0)
                return 1;
            else if (DateTime.Compare(this.Date, other.Date) < 0)
                return -1;
            if (this.DurationSeconds > other.DurationSeconds)
                return 1;
            else if (this.DurationSeconds < other.DurationSeconds)
                return -1;
            return 0;
        }
    }
    public virtual object ShallowCopy()
    {
        return (Assessment)this.MemberwiseClone();
    }
    public virtual object Clone()
    {
        var newAssessment = (Assessment)this.MemberwiseClone();
        newAssessment.Students = new List<Student>(this.Students);
        return newAssessment;
    }
    public override string ToString()
    {
        return this.GetFieldsString();
    }
}
