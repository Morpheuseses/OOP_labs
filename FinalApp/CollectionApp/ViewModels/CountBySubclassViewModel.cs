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

public class CountBySubclassViewModel : ViewModelPageBase, ILogger<string>
{
    private string _filePath = "";
    public RelayCommand RequestCommand { get; }

    public CountBySubclassViewModel(NewAssessmentTree tree, string filePath)
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
        OutputText = "";
        try
        {
            var items = AssessmentQuery.CountBySubclass(_tree);
            foreach (var kv in items)
            {
                OutputText += $"{kv.Type.Name} - {kv.Count}\n";
            }
            Append(_filePath, OutputText);
            OutputText = "Тип испытания - количество:\n" + OutputText;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Ошибка при выполнении запроса: {ex.Message}";
        }
    }
}
