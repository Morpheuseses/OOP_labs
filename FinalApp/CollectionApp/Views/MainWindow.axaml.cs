using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Tmds.DBus.Protocol;
using CollectionApp.ViewModels;

namespace CollectionApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    public void CloseButtonClickHandler(object sender, RoutedEventArgs args)
    {
        Console.WriteLine("Close button is clicked!");
        Environment.Exit(0);
    }
    private void NodeButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is Button btn && btn.DataContext is Node node)
        {
            node.Command?.Invoke(this); // передаем текущее окно
        }
    }

}