using System;
using System.IO;
using ReactiveUI;
using CollectionLib;
using CollectionApp;
using Lib;
using System.Threading;
using System.Collections.Generic;

using MethodsLib;

namespace CollectionApp.ViewModels.Pages;

public class MaxByViewModel : ViewModelPageBase, ILogger<string>
{
    private string _filePath = "";
    private string? _fieldType;
    public string? FieldType
    {
        get => _fieldType;
        set => this.RaiseAndSetIfChanged(ref _fieldType, value);
    }   

    public RelayCommand RequestCommand { get; }

    public MaxByViewModel(NewAssessmentTree tree,string filePath)
    {
        _tree = tree;
        _filePath = filePath;
        RequestCommand = new RelayCommand(MakeRequest);
    }
    public void Append(string filePath, string data)
    {
        var dir = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        var toSave = $"MaxBy {DateTime.Now.ToString()}\n\n";
        File.AppendAllText(_filePath, toSave + data + "\n");
    }
    private void MakeRequest()
    {
        ErrorMessage = "";

        try
        {
            Assessment item;
            Console.WriteLine("Field type:" + FieldType);
            switch (FieldType)
            {
                case "Date":
                    item = AssessmentQuery.MinBy(_tree, a => a.Date);     
                    OutputText = item.ToString() + "\n";
                    break;
                case "Duration(Seconds)":
                    item = AssessmentQuery.MinBy(_tree, a => a.DurationSeconds);
                    OutputText = item.ToString() + "\n";
                    break;
                case "Title":
                    item = AssessmentQuery.MinBy(_tree, a => a.Title);
                    OutputText = item.ToString() + "\n";
                    break;
                default:
                    ErrorMessage = $"Не было выбрано поле. Запрос будет выполнен по полю DurationSeconds";
                    item = AssessmentQuery.MinBy(_tree, a => a.DurationSeconds);
                    OutputText = item.ToString() + "\n";
                    break;

            }
            Append(_filePath, item.ToString());
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Ошибка при выполнении запроса: {ex.Message}";
        }
    }
}
