using Lib;
using System;
using System.Collections.Generic;

namespace EventLib;

public delegate void NewAssessmentTreeHandler(object source, NewAssessmentTreeEventArgs args);
public class NewAssessmentTreeEventArgs : EventArgs
{
    public string Name { get; set; }
    public string EventType { get; set; }
    public Assessment Object { get; set; }

    public NewAssessmentTreeEventArgs(string name, string eventType, Assessment? changedObject)
    {
        this.Name = name;
        this.EventType = eventType;
        this.Object = changedObject ?? default!;
    }
    public override string ToString()
    {
        return $"NewAssessmentTreeEventArgs: #{GetHashCode} Name: {Name}, Event type: {EventType}";
    }
}
