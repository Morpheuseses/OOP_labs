using Lib;
namespace EventAppLib;

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
