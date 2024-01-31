using System;
using System.Collections.Generic;
using System.Linq;

class ClassSchedule
{
    private List<Subject> subjects;

    public ClassSchedule()
    {
        subjects = new List<Subject>();
    }

    public void AddSubject(Subject subject)
    {
        subjects.Add(subject);
    }

    public void DisplaySchedule()
    {
        Console.WriteLine("Class Schedule:");
        foreach (var subject in subjects)
        {
            subject.DisplayDetails();
        }
    }
}
