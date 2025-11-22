using System.Collections.Generic;
using CollectionLib;

namespace JournalLib;
public class JournalTxt : Journal
{
    private string filePath {get; set;}

    private string PreviosEvents { get; set; }
    public JournalTxt(string filePath) : base()
    {
        this.filePath = filePath;
        PreviosEvents = File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
    }
    public void AppendToFile(JournalEntry entry)
    {
        var dir = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        using var writer = new StreamWriter(filePath, append: true);
        writer.WriteLine(entry.ToString());
    }
    public override void CollectionCountChanged(object source, NewAssessmentTreeEventArgs args)
    {
        var objectInfo = args.Object?.ToString() ?? "null";
        var entry = new JournalEntry(args.Name, args.EventType, objectInfo);
        entries.Add(entry);

        AppendToFile(entry);
    }
    public override void CollectionReferenceChanged(object source, NewAssessmentTreeEventArgs args)
    {
        var objectInfo = args.Object?.ToString() ?? "null";
        var entry = new JournalEntry(args.Name, args.EventType, objectInfo);
        entries.Add(entry);

        AppendToFile(entry);
    }
    public override string ToString()
    {
        string result = PreviosEvents + "\n";
        foreach (JournalEntry entry in this.entries)
        {
            result += entry.ToString() + '\n';
        }
        return result;
    }
}