using System;
using ReactiveUI;
using CollectionLib;
using CollectionApp;
using Lib;
using System.Threading;

namespace CollectionApp.ViewModels.Pages;

public class ModifyElementViewModel : ViewModelPageBase
{
    private string _title = "";
    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    private DateTime _date = DateTime.Now;
    public DateTime Date
    {
        get => _date;
        set => this.RaiseAndSetIfChanged(ref _date, value);
    }

    private int _durationSeconds;
    public int DurationSeconds
    {
        get => _durationSeconds;
        set => this.RaiseAndSetIfChanged(ref _durationSeconds, value);
    }

    private int _numberOfQuestions;
    public int NumberOfQuestions
    {
        get => _numberOfQuestions;
        set => this.RaiseAndSetIfChanged(ref _numberOfQuestions, value);
    }

    private int _numberOfWrittenQuestions;
    public int NumberOfWrittenQuestions
    {
        get => _numberOfWrittenQuestions;
        set => this.RaiseAndSetIfChanged(ref _numberOfWrittenQuestions, value);
    }

    private GraduationLevel _graduationLevel = GraduationLevel.Bachelor;
    public GraduationLevel GraduationLevel
    {
        get => _graduationLevel;
        set => this.RaiseAndSetIfChanged(ref _graduationLevel, value);
    }

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

    public ModifyElementViewModel(NewAssessmentTree tree)
    {
        _tree = tree;
        SaveCommand = new RelayCommand(UpdateElement);
    }

    private void UpdateElement()
    {
        ErrorMessage = "";

        try
        {
            var node = _tree.FindNodeByTitle(Title, _tree.RootNode);

            if (node == null)
            {
                ErrorMessage = $"Элемент с названием '{Title}' не найден";
                return;
            }

            Assessment updated = null;

            if (SelectedType == "Assessment" && node.Data is Assessment)
            {
                updated = new Assessment(Title, Date, DurationSeconds);
            }
            else if (SelectedType == "Test" && node.Data is Test)
            {
                updated = new Test(Title, Date, DurationSeconds, NumberOfQuestions);
            }
            else if (SelectedType == "Exam" && node.Data is Exam)
            {
                updated = new Exam(Title, Date, DurationSeconds, NumberOfQuestions, NumberOfWrittenQuestions);
            }
            else if (SelectedType == "FinalExam" && node.Data is FinalExam)
            {
                updated = new FinalExam(Title, Date, DurationSeconds, NumberOfQuestions, NumberOfWrittenQuestions, GraduationLevel);
            }
            else
            {
                ErrorMessage = "Неверный тип или несоответствие существующего объекта.";
                return;
            }

            node.Data = updated; 
            Console.WriteLine($"Элемент '{Title}' обновлен");
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Ошибка при обновлении элемента: {ex.Message}";
        }
    }
}
