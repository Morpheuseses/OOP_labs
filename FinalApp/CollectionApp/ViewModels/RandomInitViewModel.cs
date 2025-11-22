using System;
using CollectionLib;
using Lib;
using ReactiveUI;

namespace CollectionApp.ViewModels.Pages;
public class RandomInitViewModel : ViewModelPageBase
{   
    private int _count;
    public int Count 
    {
        get => _count;
        set
        {
            _count = value;
            OnPropertyChanged();
        }
    }
    public RelayCommand RandomInitCommand { get; }
    public RandomInitViewModel(NewAssessmentTree tree)
    {
        Count = 10;
        _tree = tree;
        RandomInitCommand = new RelayCommand(RandomInit);
    }
    
    private void RandomInit()
    {
        if (Count <= 0)
        {
            Console.WriteLine("Count must be greater and not lower than 0");
            return;
        }
            
        Assessment[] elements = Request.RandomInitUniqueAssessments(Count);
        if (elements is null)
        {
            Console.WriteLine("Something is gone wrong");
            return;
        }
        _tree.Clear();
        _tree.AddRange(elements);
        
    }
    
}

