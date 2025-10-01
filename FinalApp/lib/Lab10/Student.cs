namespace Lib;

public class Student : IInit
{
    public string Name { get; set; }
    public int Year { get; set; }
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Year);
    }
    public Student() => RandomInit();
    public Student(Student other)
    {
        this.Name = other.Name;
        this.Year = other.Year;
    }
    public void Init() => RandomInit();
    public void RandomInit()
    {
        string[] firstNames = { "Mark", "John", "Maria", "Matthew", "Alex", "Carl" };
        string[] surnames = { "Johnson", "Smith", "Holey", "Moley", "Wilson" };
        var rand = new Random();
        this.Name = firstNames[rand.Next(firstNames.Length)] + " " + surnames[rand.Next(surnames.Length)];
        this.Year = rand.Next(1, 9); // Bachelor's + Master's + PhD
    }
    public override string ToString()
    {
        return "------------------------------------\n"
                + $"Object Type: {this.GetType().Name}\n"
                + $"Student's name: {this.Name}\n"
                + $"Year: {Year}";
    }
}
