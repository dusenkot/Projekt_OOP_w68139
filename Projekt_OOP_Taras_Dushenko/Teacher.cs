using Projekt_OOP_Taras_Dushenko;
using System.Linq;

public class Teacher : Person
{
    public List<Course> CoursesTaught { get; private set; } = new List<Course>();
    public List<Student> RegisteredStudents { get; private set; } = new List<Student>();
    public List<Reservation> Reservations { get; private set; } = new List<Reservation>();
    public List<Room> AvailableRooms { get; private set; } = new List<Room>();
    public ReservationManager ReservationManager { get; private set; } = new ReservationManager();



    public void ViewSchedule() => DisplayList("Courses you are teaching:", CoursesTaught.Select(course => course.CourseName));

    public void ViewRegisteredStudents() => DisplayList("Your registered students:", RegisteredStudents.Select(student => $"{student.FirstName} {student.LastName} (ID: {student.Id})"));

    public void ViewAllReservations() => DisplayList("Your reservations:", Reservations.Select(reservation => $"{reservation.ReservedCourse.CourseName}, Student: {reservation.ReservedFor.FirstName} {reservation.ReservedFor.LastName}, Start Time: {reservation.StartTime}, End Time: {reservation.EndTime}"));

    public void AddReservation(Course course, Person reservedFor, DateTime startTime, DateTime endTime, ReservationManager reservationManager)
    {
        Room reservedRoom = GetAvailableRoom(startTime, endTime);

        if (reservedRoom != null)
        {
            Reservation reservation = new Reservation
            {
                ReservedCourse = course,
                ReservedFor = reservedFor,
                StartTime = startTime,
                EndTime = endTime,
                
            };

            Reservations.Add(reservation);
            reservationManager.AddReservation(reservation);

            // Mark the room as reserved
            reservedRoom.IsReserved = true;

            Console.WriteLine($"Reservation added successfully for room {reservedRoom.RoomNumber}.");
        }
        else
        {
            Console.WriteLine("No available rooms. Reservation failed.");
        }
    }


    private Room GetAvailableRoom(DateTime startTime, DateTime endTime) => AvailableRooms.FirstOrDefault(room => !room.IsReserved);

    public void ReserveRoomOption(List<Room> rooms, Person currentUser, ReservationSystem reservationSystem)
    {
        AvailableRooms = rooms.Where(room => !room.IsReserved).ToList();

        Console.WriteLine("Available Rooms:");
        foreach (var room in rooms)
        {
            Console.WriteLine($"Room Number: {room.RoomNumber}, Status: {(room.IsReserved ? "Reserved" : "Available")}");
        }

        Console.Write("Enter the Room Number to reserve: ");
        if (int.TryParse(Console.ReadLine(), out int roomNumber))
        {
            Room selectedRoom = rooms.Find(r => r.RoomNumber == roomNumber);

            if (selectedRoom != null && !selectedRoom.IsReserved)
            {
                Console.Write("Enter reservation start time (format: yyyy-MM-dd HH:mm:ss): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
                {
                    Console.Write("Enter reservation end time (format: yyyy-MM-dd HH:mm:ss): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime endTime))
                    {
                        Console.Write("Enter the course name: ");
                        string courseName = Console.ReadLine();

                        Course course = new Course { CourseName = courseName };

                        ((Teacher)currentUser).AddReservation(course, currentUser, startTime, endTime, ReservationManager);

                        Console.WriteLine($"Reservation added for the room {selectedRoom.RoomNumber}.");
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
                Console.WriteLine("Invalid room number or the room is already reserved. Reservation failed.");
            }
        }
        else
        {
            Console.WriteLine("Invalid room number. Reservation failed.");
        }
    }

    private void DisplayList<T>(string header, IEnumerable<T> items)
    {
        Console.WriteLine(header);
        foreach (var item in items)
        {
            Console.WriteLine($"- {item}");
        }
    }

    internal void AddReservation(Reservation reservation)
    {
        throw new NotImplementedException();
    }
    public void RegisterStudent(string fullName)
    {
        // Implement the logic to register a student with the given full name
        // For example:
        Student newStudent = new Student
        {
            Id = (new Random()).Next(10000, 99999),
            FirstName = fullName.Split(' ')[0],
            LastName = fullName.Split(' ')[1]
        };

        // Add the new student to the list of registered students
        RegisteredStudents.Add(newStudent);

    }
}
