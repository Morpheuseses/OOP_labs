using System;
using ReactiveUI;
using CollectionLib;
using CollectionApp;
using Lib;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

using MethodsLib;

namespace CollectionApp.ViewModels.Pages;

public class ByTitleContainsViewModel : ViewModelPageBase
{
    private string _subTitle = "";
    public string SubTitle
    {
        get => _subTitle;
        set {
            this.RaiseAndSetIfChanged(ref _subTitle, value);
            OnPropertyChanged();
        }
    }

    public RelayCommand RequestCommand { get; }

    public ByTitleContainsViewModel(NewAssessmentTree tree)
    {
        _tree = tree;
        RequestCommand = new RelayCommand(MakeRequest);
    }

    private void MakeRequest()
    {
        ErrorMessage = "";

        try
        {
            OutputText = $"Испытания с подстрокой {SubTitle} в названии:\n";
            var items = AssessmentFilter.ByTitleContains(_tree,SubTitle);
            if (!items.Any())
            {
                OutputText += "Элементы с такой подстрокой не найдены\n";
                return;
            }
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
