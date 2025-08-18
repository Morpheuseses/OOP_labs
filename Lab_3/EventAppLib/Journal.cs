using CollectionLib;
using Lib;

namespace EventAppLib;

public class Journal
{
    private List<JournalEntry> entries;
    public string Name { get; set; }
    public class JournalEntry
    {
        public string Name { get; set; }
        public string EventType { get; set; }
        public string Object { get; set; }
        public JournalEntry(string name, string eventType, string obj)
        {
            this.Name = name;
            this.EventType = eventType;
            this.Object = obj;
        }
    }
    public Journal(string name)
    {
        this.Name = name;
        entries = new List<JournalEntry>();
    }
    public void CollectionCountChanged(object source, NewAssessmentTreeEventArgs args)
    {
        entries.Add(new JournalEntry(args.Name, args.EventType, source.ToString().Replace("\n", " ").Replace("-", "")));
    }
    public void CollectionReferenceChanged(object source, NewAssessmentTreeEventArgs args)
    {
        entries.Add(new JournalEntry(args.Name, args.EventType, source.ToString().Replace("\n", " ").Replace("-", "")));
    }
    public override string ToString()
    {
        return base.ToString();
    }
}
