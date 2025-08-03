namespace Lib;

public class AppCLI
{
    bool state;
    Assessment[]? objects;
    const int optionsCount = 6;

    public AppCLI()
    {
        state = true;
    }
    public void PrintMenu()
    {
        Console.WriteLine(
            "Choose one of the following requests(enter number of the option):\n"
            + "0. Exit\n"
            + "1. Random Init of array\n"
            + "2. Manual Init of array\n"
            + "3. Show array elements(Virtual)\n"
            + "4. Show array elements(Non-virtual)\n"
            + "5. Print Menu"
        );
    }
    public int ChooseOptions()
    {
        int chosenRequest = -1;
        while (chosenRequest < 0)
        {
            chosenRequest = Input.InputMessageIntNoLine("> ");
            if (chosenRequest >= optionsCount)
            {
                Console.WriteLine("There is no option with that number");
                chosenRequest = -1;
            }
        }
        return chosenRequest;
    }
    public void Init()
    {
        PrintMenu();
        while (state)
        {
            int count;
            int chosenRequest = ChooseOptions();
            switch (chosenRequest)
            {
                case 0:
                    state = false;
                    break;
                case 1:
                    count = Input.InputMessageInt("Write down the count of objects to create:");
                    Request.RandomInitObjects(ref objects, count);
                    break;
                case 2:
                    count = Input.InputMessageInt("Write down the count of objects to create:");
                    Request.Init(ref objects, count);
                    break;
                case 3:
                    if (objects == null)
                    {
                        Console.WriteLine("No objects have been initialized");
                        break;
                    }
                    Request.ShowObjectsVirt(objects);
                    break;
                case 4:
                    if (objects == null)
                    {
                        Console.WriteLine("No objects have been initialized");
                        break;
                    }
                    Request.ShowObjects(objects);
                    break;
                case 5:
                    PrintMenu();
                    break;
            }
        }
    }
}
