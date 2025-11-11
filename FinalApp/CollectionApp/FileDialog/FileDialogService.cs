using Avalonia.Controls;
using System.Threading.Tasks;

public static class FileDialogService
{
    public static async Task<string[]> OpenFilesAsync(Window parent)
    {
        var dialog = new OpenFileDialog
        {
            Title = "Открыть коллекцию",
            AllowMultiple = false,
            Filters = { new FileDialogFilter { Name = "JSON", Extensions = { "json" } } }
        };
        return await dialog.ShowAsync(parent) ?? new string[0];
    }

    public static async Task<string> SaveFileAsync(Window parent)
    {
        var dialog = new SaveFileDialog
        {
            Title = "Сохранить коллекцию",
            Filters = { new FileDialogFilter { Name = "JSON", Extensions = { "json" } } }
        };
        return await dialog.ShowAsync(parent) ?? string.Empty;
    }
}