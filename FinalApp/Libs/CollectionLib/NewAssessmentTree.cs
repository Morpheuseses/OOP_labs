using Lib;
using System.Xml.Serialization;

namespace CollectionLib;

[Serializable]
public class NewAssessmentTree : BinarySearchTree<Assessment>
{
    public event NewAssessmentTreeHandler? CollectionCountChanged;
    public event NewAssessmentTreeHandler? CollectionReferenceChanged;

    public string Name { get; set; }
    public NewAssessmentTree() : base()
    {
        Name = "Hehehoho tree";
    }
    public NewAssessmentTree(NewAssessmentTree btr) : base(btr)
    {
        Name = btr.Name;
    }
    public void OnCollectionCountChanged(object source, NewAssessmentTreeEventArgs args)
    {
        CollectionCountChanged?.Invoke(source, args);
    }
    public void OnCollectionReferenceChanged(object source, NewAssessmentTreeEventArgs args)
    {
        CollectionReferenceChanged?.Invoke(source, args);
    }
    public override void Add(Assessment data)
    {
        OnCollectionCountChanged(this, new NewAssessmentTreeEventArgs(Name, "inserted", data));
        base.Add(data);
    }
    public override void AddRange(Assessment[]  data)
    {
        base.AddRange(data);
    }
    public override bool Remove(Assessment data)
    {
        OnCollectionCountChanged(this, new NewAssessmentTreeEventArgs(Name, "deleted", data));
        return base.Remove(data);
    }
    public override void Clear()
    {
        OnCollectionCountChanged(this, new NewAssessmentTreeEventArgs(Name, "clear", null));
        base.Clear();
    }
}
