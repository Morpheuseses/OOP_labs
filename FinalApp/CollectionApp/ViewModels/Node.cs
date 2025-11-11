using System;
using System.Collections.ObjectModel;

public class Node
{
    public string Title { get; }
    public ObservableCollection<Node>? SubNodes { get; }
    public Action<Avalonia.Controls.Window>? Command { get; }

    public Node(string title, Action<Avalonia.Controls.Window>? command = null, ObservableCollection<Node>? subNodes = null)
    {
        Title = title;
        Command = command;
        SubNodes = subNodes ?? new ObservableCollection<Node>();
    }
}