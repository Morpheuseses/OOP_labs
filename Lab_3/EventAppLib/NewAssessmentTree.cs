using CollectionLib;
using Lib;

namespace EventAppLib;

public class NewAssessmentTree : BinarySearchTree<Assessment>
{
    public delegate void NewBinarySearhTreeHandler(object source, NewAssessmentTreeEventArgs args);
    public event NewBinarySearhTreeHandler? CollectionCountChanged;
    public event NewBinarySearhTreeHandler? CollectionReferenceChanged;

    public string Name { get; set; }
    public NewAssessmentTree() : base()
    {
        Name = "Hehehoho tree";
    }
    public NewAssessmentTree(int capacity) : base(capacity)
    {

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
    public override bool Remove(Assessment data)
    {
        OnCollectionCountChanged(this, new NewAssessmentTreeEventArgs(Name, "deleted", data));
        return base.Remove(data);
    }
}
