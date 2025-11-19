using System;
using CollectionLib;
using Lib;
using JournalLib;
using ReactiveUI;

namespace CollectionApp.ViewModels.Pages;
public class ShowJournalViewModel : ViewModelBase
{
    private string _outputText;
    public string OutputText 
    {
        get => _outputText;
    }
    public ShowJournalViewModel(Journal journal)
    {
        _outputText = journal.ToString(); 
    }
    
}

