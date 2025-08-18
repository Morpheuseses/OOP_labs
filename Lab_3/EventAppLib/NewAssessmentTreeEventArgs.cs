namespace EventAppLib;

public class NewAssessmentTreeEventArgs : EventArgs
{
    public string Name { get; set; }
    public string EventType { get; set; }
    public string Object { get; set; }

    public NewAssessmentTreeEventArgs(string name, string eventType, string obj)
    {
        this.Name = name;
        this.EventType = eventType;
        this.Object = obj;
    }
    public override string ToString()
    {
        return $"NewAssessmentTreeEventArgs: #{GetHashCode} Name: {Name}, Event type: {EventType}, obj: {Object}";
    }
}
