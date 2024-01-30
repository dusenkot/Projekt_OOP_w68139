using Newtonsoft.Json;
using Projekt_OOP_Taras_Dushenko;

public class ReservationSystem
{
    private ReservationManager reservationManager;
    private Person currentUser;
    private List<Student> students;
    private List<Teacher> teachers;
    private List<Room> rooms;


    public ReservationSystem(Student student)
    {
        students = new List<Student>();
        teachers = new List<Teacher>();
        rooms = LoadRoomsFromFile("rooms.json");

        reservationManager = new ReservationManager();
        if (student != null)
        {
            currentUser = students.Find(s => s.Id == student.Id) ?? student;
        }

        currentUser = student != null ? students.Find(s => s.Id == student.Id) : null;
    }

    public ReservationSystem(Teacher teacher, bool isNewTeacher)
    {
        students = new List<Student>();
        teachers = new List<Teacher>();
        rooms = LoadRoomsFromFile("rooms.json");
        reservationManager = new ReservationManager();

        currentUser = teacher != null ? teachers.Find(t => t.Id == teacher.Id) : null;

        if (isNewTeacher)
        {
            RegisterTeacher();
        }
        else
        {
            teachers.Add(teacher);
        }
    }

    private void RegisterTeacher()
    {
        Teacher newTeacher = new Teacher
        {
            Id = (new Random()).Next(1000, 9999)
        };

        Console.Write("Enter your full name: ");
        string fullName = Console.ReadLine();

        if (!string.IsNullOrEmpty(fullName) && fullName.Split(' ').Length >= 2)
        {
            newTeacher.FirstName = fullName.Split(' ')[0];
            newTeacher.LastName = fullName.Split(' ')[1];

            teachers.Add(newTeacher);
            Console.WriteLine($"Teacher {newTeacher.FirstName} {newTeacher.LastName} registered successfully with ID {newTeacher.Id}.");

            // Save the registered teachers to a JSON file
            SaveTeachersToFile("teachers.json", teachers);

            currentUser = newTeacher;
        }

    }
    private void RegisterStudent(string fullName)
    {
        Student newStudent = new Student
        {
            Id = (new Random()).Next(10000, 99999)
        };

        if (!string.IsNullOrEmpty(fullName) && fullName.Split(' ').Length >= 2)
        {
            newStudent.FirstName = fullName.Split(' ')[0];
            newStudent.LastName = fullName.Split(' ')[1];

            students.Add(newStudent);
            Console.WriteLine($"Student {newStudent.FirstName} {newStudent.LastName} registered successfully with ID {newStudent.Id}.");

            // Save the registered students to a JSON file
            SaveStudentsToFile("students.json", students);

            // Set currentUser to the new student only if the current user is a student
            currentUser = currentUser is Student ? newStudent : currentUser;
        }
    }

    private void SaveTeachersToFile(string filePath, List<Teacher> teachers)
    {
        SaveToFile(filePath, teachers);
    }

    private void SaveStudentsToFile(string filePath, List<Student> students)
    {
        SaveToFile(filePath, students);
    }

    private void SaveToFile<T>(string filePath, List<T> items)
    {
        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
        File.WriteAllText(fullPath, JsonConvert.SerializeObject(items, Formatting.Indented));
    }
    public static List<Student> LoadStudentsFromFile(string filePath)
    {
        return File.Exists(filePath) ? JsonConvert.DeserializeObject<List<Student>>(File.ReadAllText(filePath)) : new List<Student>();
    }

    public void Run()
    {
        
        while (true)
        {
            
            if (currentUser == null) DisplayLoginMenu();

            if (currentUser is Student) ProcessStudentMenu();
            else if (currentUser is Teacher) ProcessTeacherMenu();
        }
    }

