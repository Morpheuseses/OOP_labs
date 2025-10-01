using CollectionLib;
using Lib;

namespace EventLib;

public class Journal
{
    private List<JournalEntry> entries;
    public class JournalEntry
    {
        public string Name { get; }
        public string EventType { get; }
        public string ObjectInfo { get; }
        public JournalEntry(string name, string eventType, string obj)
        {
            this.Name = name;
            this.EventType = eventType;
            this.ObjectInfo = obj;
        }
        public override string ToString()
        {
            return $"Collection name: {Name}\n"
            + $"Collection change type: {EventType}\n"
            + $"Collection object information: {ObjectInfo}\n";
        }
    }
    public Journal()
    {
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
        string result = "";
        result += "\tJournal start\n";
        foreach (JournalEntry entry in this.entries)
        {
            result += entry.ToString() + '\n';
        }
        result += "\tJournal End\n";
        return result;
    }
}
