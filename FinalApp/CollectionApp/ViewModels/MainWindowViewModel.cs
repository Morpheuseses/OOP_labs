namespace CollectionApp.ViewModels;

using System;
using Avalonia.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CollectionApp.ViewModels.Pages;
using Avalonia.Interactivity;
using ReactiveUI;
using System.Reactive;
using static System.Net.Mime.MediaTypeNames;

public partial class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
{
    public string AppName { get; } = "CollectionApp";

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

    private HomePageViewModel _homePage = new ();
    private AddElementViewModel _addElementPage = new();
    private DeleteByKeyViewModel _deleteByKeyPage = new();
    private ModifyElementViewModel _modifyElementPage = new ();

    public ObservableCollection<string> Files { get; } = new ObservableCollection<string>();

    public ReactiveCommand<Window, Unit> OpenFilesCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    
    public ObservableCollection<Node> Nodes { get; }
    public ObservableCollection<Node> SelectedNodes { get; }

    private async void OpenFile(Window parent)
    {
        StatusText = "Открыт файл";
        Console.WriteLine("Открытие файла...");  
        var files = await FileDialogService.OpenFilesAsync(parent);
        if (files.Length > 0)
        {
            StatusText = $"Открыт файл: {files[0]}";
            Console.WriteLine("Открыт файл: " + files[0]);
        }
    }
    private void RecentFile(Window parent)
    {
        StatusText = "Список последних файлов выведен.";
        Console.WriteLine("Список последних файлов выведен.");
    }
    public async void SaveFile(Window parent)
    {
        StatusText = "Сохранение файла";
        Console.WriteLine("Сохранение файла...");
         var dialog = new SaveFileDialog
        {
            Title = "Сохранить коллекцию",
            Filters =
            {
                new FileDialogFilter { Name = "JSON", Extensions = { "json" } },
                new FileDialogFilter { Name = "Все файлы", Extensions = { "*" } }
            }
        };

        // Открываем диалог и получаем выбранный путь
        var file = await dialog.ShowAsync(parent);

        if (!string.IsNullOrEmpty(file))
        {
            // Здесь добавляем логику сохранения коллекции в файл
            StatusText = $"Коллекция сохранена в {file}";
            Console.WriteLine(StatusText);
        }
    }
    private void CloseFile(Window parent)
    {
        StatusText = "Файл закрыт";
        Console.WriteLine("Файл закрыт.");
    }

    private void AddCollectionItem(Window parent)
    {
        CurrentPage = _addElementPage;
        StatusText = "Добавление элемента...";
        Console.WriteLine("Opening AddElement page");
    }
    private void ModifyCollectionItem(Window parent)
    {
        CurrentPage = _modifyElementPage;   
        StatusText = "Изменение элемента...";
        Console.WriteLine("Openning ModifyElement page...");
    }
    private void RemoveCollectionItem(Window parent)
    {
        CurrentPage = _deleteByKeyPage;
        StatusText = "Удаление элемента";
        Console.WriteLine("Openning RemoveElement page...");
    }
    private void RandomInitCollection(Window parent)
    {
        StatusText = "Коллекция была сгенерирована со случайными значениями.";
        Console.WriteLine("Openning RandomInit page...");
    }
    private void ManualInitCollectoin(Window parent)
    {
        StatusText = "Элемент удалён из коллекции.";
        Console.WriteLine("Openning ManualInit page...");
    }

    public void SaveCollection(Window parent)
    {
        StatusText = "Коллекция сохранена.";
        Console.WriteLine("Коллекция сохранена.");
    }
    public void SaveCollection()
    {
        StatusText = "Коллекция сохранена.";
        Console.WriteLine("Коллекция сохранена.");
    }
    public void ModifyCollection(Window parent)
    {
        StatusText = "Изменение коллекции.";
        Console.WriteLine("Коллекция сохранена.");
    }
    private void RemoveJournal(Window parent)
    {
        StatusText = "Журнал удалён.";
        Console.WriteLine("Журнал удалён.");
    }
    private void ShowJournal(Window parent)
    {
        StatusText = "Журнал выведен на экран.";
        Console.WriteLine("Журнал выведен на экран.");
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
    private void OpenMainPage(Window parent)
    {
        CurrentPage = _homePage;
        StatusText = "Журнал открыт.";
        Console.WriteLine("Журнал открыт.");
    }

    private void ShowCollection(Window parent)
    {

    }

    public MainWindowViewModel()
    {
        CurrentPage = _homePage;
        SaveButtonCommand = new RelayCommand(OnSaveButtonClicked);

        SelectedNodes = new ObservableCollection<Node>();
        Nodes = new ObservableCollection<Node>
        {
            new Node("Файл", subNodes: new ObservableCollection<Node>
            {
                new Node("Открыть коллекцию", w => OpenFile(w)),
                new Node("Сохранить коллекцию как", w => SaveFile(w)),
                new Node("Открыть журнал", w => OpenJournal(w)),
                new Node("Сохранить журнал как", w => SaveJournal(w)),
                //new Node("Закрыть ", () => CloseFile()),
                //new Node("Последние", () => RecentFile())
            }),
            new Node("Представления", subNodes: new ObservableCollection<Node>
            {
                new Node("Главная страница", w => OpenMainPage(w)),
                new Node("Показать коллекцию", w => ShowCollection(w)),
                new Node("Показать журнал", w => ShowJournal(w)),
                //new Node("Закрыть ", () => CloseFile()),
                //new Node("Последние", () => RecentFile())
            }),
            new Node("Коллекция", subNodes: new ObservableCollection<Node>
            {
                new Node("Случайная инициализация", w => RandomInitCollection(w)),
                new Node("Добавить элемент", w => AddCollectionItem(w)),
                new Node("Изменить элемент", w => ModifyCollectionItem(w)),
                new Node("Удалить элемент", w => RemoveCollectionItem(w)),
                new Node("Показать элемент", w => RemoveCollectionItem(w)),
                //new Node("Сохранить коллекцию", () => SaveCollection()),
            }),
            new Node("Журнал", subNodes: new ObservableCollection<Node>
            {
                new Node("Показать", w => ShowJournal(w)),
                new Node("Сохранить", w => SaveJournal(w)),
                new Node("Удалить", w => RemoveJournal(w)),
            }),
        };
    }

    private void OnSaveButtonClicked()
    {
        SaveCollection();
    }
}

