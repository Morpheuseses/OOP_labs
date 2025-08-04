namespace Lib;

public class AppCLI
{
    bool state;
    Assessment[]? objects;

    public AppCLI()
    {
        state = true;
    }
    public void PrintMenu()
    {
        Console.WriteLine(
            "Choose one of the following requests(enter number of the option):\n"
            + "0. Exit\n"
            + "1. Print Menu\n"
            + "2. Random Init of array\n"
            + "3. Manual Init of array\n"
            + "4. Show array elements(Virtual)\n"
            + "5. Show array elements(Non-virtual)\n"
            + "6. Show average duration in seconds\n"
        );
    }
    public int ChooseOptions()
    {
        return Input.InputMessageIntNoLine("> ");
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
                    PrintMenu();
                    break;
                case 2:
                    count = Input.InputMessageInt("Write down the count of objects to create:");
                    Request.RandomInitObjects(ref objects, count);
                    break;
                case 3:
                    count = Input.InputMessageInt("Write down the count of objects to create:");
                    Request.Init(ref objects, count);
                    break;
                case 4:
                    if (objects == null)
                    {
                        Console.WriteLine("No objects have been initialized");
                        break;
                    }
                    Request.ShowObjectsVirt(objects);
                    break;
                case 5:
                    if (objects == null)
                    {
                        Console.WriteLine("No objects have been initialized");
                        break;
                    }
                    Request.ShowObjects(objects);
                    break;
                case 6:
                    Request.AverageAssessmentDuration(objects);
                    break;
                default:
                    Console.WriteLine("There is no option with this number");
                    break;
            }
        }
    }
}
