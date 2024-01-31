using System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Choose your role:");
        Console.WriteLine("1. Student");
        Console.WriteLine("2. Teacher");

        string roleChoice = Console.ReadLine();

        if (roleChoice == "1")
        {
            // Створення та робота з об'єктом Student
            Student student = new Student { FirstName = "Alice", LastName = "Smith" };

            // Розклад студента
            Subject subject1 = new Subject { SubjectName = "Math", Room = "Room101", StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) };
            Subject subject2 = new Subject { SubjectName = "History", Room = "Room102", StartTime = DateTime.Now.AddHours(2), EndTime = DateTime.Now.AddHours(3) };

            // Реєстрація предметів
            student.RegisterSubject(subject1);
            student.RegisterSubject(subject2);

            // Збереження та завантаження розкладу
            student.SaveScheduleToFile("schedule.json");
            student.LoadScheduleFromFile("schedule.json");
            Console.WriteLine($"Welcome, {student.FirstName} {student.LastName}!");

            // Поки користувач не введе "exit", дозволяємо студенту виконувати дії
            student.ChooseAndRegisterSubjects();
        }
        else if (roleChoice == "2")
        {
            // Teacher section
            Teacher teacher = new Teacher();

            // Check if the teacher file doesn't exist, then create a new teacher and save to file
            if (!File.Exists("teacher.json"))
            {
                Console.WriteLine("No teacher file found. Creating a new teacher...");

                teacher.FirstName = "John";
                teacher.LastName = "Doe";

                // ... initialize lectures, laboratories, and projects ...
                teacher.LecturesTaught.Add(new Lecture { /*...*/ });
                teacher.LaboratoriesTaught.Add(new Laboratory { /*...*/ });
                teacher.ProjectsTaught.Add(new Project { /*...*/ });

                // Save teacher data to file
                teacher.SaveTeacherToFile("teacher.json");

                Console.WriteLine("New teacher created and saved to 'teacher.json'.");
            }
            Teacher teachers = new Teacher { FirstName = "John", LastName = "Doe" };
            Console.WriteLine($"Welcome, {teacher.FirstName} {teacher.LastName}!");
            // Поки користувач не введе "exit", дозволяємо викладачу виконувати дії

            while (true)
            {
                Console.WriteLine("Actions for teacher:");
                Console.WriteLine("1. View Schedule Teacher");
                Console.WriteLine("2. Reserve Room");
                Console.WriteLine("Type 'exit' to exit.");

                string actionChoice = Console.ReadLine();

                if (actionChoice == "1")
                {
                    teacher.ViewSchedule();
                }

                else if (actionChoice == "2")
                {

                    if (actionChoice == "2")
                    {
                        // Дозволяє викладачу резервувати кабінет для предмету
                        teacher.ReserveRoomForSubject();
                    }
                    else
                    {
                        Console.WriteLine("Invalid room number. Try again.");
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
        else
        {
            Console.WriteLine("Invalid choice. Exiting program.");
        }
        }
    } 

