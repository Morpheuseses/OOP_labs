using System;
using System.IO;
using CollectionLib;
using Lib;
using JournalLib;
using ReactiveUI;

namespace CollectionApp.ViewModels.Pages;
public class ShowReportViewModel : ViewModelPageBase
{
    public ShowReportViewModel(string filePath)
    {
         try
        {
            if (File.Exists(filePath))
            {
                OutputText = File.ReadAllText(filePath);
            }
            else
            {
                OutputText = "Файл отчёта не найден.";
            }
        }
        catch (Exception ex)
        {
            OutputText = $"Ошибка при чтении файла: {ex.Message}";
        }
    }
    
}

