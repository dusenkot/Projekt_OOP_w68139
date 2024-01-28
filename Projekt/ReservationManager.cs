using System;

namespace Projekt
{
    public class ReservationManager
    {
        private List<Classroom> classrooms;
        private List<Reservation> reservations;
        private DatabaseManager databaseManager;

        public ReservationManager()
        {
            databaseManager = new DatabaseManager();
            classrooms = databaseManager.LoadClassrooms();
            reservations = databaseManager.LoadReservations();
        }

        public void AddClassroom(Classroom classroom)
        {
            classrooms.Add(classroom);
            databaseManager.SaveClassrooms(classrooms);
        }

        public void ReserveClassroom(Classroom classroom, DateTime startTime, DateTime endTime)
        {
            if (!classroom.IsAvailable)
            {
                Console.WriteLine("Classroom is already reserved for the given time.");
                return;
            }

            Reservation reservation = new Reservation
            {
                Id = reservations.Count + 1,
                StartTime = startTime,
                EndTime = endTime,
                ReservedClassroom = classroom
            };

            reservations.Add(reservation);
            classroom.IsAvailable = false;

            databaseManager.SaveReservations(reservations);
            Console.WriteLine($"Classroom '{classroom.Name}' reserved from {startTime} to {endTime}.");
        }

        public void DisplayReservations()
        {
            foreach (var reservation in reservations)
            {
                Console.WriteLine($"Reservation {reservation.Id}: Classroom '{reservation.ReservedClassroom.Name}', Time: {reservation.StartTime} - {reservation.EndTime}");
            }
        }

        public Classroom GetClassroomById(int classroomId)
        {
            return classrooms.FirstOrDefault(c => c.Id == classroomId);
        }

        public void RunReservationSystem()
        {
            Console.WriteLine("Welcome to the Reservation System!");
            DisplayAllClassrooms();
            // Display existing reservation

            // Read classroom ID from the console
            int classroomId = ReadClassroomIdFromConsole();
            Classroom selectedClassroom = GetClassroomById(classroomId);

            if (selectedClassroom != null)
            {
                // Read reservation time from the console
                DateTime startTime = ReadReservationTimeFromConsole("start time");
                DateTime endTime = ReadReservationTimeFromConsole("end time");

                // Reserve the classroom
                ReserveClassroom(selectedClassroom, startTime, endTime);
            }
            else
            {
                Console.WriteLine("Invalid classroom ID. Exiting.");
            }

        }

        private int ReadClassroomIdFromConsole()
        {
            Console.Write("Enter Classroom ID to reserve: ");
            int classroomId;
            while (!int.TryParse(Console.ReadLine(), out classroomId))
            {
                Console.Write("Invalid input. Enter a valid Classroom ID: ");
            }
            return classroomId;
        }

        private DateTime ReadReservationTimeFromConsole(string timeType)
        {
            DateTime dateTime;
            Console.Write($"Enter {timeType} (yyyy-MM-dd HH:mm): ");
            while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out dateTime))
            {
                Console.Write($"Invalid input. Enter a valid {timeType} (yyyy-MM-dd HH:mm): ");
            }
            return dateTime;
        }
        public void DisplayAllClassrooms()
        {
            Console.WriteLine("All Classrooms:");
            foreach (var classroom in classrooms)
            {
                Console.WriteLine($"Classroom {classroom.Id}: {classroom.Name}, Availability: {(classroom.IsAvailable ? "Available" : "Occupied")}");
            }
        }
        
    }
}
