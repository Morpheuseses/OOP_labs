namespace Lib;

public class Input
{
    public static int InputMessageInt(string message)
    {
        Console.WriteLine(message);
        int inputValue = -1;

        while (inputValue == -1)
        {
            try
            {
                inputValue = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                inputValue = -1;
                continue;
            }
        }
        return inputValue;
    }
    public static string InputMessageString(string message)
    {
        Console.WriteLine(message);
        string inputValue = "";

        while (inputValue == "")
        {
            inputValue = Console.ReadLine();
        }
        return inputValue;
    }
}
