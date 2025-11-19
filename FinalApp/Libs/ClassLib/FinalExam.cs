using System;
using System.Collections.Generic;

namespace Lib;

public enum GraduationLevel
{
    Bachelor = 1,
    Master,
    PhD
}

[Serializable]
public class FinalExam : Exam
{
    public GraduationLevel GraduationLevel { get; set; }
    public FinalExam() : base()
    {
        this.GraduationLevel = GraduationLevel.Bachelor;
    }
    public FinalExam(string title, DateTime date, int duration, int numberOfQuestions, int numberOfWrittenQuestions, GraduationLevel graduationLevel) : base(title, date, duration, numberOfQuestions, numberOfWrittenQuestions)
    {
        this.GraduationLevel = graduationLevel;
    }
    public FinalExam(FinalExam other) : base(other)
    {
        this.GraduationLevel = other.GraduationLevel;
    }
    protected override string GetFieldsString()
    {
        return base.GetFieldsString()
               + $"Graduation level: {this.GraduationLevel.ToString()}\n";
    }
    public override void Init()
    {
        base.Init();
        this.GraduationLevel = (GraduationLevel)Enum.Parse(typeof(GraduationLevel), Input.InputMessageString("Write down the graduation level(Bachelor, Master, PhD):"), ignoreCase: true);
    }
    public override void RandomInit()
    {
        base.RandomInit();
        var values = GraduationLevel.GetValues(typeof(GraduationLevel));
        this.GraduationLevel = (GraduationLevel)values.GetValue(rand.Next(values.Length))!;
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
                          + $"Number of written questions: {this.NumberOfWrittenQuestions}\n"
                          + $"Graduation level: {this.GraduationLevel.ToString()}"
        );
    }
    public override void ShowVirt()
    {
        Console.WriteLine(GetFieldsString());
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), this.GraduationLevel);
    }
    public override bool Equals(object? obj)
    {
        if (obj is not FinalExam other)
            return false;
        return base.Equals(obj) && this.GraduationLevel == other.GraduationLevel;
    }
    public override object ShallowCopy()
    {
        return (FinalExam)this.MemberwiseClone();
    }
    public override object Clone()
    {
        var newFinalExam = (FinalExam)this.MemberwiseClone();
        newFinalExam.Marks = new Dictionary<string, int?>(this.Marks);
        return newFinalExam;
    }
    public override string ToString()
    {
        return this.GetFieldsString();
    }
}
