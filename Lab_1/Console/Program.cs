using Lib;

class Program
{
    public static void Main(string[] args)
    {
        Assessment test = new Assessment();
        //        test.Init();
        //        test.Show();
        test.RandomInit();
        Assessment test1 = new Assessment(test);
        test1.Show();
        test.Show();
        if (test == test1)
            Console.WriteLine("The same");
        else
            Console.WriteLine("There are not the same");
        if (test.Equals(test1))
            Console.WriteLine("The same");
        else
            Console.WriteLine("There are not the same");
    }
}
