namespace CollectionApp.ViewModels;

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


using CollectionLib;
using JournalLib;
using MethodsLib;
using Lib;
using FileLib;
using CollectionApp.Views.Pages;

public partial class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
{
    public string AppName { get; } = "CollectionApp";

    private static NewAssessmentTree _tree;
    public static NewAssessmentTree Tree 
    {
        get => _tree;
        set
        {
            _tree = value;
        }
    }

    private static Journal _journal;
    public static Journal Journal 
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
    private AddElementViewModel _addElementPage;
    private RemoveElementViewModel _removeElementPage;
    private ModifyElementViewModel _modifyElementPage;
    private ShowCollectionViewModel _showCollectionPage;
    private ShowJournalViewModel _showJournalPage;
    private RandomInitViewModel _randomInitPage;
    private FindElementViewModel _findElementPage;
    private MinByViewModel _minbyPage;

    public ObservableCollection<string> Files { get; } = new ObservableCollection<string>();

    public ReactiveCommand<Window, Unit> OpenFilesCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    
    public ObservableCollection<Node> Nodes { get; }
    public ObservableCollection<Node> SelectedNodes { get; }

    private async void OpenFileCollection(Window parent)
    {
        StatusText = "Открыт файл";
        Console.WriteLine("Открытие файла...");  
        var dialog = new OpenFileDialog
        {
            AllowMultiple = false,
            Filters =
            {
                new FileDialogFilter { Name = "All Supported", Extensions = { "json", "xml", "bin" } }
            }
        };

        var result = await dialog.ShowAsync(parent);
        var path = result?.FirstOrDefault();
        if (path == null) return;

        IFileSerializer<NewAssessmentTree> serializer = path switch
        {
            var p when p.EndsWith(".json") => new JsonSerializerWrapper<NewAssessmentTree>(),
            var p when p.EndsWith(".xml")  => new XmlSerializerWrapper<NewAssessmentTree>(),
            var p when p.EndsWith(".bin")  => new BinSerializer<NewAssessmentTree>(),
            _ => throw new Exception("Неизвестный формат файла")
        };

        Tree = serializer.Load(path);
    }
    public async void SaveFileCollection(Window parent)
    {
        StatusText = "Сохранение файла";
        Console.WriteLine("Сохранение файла...");
        var dialog = new SaveFileDialog
        {
            Filters =
            {
                new FileDialogFilter { Name = "JSON", Extensions = { "json" } },
                new FileDialogFilter { Name = "XML", Extensions = { "xml" } },
                new FileDialogFilter { Name = "Binary", Extensions = { "bin" } }
            }
        };

        var path = await dialog.ShowAsync(parent);
        if (path == null) return;

        IFileSerializer<NewAssessmentTree> serializer = path switch
        {
            var p when p.EndsWith(".json") => new JsonSerializerWrapper<NewAssessmentTree>(),
            var p when p.EndsWith(".xml")  => new XmlSerializerWrapper<NewAssessmentTree>(),
            var p when p.EndsWith(".bin")  => new BinSerializer<NewAssessmentTree>(),
            _ => throw new Exception("Неизвестный формат файла")
        };

        serializer.Save(path, Tree);
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
    private void SaveJournal(Window parent)
    {
        StatusText = "Журнал сохранен.";
        Console.WriteLine("Журнал сохранен.");
    }
    private void OpenJournal(Window parent)
    {
        StatusText = "Журнал открыт.";
        Console.WriteLine("Журнал открыт.");
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
        CurrentPage = _removeElementPage;
        StatusText = "Удаление элемента";
        Console.WriteLine("Openning RemoveElement page...");
    }
    private void FindCollectionItem()
    {
        CurrentPage = _findElementPage;
        StatusText = "Поиск элемента";
        Console.WriteLine("Openning FindElement page...");
    }
    private void RandomInitCollection()
    {
        Console.WriteLine("Openning RandomInit page...");
        StatusText = "Генерируем новые значения.";
        //Assessment[]? elements = null;
        //Request.RandomInitObjects(ref elements, 10);
        CurrentPage = _randomInitPage;
        StatusText = "Коллекция была сгенерирована со случайными значениями.";
    }
    private void RemoveJournal()
    {
        StatusText = "Журнал удалён.";
        Console.WriteLine("Журнал удалён.");
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
        CurrentPage = _minbyPage;
    }
    private void MaxBy()
    {
        Console.WriteLine("MaxBy\n");
        CurrentPage = _minbyPage;
    }
    private void TopN()
    {
        Console.WriteLine("TopN\n");
        var items = AssessmentQuery.TopN(Tree, 5,a => a.DurationSeconds > 80,  a => a.DurationSeconds);
        
        foreach (var a in items)
        {
            Console.WriteLine($"{a.Title} - {a.DurationSeconds}");
        }
    }
    private void ByTitle()
    {
        Console.WriteLine("ByTitle\n");
        var items = AssessmentFilter.ByTitleContains(Tree, "noname");
        
        foreach (var a in items)
        {
            Console.WriteLine($"{a.Title}");
        }
    }
    private void OnlyTests()
    {
        Console.WriteLine("OnlyTests\n");
        var items = AssessmentFilter.OnlyTests(Tree);

        foreach (var a in items)
        {
            Console.WriteLine($"{a.Title} - {a.DurationSeconds}");
        }
    }
    private void OnlyExams()
    {
        Console.WriteLine("OnlyExams\n");
        var items = AssessmentFilter.OnlyExams(Tree);
        
        foreach (var a in items)
        {
            Console.WriteLine($"{a.Title} - {a.DurationSeconds}");
        }
    }
    private void OnlyFinalExams()
    {
        Console.WriteLine("OnlyFinalExams\n");
        var items = AssessmentFilter.OnlyFinals(Tree);
        
        foreach (var a in items)
        {
            Console.WriteLine($"{a.Title} - {a.DurationSeconds}");
        }
    }
    public MainWindowViewModel()
    {
        _tree = new NewAssessmentTree();
        _journal = new Journal();

        _tree.CollectionCountChanged += _journal.CollectionCountChanged;
        _tree.CollectionReferenceChanged+= _journal.CollectionReferenceChanged;

        _homePage = new ();

        _randomInitPage = new RandomInitViewModel(Tree);
        _findElementPage = new FindElementViewModel(Tree);
        _addElementPage = new AddElementViewModel(Tree);
        _modifyElementPage = new ModifyElementViewModel(Tree);
        _removeElementPage = new RemoveElementViewModel(Tree);
        _minbyPage = new MinByViewModel(Tree);
        

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
            }),
            new Node("Представления", subNodes: new ObservableCollection<Node>
            {
                new Node("Главная страница", w => OpenMainPage()),
                new Node("Показать коллекцию", w => ShowCollection()),
                new Node("Показать отчет", w => ShowCollection()),
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
            new Node("Журнал", subNodes: new ObservableCollection<Node>
            {
                new Node("Сохранить", w => SaveJournal(w)),
                new Node("Удалить", w => RemoveJournal()),
            }),
            new Node("Запрос", subNodes: new ObservableCollection<Node>
            {
                new Node("Минимальный по полю", w => MinBy()),
                new Node("Максимальный по полю", w => MaxBy()),
                new Node("Наибольшие N", w => TopN()),
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

