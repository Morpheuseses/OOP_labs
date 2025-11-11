using System;
using System.Windows.Input;

namespace CollectionApp.ViewModels.Pages;

public class DeleteByKeyViewModel : ViewModelBase
{
    private string? _key;
    public string? Key
    {
        get => _key;
        set
        {
            if (_key != value)
            {
                _key = value;
                OnPropertyChanged(nameof(Key));
            }
        }
    }
    public ICommand DeleteCommand { get; }
    public DeleteByKeyViewModel()
    {
        DeleteCommand = new RelayCommand(DeleteElement);
    }
    private void DeleteElement()
    {
        if (string.IsNullOrWhiteSpace(Key))
        {
            Console.WriteLine("Ошибка: ключ не указан.");
            return;
        }
        Console.WriteLine($"Удаляю элемент с ключом: {Key}");
        Key = "";
    }
}

