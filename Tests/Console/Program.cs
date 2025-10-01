using System.Collections.Generic;

class Program
{
    public static void Main(String[] args)
    {
        var dict = new Dictionary<string, int?>();
        dict.Add("hoho", 8);
        Console.WriteLine("hehe{0}", dict["hoho"]);
    }
}
