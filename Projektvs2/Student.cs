using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


class Student : Person
{
    public int StudentId { get; set; }
    public List<Subject> Subjects { get; private set; } = new List<Subject>();

    public Student()
    {
        // Load student data from a file during instantiation
        LoadStudentFromFile("student.json");
    }

    public void ViewSchedule()
    {
        Console.WriteLine("Your schedule:");
        foreach (var subject in Subjects)
        {
            subject.DisplayDetails();
        }
    }
    public void LoadScheduleFromFile(string filePath)
    {
        FileManager fileManager = new FileManager();
        Subjects = fileManager.LoadFromFile<Subject>(filePath);
        Console.WriteLine($"Schedule loaded from {filePath}.");
    }

    public void RegisterSubject(Subject subject)
    {
        Subjects.Add(subject);
        Console.WriteLine($"Subject '{subject.SubjectName}' registered successfully.");
    }

    public void SaveScheduleToFile(string filePath)
    {
        FileManager fileManager = new FileManager();
        fileManager.SaveToFile<Subject>(filePath, Subjects);
        Console.WriteLine($"Schedule saved to {filePath}.");
    }

    public void ChooseAndRegisterSubjects()
    {
        while (true)
        {
            Console.WriteLine("Student Actions:");
            Console.WriteLine("1. View Schedule");
            Console.WriteLine("2. Register for Subject");
            Console.WriteLine("Type 'exit' to exit.");

            string actionChoice = Console.ReadLine();

            if (actionChoice == "1")
            {
                ViewSchedule();
            }
            else if (actionChoice == "2")
            {
                Console.WriteLine("Available Subjects:");
                foreach (var subject in GetAllSubjects())
                {
                    Console.WriteLine($"{subject.SubjectName}, Room: {subject.Room}, Group: {subject.GroupNumber}");
                }

                Console.Write("Enter the name of the subject you want to register for: ");
                string subjectNameToRegister = Console.ReadLine();

                Subject selectedSubject = GetAllSubjects().FirstOrDefault(s => s.SubjectName == subjectNameToRegister);

                if (selectedSubject != null)
                {
                    RegisterSubject(selectedSubject);
                    Console.WriteLine($"Successfully registered for '{selectedSubject.SubjectName}'.");
                }
                else
                {
                    Console.WriteLine("Invalid subject name. Registration failed.");
                }
            }
            else if (actionChoice.ToLower() == "exit")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Try again.");
            }
        }
    }

    static List<Subject> GetAllSubjects()
    {
        DataRepository dataRepository = new DataRepository();
        return dataRepository.GetAllSubjects();
    }

    public static void InitializeStudentFile(string filePath, Student student)
    {
        FileManager fileManager = new FileManager();
        // Wrap the student in a List<Student> before passing it to SaveToFile
        fileManager.SaveToFile<Student>(filePath, new List<Student> { student });
        Console.WriteLine($"New student data saved to {filePath}.");
    }

    public static Student CreateNewStudent()
    {
        // Create a new student with some initial data
        return new Student
        {
            StudentId = 1,
            FirstName = "Alice",
            LastName = "Smith"
        };
    }
    public void LoadStudentFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            FileManager fileManager = new FileManager();
            var loadedStudents = fileManager.LoadFromFile<List<Student>>(filePath);
        }
        else
        {
            Console.WriteLine($"Student file '{filePath}' not found. Creating a new student.");

            // Create a new student and save it to the file
            Student newStudent = CreateNewStudent();
            InitializeStudentFile(filePath, newStudent);
        }
    }

}
