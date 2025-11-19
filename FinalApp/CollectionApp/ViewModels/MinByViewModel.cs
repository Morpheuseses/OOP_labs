using System;
using ReactiveUI;
using CollectionLib;
using CollectionApp;
using Lib;
using System.Threading;
using System.Collections.Generic;

using MethodsLib;

namespace CollectionApp.ViewModels.Pages;

public class MinByViewModel : ViewModelBase
{
    private NewAssessmentTree _tree;

    private string _title = "";
    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }
    private string _fieldType;
    public string FieldType
    {
        get => _fieldType;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }
    private string? _outputText;
    public string? OutputText
    {
        get => _outputText;
        set
        {
            if (_outputText != value)
            {
                _outputText = value;
                OnPropertyChanged();
            }
        }
    }

    private string _errorMessage = "";
    public string ErrorMessage
    {
        get => _errorMessage;
        set {
            this.RaiseAndSetIfChanged(ref _errorMessage, value);
            OnPropertyChanged();
        }
    }

    public RelayCommand RequestCommand { get; }

    public MinByViewModel(NewAssessmentTree tree)
    {
        _tree = tree;
        RequestCommand = new RelayCommand(MakeRequest);
    }

    private void MakeRequest()
    {
        ErrorMessage = "";

        try
        {
            Assessment item;
            switch (FieldType)
            {
                case "Date":
                    item = AssessmentQuery.MinBy(_tree, a => a.Date);     
                    OutputText = item.ToString() + "\n";
                    break;
                case "Duration(seconds)":
                    item = AssessmentQuery.MinBy(_tree, a => a.DurationSeconds);
                    OutputText = item.ToString() + "\n";
                    break;
                case "Title":
                    item = AssessmentQuery.MinBy(_tree, a => a.Title);
                    OutputText = item.ToString() + "\n";
                    break;
                //case "Number of questions":
                //    items = AssessmentQuery.MinBy(_tree, a => a.NumberOfQuestions);
                //    foreach (var item in items)
                //    {
               //         OutputText = item.ToString();
               //     }
               //     break;
                default:
                    ErrorMessage = $"Не было выбрано поле. Запрос будет выполнен по полю DurationSeconds";
                    item = AssessmentQuery.MinBy(_tree, a => a.DurationSeconds);
                    OutputText = item.ToString() + "\n";
                    break;

            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Ошибка при выполнении запроса: {ex.Message}";
        }
    }
}
