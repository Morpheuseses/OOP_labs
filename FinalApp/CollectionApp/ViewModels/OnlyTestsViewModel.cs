using System;
using ReactiveUI;
using CollectionLib;
using CollectionApp;
using Lib;
using System.Threading;
using System.Collections.Generic;

using MethodsLib;

namespace CollectionApp.ViewModels.Pages;

public class OnlyTestsViewModel : ViewModelPageBase
{
    public RelayCommand RequestCommand { get; }

    public OnlyTestsViewModel(NewAssessmentTree tree)
    {
        _tree = tree;
        RequestCommand = new RelayCommand(MakeRequest);
    }
    
    private void MakeRequest()
    {
        ErrorMessage = "";

        try
        {
            OutputText = "Все тесты из коллекции:\n";
            var items = AssessmentFilter.OnlyTests(_tree);
            foreach (var item in items)
            {
                OutputText += $"{item.ToString()}\n";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Ошибка при выполнении запроса: {ex.Message}";
        }
    }
}
