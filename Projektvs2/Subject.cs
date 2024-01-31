using System;

class Subject
{
    public string SubjectName { get; set; }
    public string Room { get; set; }
    public int GroupNumber { get; set; }
    public DateTime StartTime {get;set;}
    public DateTime EndTime {get;set;}
    public Teacher ReservedBy { get; set; }

    public virtual void DisplayDetails()
    {
        Console.WriteLine($"Subject: {SubjectName}, Room: {Room}, Group: {GroupNumber}");
    }
}
