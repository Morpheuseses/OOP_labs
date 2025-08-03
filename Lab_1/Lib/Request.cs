namespace Lib;

class Request
{
    public static void RandomInitObjects(ref Assessment[]? objects, int count)
    {
        Random rand = new Random();
        objects = new Assessment[count];
        for (int i = 0; i < count; i++)
        {
            switch (rand.Next(4))
            {
                case 0:
                    objects[i] = new Assessment();
                    objects[i].RandomInit();
                    break;
                case 1:
                    objects[i] = new Test();
                    objects[i].RandomInit();
                    break;
                case 2:
                    objects[i] = new Exam();
                    objects[i].RandomInit();
                    break;
                case 3:
                    objects[i] = new FinalExam();
                    objects[i].RandomInit();
                    break;
            }
        }
    }
    public static void ShowObjectsVirt(Assessment[] objects)
    {
        Console.WriteLine("Here your objects:");
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].ShowVirt();
        }
    }
    public static void ShowObjects(Assessment[] objects)
    {
        Console.WriteLine("Here your objects:");
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].Show();
        }
    }
    public static void Init(ref Assessment[]? objects, int count)
    {
        objects = new Assessment[count];
        for (int i = 0; i < count; i++)
        {
            int chosenType = Input.InputMessageInt("Write down the type of Assessment(0 - Assessment,1 - Test, 2 - Exam, 3 - FinalExam): ");
            switch (chosenType)
            {
                case 0:
                    objects[i] = new Assessment();
                    objects[i].Init();
                    break;
                case 1:
                    objects[i] = new Test();
                    objects[i].Init();
                    break;
                case 2:
                    objects[i] = new Exam();
                    objects[i].Init();
                    break;
                case 3:
                    objects[i] = new FinalExam();
                    objects[i].Init();
                    break;
                default:
                    Console.WriteLine("You've written wrong number. Try again");
                    i--;
                    continue;
            }
        }
    }
}
