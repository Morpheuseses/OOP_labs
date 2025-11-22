using System;
using ReactiveUI;
using CollectionLib;
using CollectionApp;
using Lib;
using System.Threading;

namespace CollectionApp.ViewModels.Pages;
public class AddElementViewModel : ViewModelPageBase
{

    public string Title { get; set; }
    public DateTime Date { get; set; }
    public int DurationSeconds { get; set; }
    public int NumberOfQuestions { get; set; }
    public int NumberOfWrittenQuestions { get; set; }
    public Lib.GraduationLevel GraduationLevel { get; set; }

    private string? _selectedType;
    public string? SelectedType
    {
        get => _selectedType;
        set => this.RaiseAndSetIfChanged(ref _selectedType, value);
    }

    private string? _selectedDegree;
    public string? SelectedDegree
    {
        get => _selectedDegree;
        set => this.RaiseAndSetIfChanged(ref _selectedDegree, value);
    }

    public RelayCommand SaveCommand { get; }

    public AddElementViewModel(NewAssessmentTree tree)
    {
        _tree = tree;
        SaveCommand = new RelayCommand(SaveElement);
    }

    private void SaveElement()
    {
        ErrorMessage = "";
        if (string.IsNullOrWhiteSpace(Title))
            Title = "noname";

        Assessment newElement = null;

        try
        {
            switch (SelectedType)
            {
                case "Assessment":
                    newElement = new Assessment(Title, Date, DurationSeconds);
                    break;

                case "Test":
                    newElement = new Test(Title, Date, DurationSeconds, NumberOfQuestions);
                    break;

                case "Exam":
                    newElement = new Exam(Title, Date, DurationSeconds, NumberOfQuestions, NumberOfWrittenQuestions);
                    break;

                case "FinalExam":
                    GraduationLevel level = Lib.GraduationLevel.Bachelor; 
                    try
                    {
                        if (!string.IsNullOrEmpty(SelectedDegree))
                            level = (GraduationLevel)Enum.Parse(typeof(GraduationLevel), SelectedDegree);
                    }
                    catch
                    {
                        Console.WriteLine($"Некорректная степень '{SelectedDegree}', используется по умолчанию: {level}");
                        ErrorMessage = $"Некорректная степень '{SelectedDegree}', используется по умолчанию: {level}";
                    }
                    newElement = new FinalExam(Title, Date, DurationSeconds, NumberOfQuestions, NumberOfWrittenQuestions, level);
                    break;

                default:
                    ErrorMessage = $"Неизвестный тип '{SelectedType}', создается обычный Assessment";
                    Console.WriteLine($"Неизвестный тип '{SelectedType}', создается обычный Assessment");
                    Date = DateTime.Now.AddDays(1);
                    DurationSeconds = 1000;
                    newElement = new Assessment(Title, Date, DurationSeconds);
                    break;
            }

            if (newElement != null)
                _tree.Add(newElement);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании элемента: {ex.Message}");
            ErrorMessage = $"Ошибка при создании элемента: {ex.Message}";
        }
        
    }
    
}

