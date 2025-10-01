namespace Lib;

public class Student : IInit
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Year { get; set; }
    private static readonly Random rand = new Random();
    public Student() => RandomInit();
    public Student(Student other)
    {
        this.Name = other.Name;
        this.Surname = other.Surname;
        this.Year = other.Year;
    }
    public void Init() => RandomInit();
    public void RandomInit()
    {
        string[] firstNames = { "Mark", "John", "Maria", "Matthew", "Alex", "Carl" };
        string[] surnames = { "Johnson", "Smith", "Holey", "Moley", "Wilson" };

        this.Name = firstNames[rand.Next(firstNames.Length)];
        this.Surname = surnames[rand.Next(surnames.Length)];
        this.Year = rand.Next(1, 9); // Bachelor's + Master's + PhD
    }
    public static List<Student> CreateStudents()
    {
        var students = new HashSet<Student>();
        var length = rand.Next(1, 9);
        for (int i = 0; i < length; i++)
        {
            students.Add(new Student());
        }
        return students.ToList();
    }
    public override string ToString()
    {
        return $"Object Type: {this.GetType().Name} "
                + $"Name: {this.Name} "
                + $"Surname: {this.Surname} "
                + $"Year: {Year} ";
    }
    public override bool Equals(object? obj)
    {
        if (obj is Student other)
            return this.Name == other.Name && this.Surname == other.Surname && this.Year == other.Year;
        return false;
    }
    public override int GetHashCode() => HashCode.Combine(Name, Surname, Year);
}
