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
    }
}

