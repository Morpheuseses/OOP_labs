

using System;
using Avalonia.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CollectionApp.ViewModels.Pages;
using Avalonia.Interactivity;
using ReactiveUI;
using System.Reactive;
using static System.Net.Mime.MediaTypeNames;

using CollectionApp.Helpers;
using CollectionLib;
using JournalLib;
using MethodsLib;
using Lib;
using FileLib;
using CollectionApp.Views.Pages;

namespace CollectionApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string AppName { get; } = "CollectionApp";

    private static string filePathReport = "/media/Crossdrive/university/OOP/ActualStuff/Implementation/FinalApp/CollectionApp/Saves/Reports/actual_report.txt"; 
    private static string filePathJournal = "/media/Crossdrive/university/OOP/ActualStuff/Implementation/FinalApp/CollectionApp/Saves/Journal/actual_journal.txt"; 

    private static NewAssessmentTree _tree;
    public static NewAssessmentTree Tree 
    {
        get => _tree;
        set
        {
            _tree = value;
        }
    }

    private static JournalTxt _journal;
    public static JournalTxt Journal 
    {
        get => _journal; 
        set
        {
            _journal = value;
        }
    }

    public string SaveButton { get; } = "";
    public string Close { get; } = "";

    public ICommand SaveButtonCommand { get; }

    private string _statusText = "Nothing has happened";
    public string StatusText
    {
        get => _statusText;
        set
        {
            if (_statusText != value)
            {
                _statusText = value;
                OnPropertyChanged();
            }
        }
    }

    private string _outputText = "Here will be output";
    public string OutputText
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
    private ViewModelBase _currentPage;
    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        set
        {
            if (_currentPage != value)
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }
    }

    private HomePageViewModel _homePage;

    //public ObservableCollection<string> Files { get; } = new ObservableCollection<string>();

    public ReactiveCommand<Window, Unit> OpenFilesCommand { get; }
    
    public ObservableCollection<Node> Nodes { get; }
    public ObservableCollection<Node> SelectedNodes { get; }

    private async void OpenFileCollection(Window parent)
    {
        StatusText = "Открыт файл";
        Console.WriteLine("Открытие файла...");  

        try
        {
            var tree = await FileDialogsHelper.OpenFileCollectionAsync(parent);
            if (tree != null)
            {
                Tree = tree;
                StatusText = "Файл успешно открыт";
                Console.WriteLine("Файл успешно открыт");
            }
            else
            {
                StatusText = "Открытие файла отменено";
                Console.WriteLine("Открытие файла отменено");
            }
        }
        catch (Exception ex)
        {
            StatusText = $"Ошибка при открытии файла: {ex.Message}";
            Console.WriteLine($"Ошибка при открытии файла: {ex}");
        }
    }
    public async void SaveFileCollection(Window parent)
    {
        StatusText = "Сохранение файла";
        Console.WriteLine("Сохранение файла...");
        try
        {
            await FileDialogsHelper.SaveFileCollectionAsync(parent, Tree);
            StatusText = "Файл успешно сохранён";
            Console.WriteLine("Файл успешно сохранён");
        }
        catch (Exception ex)
        {
            StatusText = $"Ошибка при сохранении файла: {ex.Message}";
            Console.WriteLine($"Ошибка при сохранении файла: {ex}");
        }
    }
    private void ShowCollection()
    {
        StatusText = "Открыта страница для показа коллекции.";
        Console.WriteLine("Opennig ShowCollection page...");
        CurrentPage = new ShowCollectionViewModel(Tree);
    }
    private void ClearCollection()
    {
        StatusText = "Коллекция очищена";
        Console.WriteLine("Clearing collection");
        Tree.Clear();
        Console.WriteLine("Collection cleared");
    }
    private void ShowJournal()
    {
        StatusText = "Открыта страница для показа журнала";
        Console.WriteLine("Openning ShowJournal page");
        CurrentPage = new ShowJournalViewModel(Journal);
    }
    private async void SaveJournal(Window parent)
    {
        StatusText = "Сохранение журнала...";
        Console.WriteLine("Saving journal...");

        FileDialogsHelper.SaveJournalAsync(parent, Journal);
        StatusText = "Журнал сохранен.";
    }
    private async void OpenJournal(Window parent)
    {
        try
        {
            StatusText = "Открытие журнала.";
            Console.WriteLine("Opening journal...");

            var journal = await FileDialogsHelper.OpenJournalAsync(parent);
            if (journal != null)
            {
                Journal = journal;
                StatusText = "Журнал открыт.";
            }
            else
            {
                StatusText = "Открытие журнала отменено.";
            }
        }
        catch (Exception ex)
        {
            StatusText = $"Ошибка при открытии журнала: {ex.Message}";
            Console.WriteLine(ex);
        }
    }

    private void AddCollectionItem()
    {
        CurrentPage = new AddElementViewModel(Tree);
        StatusText = "Добавление элемента...";
        Console.WriteLine("Opening AddElement page");
    }
    private void ModifyCollectionItem()
    {
        CurrentPage = new ModifyElementViewModel(Tree);
        StatusText = "Изменение элемента...";
        Console.WriteLine("Openning ModifyElement page...");
        
    }
    private void RemoveCollectionItem()
    {
        CurrentPage = new RemoveElementViewModel(Tree);
        StatusText = "Удаление элемента";
        Console.WriteLine("Openning RemoveElement page...");
    }
    private void FindCollectionItem()
    {
        CurrentPage = new FindElementViewModel(Tree);
        StatusText = "Поиск элемента";
        Console.WriteLine("Openning FindElement page...");
    }
    private void RandomInitCollection()
    {
        Console.WriteLine("Openning RandomInit page...");
        StatusText = "Генерируем новые значения.";
        //Assessment[]? elements = null;
        //Request.RandomInitObjects(ref elements, 10);
        CurrentPage = new RandomInitViewModel(Tree);
        StatusText = "Коллекция была сгенерирована со случайными значениями.";
    }
    private void ShowReport()
    {
        Console.WriteLine("Openning Report page...");
        StatusText = "Открываю отчет...";
        CurrentPage = new ShowReportViewModel(filePathReport);
    }
    private void OpenMainPage()
    {
        CurrentPage = _homePage;
        StatusText = "Журнал открыт.";
        Console.WriteLine("Журнал открыт.");
    }
    private void SaveCollection()
    {
        
    }
    private void MinBy()
    {
        Console.WriteLine("MinBy\n");
        CurrentPage = new MinByViewModel(Tree,filePathReport);
    }
    private void MaxBy()
    {
        Console.WriteLine("MaxBy\n");
        CurrentPage = new MaxByViewModel(Tree,filePathReport);
    }
    private void CountBySubclass()
    {
        Console.WriteLine("CountBySubclass\n");
        CurrentPage = new CountBySubclassViewModel(Tree,filePathReport);
    }
    private void ByTitle()
    {
        Console.WriteLine("ByTitle\n");
        CurrentPage = new ByTitleContainsViewModel(Tree);
    }
    private void OnlyTests()
    {
        Console.WriteLine("OnlyTests\n");
        CurrentPage = new OnlyTestsViewModel(Tree);
    }
    private void OnlyExams()
    {
        Console.WriteLine("OnlyExams\n");
        CurrentPage = new OnlyExamsViewModel(Tree);
    }
    private void OnlyFinalExams()
    {
        Console.WriteLine("OnlyFinalExams\n");
         CurrentPage = new OnlyFinalExamsViewModel(Tree);
    }
    public MainWindowViewModel()
    {
        _tree = new NewAssessmentTree();
        _journal = new JournalTxt(filePathJournal);

        _tree.CollectionCountChanged += _journal.CollectionCountChanged;
        _tree.CollectionReferenceChanged += _journal.CollectionReferenceChanged;

        _homePage = new ();
    
        CurrentPage = _homePage;
        SaveButtonCommand = new RelayCommand(OnSaveButtonClicked);


        SelectedNodes = new ObservableCollection<Node>();
        Nodes = new ObservableCollection<Node>
        {
            new Node("Файл", subNodes: new ObservableCollection<Node>
            {
                new Node("Открыть коллекцию", w => OpenFileCollection(w)),
                new Node("Сохранить коллекцию как", w => SaveFileCollection(w)),
                new Node("Открыть журнал", w => OpenJournal(w)),
                new Node("Сохранить журнал как", w => SaveJournal(w)),
                new Node("Открыть отчет", w => OpenJournal(w)),
            }),
            new Node("Представления", subNodes: new ObservableCollection<Node>
            {
                new Node("Главная страница", w => OpenMainPage()),
                new Node("Показать коллекцию", w => ShowCollection()),
                new Node("Показать отчет", w => ShowReport()),
                new Node("Показать журнал", w => ShowJournal()),
            }),
            new Node("Коллекция", subNodes: new ObservableCollection<Node>
            {
                new Node("Случайная инициализация", w => RandomInitCollection()),
                new Node("Добавить элемент", w => AddCollectionItem()),
                new Node("Изменить элемент", w => ModifyCollectionItem()),
                new Node("Удалить элемент", w => RemoveCollectionItem()),
                new Node("Найти элемент", w => FindCollectionItem()),
                new Node("Удалить все", w => ClearCollection()),
            }),
            //new Node("Журнал", subNodes: new ObservableCollection<Node>
            //{
            //    new Node("Сохранить", w => SaveJournal(w)),
            //    new Node("Удалить", w => RemoveJournal()),
            //}),
            new Node("Запрос", subNodes: new ObservableCollection<Node>
            {
                new Node("Минимальный по полю", w => MinBy()),
                new Node("Максимальный по полю", w => MaxBy()),
                new Node("Количество по типу", w => CountBySubclass()),
            }),
            new Node("Фильтрация", subNodes: new ObservableCollection<Node>
            {
                new Node("По названию", w => ByTitle()),
                new Node("Только тесты", w => OnlyTests()),
                new Node("Только экзамены", w => OnlyExams()),
                new Node("Только выпускные экзамены", w => OnlyFinalExams()),
            }),
        };
    }

    private void OnSaveButtonClicked()
    {
        SaveCollection();
    }
}

