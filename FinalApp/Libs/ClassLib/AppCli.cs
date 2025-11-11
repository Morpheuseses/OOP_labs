using System;

namespace Lib;

public class AppCLI
{
    bool isRunning;
    Assessment[]? objects;

    public AppCLI()
    {
        isRunning = true;
    }
    public void PrintMenu()
    {
        Console.WriteLine(
            "Choose one of the following requests(enter number of the option):\n"
            + "0. Exit\n"
            + "1. Print Menu\n"
            + "Part 1\n"
            + "2. Random Init of array\n"
            + "3. Manual Init of array\n"
            + "4. Show array elements(Virtual)\n"
            + "5. Show array elements(Non-virtual)\n"
            + "Part 2\n"
            + "6. (Request)Show average duration in seconds\n"
            + "7. (Request)Show all assessments by subject\n"
            + "8. (Request)Count all assessments by type\n"
            + "9. (Request)Get non-virtual and virtual Show() difference\n"
            + "Part 3\n"
            + "10. Show new hierarchy\n"
            + "11. Sort by IComparable interface\n"
            + "13. Sort by IComparer interface\n"
            + "14. Binary search in array\n"
            + "15. Clear Console\n"
        );
    }
    public int ChooseOptions()
    {
        return Input.InputMessageIntNoLine("> ");
    }
    public void Init()
    {
        PrintMenu();
        while (isRunning)
        {
            int count;
            int chosenRequest = ChooseOptions();
            switch (chosenRequest)
            {
                case 0:
                    isRunning = false;
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
                case 7:
                    string subject = Input.InputMessageString("Write down subject: ");
                    Request.ShowAllAssessmentBySubject(objects, subject);
                    break;
                case 8:
                    Request.CountAllAssessmentByType(objects);
                    break;
                case 9:
                    Request.GetVirtAndNonVirtDiff(objects);
                    break;
                case 10:
                    Console.WriteLine("There some IInit new hierarchy elements:");
                    Request.ShowAssessementIInit();
                    break;
                case 11:
                    Request.ShowAssessmentSortIComparable(objects);
                    break;
                case 13:
                    Request.ShowAssessmentSortIComparer(objects);
                    break;
                case 14:
                    string searchTitle = "DiscreteMath";
                    Console.WriteLine($"Search value in array: {searchTitle}");
                    Assessment? elem = Request.BinarySearchByTitle(objects, searchTitle);
                    if (elem is not null)
                        elem.ShowVirt();
                    else
                        Console.WriteLine("There is no element with Title");
                    break;
                case 15:
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("There is no option with this number");
                    break;
            }
        }
    }
}
