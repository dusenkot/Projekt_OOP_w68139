class Student : Person
{
    public List<Subject> Subjects { get; private set; } = new List<Subject>();

    public void ViewSchedule()
    {
        Console.WriteLine("Your schedule:");
        foreach (var subject in Subjects)
        {
            subject.DisplayDetails();
        }
    }

    public void RegisterSubject(Subject subject)
    {
        Subjects.Add(subject);
        Console.WriteLine($"Subject '{subject.SubjectName}' registered successfully.");
    }

    public void SaveScheduleToFile(string filePath)
    {
        FileManager fileManager = new FileManager();
        fileManager.SaveToFile(filePath, Subjects);
        Console.WriteLine($"Schedule saved to {filePath}.");
    }
    public void LoadScheduleFromFile(string filePath)
    {
        FileManager fileManager = new FileManager();
        Subjects = fileManager.LoadFromFile<Subject>(filePath);
        Console.WriteLine($"Schedule loaded from {filePath}.");
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
                // Дозволяє студенту вибрати предмет для реєстрації
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

    // Перенесено за межі методу ChooseAndRegisterSubjects
    static List<Subject> GetAllSubjects()
    {
        DataRepository dataRepository = new DataRepository();
        return dataRepository.GetAllSubjects();
    }
}
