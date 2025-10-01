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
                inputValue = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                inputValue = -1;
                continue;
            }
        }
        return inputValue;
    }
    public static int InputMessageIntNoLine(string message)
    {
        Console.Write(message);
        int inputValue = -1;

        while (inputValue == -1)
        {
            try
            {
                inputValue = Convert.ToInt32(Console.ReadLine());
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
            inputValue = Convert.ToString(Console.ReadLine());
        }
        if (inputValue is null)
            inputValue = "";
        return inputValue;
    }

}
