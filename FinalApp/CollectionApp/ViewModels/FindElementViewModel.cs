using System;
using System.Windows.Input;
using CollectionLib;
using Lib;

namespace CollectionApp.ViewModels.Pages;

public class FindElementViewModel : ViewModelPageBase
{
    private string? _title;
    public string? Title
    {
        get => _title;
        set
        {
            if (_title != value)
            {
                _title = value;
                OnPropertyChanged();
            }
        }
    }
    private ICommand FindElementCommand { get; }
    public FindElementViewModel(NewAssessmentTree tree)
    {
        _tree = tree;
        FindElementCommand = new RelayCommand(FindElement);
    }
    private void FindElement()
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            Console.WriteLine("Ошибка: наименование не указано.");
            return;
        }
        Console.WriteLine($"Searching for element with Title: {Title}");
        var node = _tree.FindNodeByTitle(Title,_tree.RootNode);
        OutputText = "Найденный элемент коллекции:\n" + node.ToString();
        Title = "";
    }
}

