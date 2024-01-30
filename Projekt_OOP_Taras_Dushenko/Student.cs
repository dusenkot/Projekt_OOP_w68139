using System;
using System.Collections.Generic;

namespace Projekt_OOP_Taras_Dushenko
{
    public class Student : Person
    {
       
        public List<Subject> Subjects { get; private set; } = new List<Subject>();


        public void ViewSchedule()
        {
            Console.WriteLine("Your schedule:");
            foreach (var subject in Subjects)
            {
                Console.WriteLine($"- {subject.SubjectName}, Room: {subject.Room}, Time: {subject.StartTime} - {subject.EndTime}");
            }
        }




    }
}
