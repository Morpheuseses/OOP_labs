using System;
using CollectionLib;
using Lib;
using ReactiveUI;

namespace CollectionApp.ViewModels.Pages;
public class ShowCollectionViewModel : ViewModelBase
{
    private string _outputText;
    public string OutputText 
    {
        get => _outputText;
        set
        {
            _outputText = value;
            OnPropertyChanged();
        }
    }
    public ShowCollectionViewModel(NewAssessmentTree tree)
    {
        if (tree.Length != 0)
            OutputText = tree.GetConsoleTreeString(); 
        else
            OutputText = "В этой коллекции нет элементов на данный момент";
    }
    
}

