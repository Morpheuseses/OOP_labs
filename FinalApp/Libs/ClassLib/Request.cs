using System;

namespace Lib;

public static class Request
{
    private static readonly Random rand = new Random();
    public static void RandomInitObjects(ref Assessment[]? objects, int count)
    {
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
    public static Assessment[] RandomInitUniqueAssessments(int count)
    {
        var usedTitles = new HashSet<string>();
        var result = new Assessment[count];

        for (int i = 0; i < count; i++)
        {
            Assessment assessment;

            
            switch (rand.Next(4))
            {
                case 0:
                    assessment = new Assessment();
                    break;
                case 1:
                    assessment = new Test();
                    break;
                case 2:
                    assessment = new Exam();
                    break;
                case 3:
                    assessment = new FinalExam();
                    break;
                default:
                    assessment = new Assessment();
                    break;
            }

            assessment.RandomInit();
            
            string title = assessment.Title;
            while (usedTitles.Contains(title))
            {
                title = $"{title}_{rand.Next(0,1000)}"; 
            } 

            usedTitles.Add(title);
            assessment.Title = title;

            result[i] = assessment;
        }

        return result;
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
    public static void GetVirtAndNonVirtDiff(Assessment[]? objects)
    {
        if (objects != null)
        {
            int idx = rand.Next(objects.Length);
            Console.WriteLine("Non-virtual Show()(typecast)");
            var fexam = objects[idx] as FinalExam;
            if (fexam != null)
                fexam.Show();
            else
            {
                var exam = objects[idx] as Exam;
                if (exam != null)
                    exam.Show();
                else
                {
                    var test = objects[idx] as Test;
                    if (test != null)
                        test.Show();
                    else
                    {
                        objects[idx].Show();
                    }
                }
            }
            Console.WriteLine("\nNon-virtual Show()(without typecast)");
            objects[idx].Show();
            Console.WriteLine("Virtual Show()");
            objects[idx].ShowVirt();
        }
        else
        {
            Console.WriteLine("There are no elements for request");
        }
    }
    public static int ShowAllAssessmentBySubject(Assessment[]? objects, string subject)
    {
        int count = 0;
        if (objects != null)
        {
            for (int i = 0; i < objects.Length; i++)
                if (objects[i].Title.ToLower().Contains($"{subject.ToLower()}"))
                {
                    count++;
                    objects[i].ShowVirt();
                }
        }
        else
            Console.WriteLine("There are no elements for request");
        Console.WriteLine($"Total: {count}");
        return count;
    }
    public static int[] CountAllAssessmentByType(Assessment[]? objects)
    {
        int[] count = { 0, 0, 0, 0 };
        if (objects != null)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                switch (objects[i].GetType().Name)
                {
                    case "Assessment":
                        count[0]++;
                        break;
                    case "Test":
                        count[1]++;
                        break;
                    case "Exam":
                        count[2]++;
                        break;
                    case "FinalExam":
                        count[3]++;
                        break;
                }
            }
            Console.WriteLine($"Total:\n"
                + $"Assessment {count[0]}\n"
                + $"Test {count[1]}\n"
                + $"Exam {count[2]}\n"
                + $"FinalExam {count[3]}\n"
            );
        }
        else
            Console.WriteLine("There are no elements for request");
        return count;
    }
    public static int AverageAssessmentDuration(Assessment[]? objects)
    {
        if (objects != null)
        {
            int average = 0;
            for (int i = 0; i < objects.Length; i++)
                average += objects[i].DurationSeconds;
            average = average / objects.Length;
            Console.WriteLine($"There is average durations of all assessments: {average}");
            return average / objects.Length;
        }
        Console.WriteLine("There are no elements for request");
        return -1;
    }
    public static void ShowAssessementIInit()
    {
        IInit[] inits = new IInit[]
        {
            new Assessment(),
            new Test(),
            new Exam(),
            new FinalExam(),
            new Student()
        };
        Console.WriteLine("IInit objects initialization: ");
        foreach (var item in inits)
        {
            item.RandomInit();
            Console.WriteLine(item.ToString());
        }
    }
    public static void ShowAssessmentSortIComparable(Assessment[]? objects)
    {
        if (objects is not null)
        {
            Array.Sort(objects);
            Console.WriteLine("Sorted array: ");
            Request.ShowObjectsVirt(objects);
        }
        else
            Console.WriteLine("There are no elements for request");
    }
    public static void ShowAssessmentSortIComparer(Assessment[]? objects)
    {
        if (objects is not null)
        {
            Array.Sort(objects, new SortByDuration());
            Console.WriteLine("Sorted array by duration: ");
            Request.ShowObjectsVirt(objects);
        }
        else
            Console.WriteLine("There are no elements for request");
    }
    public static Assessment? BinarySearchByTitle(Assessment[]? objects, string? target)
    {
        if (objects is not null)
        {
            Array.Sort(objects);
            int low = 0, mid = objects.Length / 2, high = objects.Length - 1;
            while (low <= high)
            {
                mid = low + (high - low) / 2;

                if (objects[mid].Title == target)
                {
                    return objects[mid];
                }
                else if (String.Compare(objects[mid].Title, target) < 0)
                    low = mid + 1;
                else
                    high = mid - 1;
            }
        }
        else
            Console.WriteLine("There are no elements for request");
        return null;
    }
}
