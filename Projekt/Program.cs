using System;
namespace Projekt
{
    class Program
    {
        static void Main()
        {
            ReservationManager reservationManager = new ReservationManager();

            // Приклад додавання кабінетів
            reservationManager.AddClassroom(new Classroom { Id = 1, Name = "A" });
            reservationManager.AddClassroom(new Classroom { Id = 2, Name = "B" });

            // Приклад резервації
            reservationManager.ReserveClassroom(reservationManager.GetClassroomById(1), DateTime.Now, DateTime.Now.AddHours(2));
            reservationManager.ReserveClassroom(reservationManager.GetClassroomById(2), DateTime.Now.AddHours(1), DateTime.Now.AddHours(3));

            // Показ резервацій
            reservationManager.DisplayReservations();
        }
    }
}

