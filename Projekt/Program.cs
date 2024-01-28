using System;
namespace Projekt
{
    using System;

    namespace Projekt
    {
        class Program
        {
            static void Main(string[] args)
            {
                ReservationManager reservationManager = new ReservationManager();

                // Викликати метод запуску системи резервацій
                reservationManager.RunReservationSystem();

                // Вивести на екран інформацію про резервації після здійснення резервацій
                reservationManager.DisplayReservations();
            }
        }
    }

}