    private void DisplayLoginMenu()
    {
        Console.WriteLine("Login as:");
        Console.WriteLine("1. Student");
        Console.WriteLine("2. Teacher");
        Console.WriteLine("3. Logout");
        Console.Write("Enter your choice: ");

        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            switch (choice)
            {
                case 1:
                    LoginUser<Student>(students, s => new Student { Id = s }, "Student");
                    break;
                case 2:
                    LoginUser<Teacher>(teachers, t => new Teacher { Id = t }, "Teacher");
                    break;
                case 3:
                    Logout();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
    private void LoginUser<T>(List<T> userList, Func<int, T> createUser, string userType) where T : Person
    {
        Console.Write($"Enter {userType} ID: ");
        if (int.TryParse(Console.ReadLine(), out int userId))
        {
            if (typeof(T) == typeof(Student))
            {
                students = LoadStudentsFromFile("students.json"); // Load students from the file
                T user = students.Find(s => s.Id == userId) as T;
                currentUser = user ?? throw new Exception($"{userType} with ID {userId} not found.");
            }
            else if (typeof(T) == typeof(Teacher))
            {
                teachers = LoadTeachersFromFile("teachers.json"); // Load teachers from the file
                T user = teachers.Find(t => t.Id == userId) as T;
                currentUser = user ?? throw new Exception($"{userType} with ID {userId} not found.");
            }
        }
        else
        {
            Console.WriteLine($"Invalid input. Please enter a valid {userType} ID.");
            Environment.Exit(0);
        }
    }


    public static List<Teacher> LoadTeachersFromFile(string filePath)
    {
        return File.Exists(filePath) ? JsonConvert.DeserializeObject<List<Teacher>>(File.ReadAllText(filePath)) : new List<Teacher>();
    }


    private void ProcessStudentMenu()
    {
        Console.WriteLine("1. View Schedule");
        Console.WriteLine("2. Add Subject");
        Console.WriteLine("0. Logout");

        Console.Write("Enter your choice: ");
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            switch (choice)
            {
                case 1:
                    ViewStudentSchedule();
                    break;
                case 2:
                    AddSubjectToStudent((Student)currentUser);
                    break;
                case 0:
                    Logout();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }


    private void ProcessTeacherMenu()
    {
        Console.WriteLine("1. View Your Schedule");
        Console.WriteLine("2. Add Reservation");
        Console.WriteLine("3. View All Reservations");
        Console.WriteLine("4. Register Student");
        Console.WriteLine("5. View Registered Students");
        Console.WriteLine("6. Set School Class Schedule");
        Console.WriteLine("7.Check Room Availability");// New option for setting the schedule of school classes
        Console.WriteLine("0. Logout");

        Console.Write("Enter your choice: ");
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            switch (choice)
            {
                case 1:
                    ViewTeacherSchedule();
                    break;
                case 2:
                    ReserveRoomOption();
                    break;
                case 3:
                    ViewAllReservations();
                    break;
                case 4:
                    RegisterStudentByTeacher();
                    break;
                case 5:
                    ViewRegisteredStudents();
                    break;
                case 6:
                    SetSchoolClassSchedule(); // New option for setting the schedule of school classes
                    break;
                case 7:
                    CheckRoomAvailability();
                    break;
                case 0:
                    Logout();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    private void ViewStudentSchedule() => ((Student)currentUser).ViewSchedule();

    private void ViewTeacherSchedule() => ((Teacher)currentUser).ViewSchedule();

    private void ViewAllReservations() => ((Teacher)currentUser).ViewAllReservations();

    private void ViewRegisteredStudents()
    {
        if (currentUser is Teacher teacher)
        {
            teacher.ViewRegisteredStudents();
        }
        else
        {
            Console.WriteLine("You must be logged in as a teacher to view registered students.");
        }
    }
    private void CheckRoomAvailability()
    {
        List<Room> availableRooms = GetAvailableRooms();
        if (availableRooms.Count > 0)
        {
            Console.WriteLine("Available Rooms:");
            foreach (var room in availableRooms)
            {
                Console.WriteLine($"Room Number: {room.RoomNumber}, Status: Available");
            }
        }
        else
        {
            Console.WriteLine("No available rooms.");
        }
    }
    private List<Room> GetAvailableRooms()
    {
        DateTime currentTime = DateTime.Now;

        return rooms
            .Where(room => !room.IsReserved || room.Reservations.Any(reservation => reservation.EndTime < currentTime))
            .ToList();
    }

    private void ReserveRoomOption()
    {
        List<Room> availableRooms = GetAvailableRooms();
        if (availableRooms.Count > 0)
        {
            ((Teacher)currentUser).ReserveRoomOption(availableRooms, currentUser, this);
        }
        else
        {
            Console.WriteLine("No available rooms. Reservation failed.");
        }
    }


    private void Logout()
    {
        currentUser = null;
        Console.WriteLine("Logged out successfully.");
        DisplayLoginMenu();
    }


    public static void CreateRoomsFile(List<Room> rooms, string filePath)
    {
        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
        File.WriteAllText(fullPath, JsonConvert.SerializeObject(rooms, Formatting.Indented));
    }

    public static List<Room> LoadRoomsFromFile(string filePath)
    {
        return File.Exists(filePath) ? JsonConvert.DeserializeObject<List<Room>>(File.ReadAllText(filePath)) : new List<Room>();
    }

    private void UpdateRoomStatus(List<Room> rooms, int roomNumber, bool isReserved, string filePath)
    {
        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
        Room roomToUpdate = rooms.Find(r => r.RoomNumber == roomNumber);

        if (roomToUpdate != null)
        {
            roomToUpdate.IsReserved = isReserved;
            CreateRoomsFile(rooms, filePath);
            UpdateReservationsFile(); // You may need to update the reservations file here as well
        }
        else
        {
            Console.WriteLine($"Room {roomNumber} not found.");
        }
    }

    private void UpdateReservationsFile()
    {
        string filePath = "reservations.json"; // Specify your file path
        List<Reservation> allReservations = teachers.SelectMany(t => t.Reservations).ToList();
        File.WriteAllText(filePath, JsonConvert.SerializeObject(allReservations, Formatting.Indented));
    }
    private Room GetAvailableRoom()
    {
        // Implement logic to get an available room
        return rooms.FirstOrDefault(room => !room.IsReserved);
    }
    public void AddReservation(Course course, Person reservedFor, DateTime startTime, DateTime endTime)
    {
        List<Room> availableRooms = GetAvailableRooms();

        if (availableRooms.Count > 0)
        {
            Room reservedRoom = availableRooms.First();
            Reservation reservation = new Reservation
            {
                ReservedCourse = course,
                ReservedFor = reservedFor,
                StartTime = startTime,
                EndTime = endTime,
                ReservedRoom = reservedRoom
            };

            if (reservedFor is Teacher teacher)
            {
                teacher.AddReservation(reservation);
            }

            reservationManager.AddReservation(reservation);
            UpdateRoomStatus(rooms, reservedRoom.RoomNumber, true, "rooms.json");

            Console.WriteLine($"Reservation added successfully for room {reservedRoom.RoomNumber}.");
        }
        else
        {
            Console.WriteLine("No available rooms. Reservation failed.");
        }
    }
    private void RegisterStudentByTeacher()
    {
        if (currentUser is Teacher teacher)
        {
            Console.Write("Enter the full name of the student to register: ");
            string fullName = Console.ReadLine();

            // Call RegisterStudent method to register the new student
            RegisterStudent(fullName);
        }
        else
        {
            Console.WriteLine("You must be logged in as a teacher to register a student.");
        } }
        private void AddSubjectToStudent(Student student)
        {
            Console.Write("Enter subject name: ");
            string subjectName = Console.ReadLine();

            Console.Write("Enter room: ");
            string room = Console.ReadLine();

            Console.Write("Enter start time (format: yyyy-MM-dd HH:mm:ss): ");
            DateTime startTime;
            while (!DateTime.TryParse(Console.ReadLine(), out startTime))
            {
                Console.WriteLine("Invalid date format. Please try again.");
                Console.Write("Enter start time (format: yyyy-MM-dd HH:mm:ss): ");
            }

            Console.Write("Enter end time (format: yyyy-MM-dd HH:mm:ss): ");
            DateTime endTime;
            while (!DateTime.TryParse(Console.ReadLine(), out endTime))
            {
                Console.WriteLine("Invalid date format. Please try again.");
                Console.Write("Enter end time (format: yyyy-MM-dd HH:mm:ss): ");
            }

            Subject subject = new Subject
            {
                SubjectName = subjectName,
                Room = room,
                StartTime = startTime,
                EndTime = endTime
            };

            student.Subjects.Add(subject);
            Console.WriteLine($"Subject {subject.SubjectName} added successfully.");
        }
    private void SetSchoolClassSchedule()
    {
        Console.Write("Enter course name: ");
        string courseName = Console.ReadLine();

        Console.Write("Enter room: ");
        string room = Console.ReadLine();

        Console.Write("Enter start time (e.g., 'yyyy-MM-dd HH:mm:ss'): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
        {
            Console.Write("Enter end time (e.g., 'yyyy-MM-dd HH:mm:ss'): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime endTime))
            {
                Course course = new Course
                {
                    CourseName = courseName,
                    Room = room,
                    StartTime = startTime,
                    EndTime = endTime
                };

                // Add the course to the reservation system
                AddReservation(course, currentUser, course.StartTime, course.EndTime);

                Console.WriteLine($"School class schedule set for {course.CourseName} in {course.Room}.");
            }
            else
            {
                Console.WriteLine("Invalid end time format. Please use 'yyyy-MM-dd HH:mm:ss'.");
            }
        }
        else
        {
            Console.WriteLine("Invalid start time format. Please use 'yyyy-MM-dd HH:mm:ss'.");
        }
    }


}







