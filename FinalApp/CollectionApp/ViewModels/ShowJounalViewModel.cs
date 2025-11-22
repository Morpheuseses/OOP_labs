using System;
using CollectionLib;
using Lib;
using JournalLib;
using ReactiveUI;

namespace CollectionApp.ViewModels.Pages;
public class ShowJournalViewModel : ViewModelPageBase
{
    public ShowJournalViewModel(Journal journal)
    {
        _outputText = journal.ToString(); 
    }
    
}

