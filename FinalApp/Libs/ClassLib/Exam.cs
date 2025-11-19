using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Lib;

[Serializable]
public class Exam : Test
{
    int numberOfWrittenQuestions;
    public int NumberOfWrittenQuestions
    {
        get { return this.numberOfWrittenQuestions; }
        set
        {
            //if (value > 0 && value <= this.NumberOfQuestions)
                this.numberOfWrittenQuestions = value;
            //else
            //    throw new Exception($"{this.GetType().Name}'s number of written questions should be more than 0 and less than number of all questions");
        }
    }
    public Exam() : base()
    {
        this.NumberOfWrittenQuestions = 1;
    }

    public Exam(string title, DateTime date, int duration, int numberOfQuestions, int numberOfWrittenQuestions) : base(title, date, duration, numberOfQuestions)
    {
        this.NumberOfWrittenQuestions = numberOfWrittenQuestions;
    }
    //[JsonConstructor]
    //public Exam(string title, DateTime date, int duration, int numberOfQuestions, int numberOfWrittenQuestions)
    //{
    //    this.Title = title;
    //    this.Date = date;
    //    this.DurationSeconds = duration;
    //    this.NumberOfQuestions = numberOfQuestions;
    //    this.NumberOfWrittenQuestions = numberOfWrittenQuestions;
    //}
    public Exam(Exam other) : base(other)
    {
        this.NumberOfWrittenQuestions = other.NumberOfWrittenQuestions;
    }
    protected override string GetFieldsString()
    {
        return base.GetFieldsString()
               + $"Number of written questions: {this.NumberOfWrittenQuestions}\n";
    }
    public override void Init()
    {
        base.Init();
        this.NumberOfWrittenQuestions = Input.InputMessageInt("Write down the number of written questions");
    }
    public override void RandomInit()
    {
        base.RandomInit();
        this.NumberOfWrittenQuestions = rand.Next(1, this.NumberOfQuestions);
    }
    public new void Show()
    {
        Console.WriteLine(
                          "------------------------------------\n"
                          + $"Object Type: {this.GetType().Name}\n"
                          + $"Title: {this.Title}\n"
                          + $"Date: {this.Date}\n"
                          + $"Duration: {this.DurationSeconds / 3600}h, {this.DurationSeconds / 60 % 60}m, {this.DurationSeconds % 60}s\n"
                          + $"Number of questions: {this.NumberOfQuestions}\n"
                          + $"Number of written questions: {this.NumberOfWrittenQuestions}"
        );
    }
    public override void ShowVirt()
    {
        Console.WriteLine(GetFieldsString());
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), this.NumberOfQuestions);
    }
    public override bool Equals(object? obj)
    {
        if (obj is not Exam other)
            return false;
        return base.Equals(obj) && this.NumberOfWrittenQuestions == other.NumberOfWrittenQuestions;
    }
    //public int CompareTo(object? obj)
    //{
    //    if (obj == null)
    //        return 1;
    //    else
    //    {
    //        base.CompareTo(obj);
    //        var other = (Exam)obj;
    //        if (this.NumberOfWrittenQuestions > other.NumberOfWrittenQuestions)
    //            return 1;
    //        else if (this.NumberOfWrittenQuestions < other.NumberOfWrittenQuestions)
    //            return -1;
    //        return 0;
    //    }
    //}
    public override object ShallowCopy()
    {
        return (Exam)this.MemberwiseClone();
    }
    public override object Clone()
    {
        var newExam = (Exam)this.MemberwiseClone();
        newExam.Marks = new Dictionary<string, int?>(this.Marks);
        return newExam;
    }
    public override string ToString()
    {
        return this.GetFieldsString();
    }
}
