using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


class Teacher : Person
{
    public List<Lecture> LecturesTaught { get; set; }
    public List<Laboratory> LaboratoriesTaught { get; set; }
    public List<Project> ProjectsTaught { get; set; }

    public Teacher()
    {
        LecturesTaught = new List<Lecture>();
        LaboratoriesTaught = new List<Laboratory>();
        ProjectsTaught = new List<Project>();
        LecturesTaught.Add(new Lecture
        {
            SubjectName = "Math Lecture",
            Room = "Room101",
            GroupNumber = 1,
            StartTime = DateTime.Parse("2022-12-01 10:00:00"),
            EndTime = DateTime.Parse("2022-12-01 11:30:00")
        });

        LaboratoriesTaught.Add(new Laboratory
        {
            SubjectName = "Physics Lab",
            Room = "Room102",
            GroupNumber = 2,
            StartTime = DateTime.Parse("2022-12-01 13:00:00"),
            EndTime = DateTime.Parse("2022-12-01 15:00:00")
        });

        ProjectsTaught.Add(new Project
        {
            SubjectName = "Computer Science Project",
            Room = "Room103",
            GroupNumber = 3,
            StartTime = DateTime.Parse("2022-12-01 15:30:00"),
            EndTime = DateTime.Parse("2022-12-01 17:00:00")
        });
        
        
    }

    public void ReserveRoomForSubject()
    {
        Console.WriteLine("Enter the details for reserving a room for a subject:");

        Console.Write("Enter the subject name: ");
        string subjectName = Console.ReadLine();

        Console.Write("Enter the subject type (Lecture, Laboratory, or Project): ");
        string subjectTypeInput = Console.ReadLine();

        if (Enum.TryParse(subjectTypeInput, true, out SubjectType subjectType))
        {
            ViewAvailableRooms();

            Console.Write("Enter the room number: ");
            if (int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                Room roomToReserve = GetAvailableRoomByNumber(roomNumber);

                if (roomToReserve != null)
                {
                    Console.Write("Enter the start time: ");
                    DateTime startTime;
                    if (DateTime.TryParse(Console.ReadLine(), out startTime))
                    {
                        Console.Write("Enter the end time: ");
                        DateTime endTime;
                        if (DateTime.TryParse(Console.ReadLine(), out endTime))
                        {
                            Subject subject;

                            switch (subjectType)
                            {
                                case SubjectType.Lecture:
                                    subject = new Lecture
                                    {
                                        SubjectName = subjectName,
                                        Room = $"Room{roomNumber}",
                                        GroupNumber = 0,
                                        StartTime = startTime,
                                        EndTime = endTime
                                    };
                                    break;

                                case SubjectType.Laboratory:
                                    subject = new Laboratory
                                    {
                                        SubjectName = subjectName,
                                        Room = $"Room{roomNumber}",
                                        GroupNumber = 0,
                                        StartTime = startTime,
                                        EndTime = endTime
                                    };
                                    break;

                                case SubjectType.Project:
                                    subject = new Project
                                    {
                                        SubjectName = subjectName,
                                        Room = $"Room{roomNumber}",
                                        GroupNumber = 0,
                                        StartTime = startTime,
                                        EndTime = endTime
                                    };
                                    break;

                                default:
                                    Console.WriteLine("Invalid subject type. Reservation failed.");
                                    return;
                            }

                           
                            UpdateTeacherSchedule(subject);
                            SaveTeacherToFile("teacher.json");
                            Console.WriteLine($"Room {roomToReserve.RoomNumber} reserved successfully for subject '{subject.SubjectName}'.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid end time. Reservation failed.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid start time. Reservation failed.");
                    }
                }
                else
                {
                    Console.WriteLine($"Room {roomNumber} is not available for reservation.");
                }
            }
            else
            {
                Console.WriteLine("Invalid room number. Please enter a valid number.");
            }
        }
        else
        {
            Console.WriteLine("Invalid subject type. Reservation failed.");
        }
    }


    public void ViewSchedule()
    {
        Console.WriteLine("Schedule:");

        Console.WriteLine("Lectures Taught:");
        foreach (var lecture in LecturesTaught)
        {
            lecture.DisplayDetails();
        }

        Console.WriteLine("\nLaboratories Taught:");
        foreach (var laboratory in LaboratoriesTaught)
        {
            laboratory.DisplayDetails();
        }

        Console.WriteLine("\nProjects Taught:");
        foreach (var project in ProjectsTaught)
        {
            project.DisplayDetails();
        }
    }

    private void UpdateTeacherSchedule(Subject subject)
    {
        switch (subject)
        {
            case Lecture lecture:
                LecturesTaught.Add(lecture);
                break;

            case Laboratory laboratory:
                LaboratoriesTaught.Add(laboratory);
                break;

            case Project project:
                ProjectsTaught.Add(project);
                break;

            default:
                Console.WriteLine("Invalid subject type. Update schedule failed.");
                return;
        }
  
    }
  
    public void ViewAvailableRooms()
    {
        DataRepository dataRepository = new DataRepository();
        Console.WriteLine("Available Rooms:");
        foreach (var room in dataRepository.GetAllRooms().Where(room => !room.IsReserved))
        {
            Console.WriteLine($"Room {room.RoomNumber}");
        }
    }

    private Room GetAvailableRoomByNumber(int roomNumber)
    {
        DataRepository dataRepository = new DataRepository();
        return dataRepository.GetAllRooms().FirstOrDefault(room => room.RoomNumber == roomNumber && !room.IsReserved);
    }

    enum SubjectType
    {
        Lecture,
        Laboratory,
        Project
    }
    public void SaveTeacherToFile(string filePath)
    {
        var teacherData = new
        {
            FirstName,
            LastName,
            LecturesTaught,
            LaboratoriesTaught,
            ProjectsTaught
        };

        string json = Newtonsoft.Json.JsonConvert.SerializeObject(teacherData, Newtonsoft.Json.Formatting.Indented);

        File.WriteAllText(filePath, json);
    }


    public void LoadTeacherFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            var teacherData = Newtonsoft.Json.JsonConvert.DeserializeObject<Teacher>(json);

            FirstName = teacherData.FirstName;
            LastName = teacherData.LastName;
            LecturesTaught = teacherData.LecturesTaught;
            LaboratoriesTaught = teacherData.LaboratoriesTaught;
            ProjectsTaught = teacherData.ProjectsTaught;
        }
        else
        {
            Console.WriteLine($"Teacher file '{filePath}' not found. Creating a new teacher.");
        }
    }

}

