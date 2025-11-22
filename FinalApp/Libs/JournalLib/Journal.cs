using System.Collections.Generic;
using CollectionLib;
using Lib;

namespace JournalLib;

public class Journal
{
    protected List<JournalEntry> entries;
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
            + $"Collection object information: \n{ObjectInfo}\n";
        }
    }
    public Journal()
    {
        entries = new List<JournalEntry>();
    }
    public virtual void CollectionCountChanged(object source, NewAssessmentTreeEventArgs args)
    {
        var objectInfo = args.Object?.ToString() ?? "null";
        entries.Add(new JournalEntry(args.Name, args.EventType, objectInfo));

    }
    public virtual void CollectionReferenceChanged(object source, NewAssessmentTreeEventArgs args)
    {
        var objectInfo = args.Object?.ToString() ?? "null";
        entries.Add(new JournalEntry(args.Name, args.EventType, objectInfo));
    }
    public override string ToString()
    {
        string result = "";
        foreach (JournalEntry entry in this.entries)
        {
            result += entry.ToString() + '\n';
        }
        return result;
    }
}
