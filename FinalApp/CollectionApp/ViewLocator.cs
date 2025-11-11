using Avalonia.Controls;
using Avalonia.Controls.Templates;
using System;
using CollectionApp.ViewModels;

namespace CollectionApp
{
    public class ViewLocator : IDataTemplate
    {
        public Control Build(object? data)
        {
            if (data == null)
                return new TextBlock { Text = "No data" };

            // Получаем полное имя ViewModel
            var viewModelFullName = data.GetType().FullName!;

            // Заменяем namespace ViewModel на namespace View
            // И убираем суффикс "ViewModel"
            var viewFullName = viewModelFullName
                                .Replace("CollectionApp.ViewModels.Pages", "CollectionApp.Views.Pages")
                                .Replace("ViewModel", "");

            // Ищем тип View
            var type = Type.GetType(viewFullName);

            if (type != null)
            {
                var control = (Control)Activator.CreateInstance(type)!;
                control.DataContext = data;
                return control;
            }

            return new TextBlock { Text = $"View not found for {viewModelFullName}" };
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}
