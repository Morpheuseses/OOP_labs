using System;
using System.Windows.Input;
using CollectionLib;
using Lib;

namespace CollectionApp.ViewModels.Pages;

public class RemoveElementViewModel : ViewModelBase
{
    private NewAssessmentTree _tree;
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
    public ICommand RemoveElementCommand { get; }
    public RemoveElementViewModel(NewAssessmentTree tree)
    {
        _tree = tree;
        RemoveElementCommand = new RelayCommand(RemoveElement);
    }
    private void RemoveElement()
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            Console.WriteLine("Ошибка: наименование не указано.");
            return;
        }
        Console.WriteLine($"Searching for element with Title: {Title}");
        var node = _tree.FindNodeByTitle(Title,_tree.RootNode);
        OutputText = "Удаляемый элемент коллекции:\n" + node.ToString();
        Title = "";
        _tree.Remove(node.Data);
    }
}

