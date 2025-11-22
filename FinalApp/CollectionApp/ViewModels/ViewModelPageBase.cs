using System;
using ReactiveUI;
using CollectionApp;
using CollectionLib;

namespace CollectionApp.ViewModels.Pages;
public class ViewModelPageBase : ViewModelBase
{
    protected NewAssessmentTree _tree;

    protected string? _outputText;
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

    protected string _errorMessage = "";
    public string ErrorMessage
    {
        get => _errorMessage;
        set {
            this.RaiseAndSetIfChanged(ref _errorMessage, value);
            OnPropertyChanged();
        }
    }
} 