using System;
using ReactiveUI;

namespace CollectionApp.ViewModels.Pages;
public class ModifyElementViewModel : ViewModelBase
{

    public string? Title { get; set; }
    public string? Date { get; set; }
    public int DurationSeconds { get; set; }
    public int NumberOfQuestions { get; set; }
    public int NumberOfWrittenQuestions { get; set; }
    public string? GraduationLevel { get; set; }

    private string? _selectedType;
    public string? SelectedType
    {
        get => _selectedType;
        set => this.RaiseAndSetIfChanged(ref _selectedType, value);
    }


    public RelayCommand SaveCommand { get; }
    public RelayCommand SaveRandomCommand { get; }

    public ModifyElementViewModel()
    {
        SaveCommand = new RelayCommand(SaveElement);
        SaveRandomCommand = new RelayCommand(SaveRandomElement);
    }

    private void SaveElement()
    {

        Console.WriteLine($"Added:\n" +
                            $"Type={SelectedType}\n" +
                            $"Title={Title}\n" +
                            $"Date={Date}\n" +
                            $"DurationSeconds={DurationSeconds}\n" +
                            $"NumberOfQuestions={NumberOfQuestions}\n" +
                            $"NumberOfWrittenQuestions={NumberOfWrittenQuestions}\n" +
                            $"GraduationLevel={GraduationLevel}\n"
                        );

        Title = "";
        Date = "";
        DurationSeconds = 0;
        NumberOfQuestions = 0;
        NumberOfWrittenQuestions = 0;
        GraduationLevel = "";
    }
    private void SaveRandomElement()
    {
        Console.WriteLine("Добавлен случайный элемент");
    }
    
}

