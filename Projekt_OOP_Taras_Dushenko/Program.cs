using System;
using Projekt_OOP_Taras_Dushenko;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Reservation System!");

        string role;
        do
        {
            Console.Write("Enter your role (student/teacher): ");
            role = Console.ReadLine().ToLower();
        } while (role != "student" && role != "teacher");

        int id;
        do
        {
            Console.Write("Enter your ID: ");
        } while (!int.TryParse(Console.ReadLine(), out id) || id <= 0);

        ReservationSystem reservationSystem;

        if (role == "student")
        {
            reservationSystem = new ReservationSystem(new Student { Id = id });
        }
        else // Assuming role can only be "student" or "teacher"
        {
            Console.Write("Are you a new teacher? (yes/no): ");
            bool isNewTeacher = Console.ReadLine().ToLower() == "yes";

            reservationSystem = new ReservationSystem(new Teacher { Id = isNewTeacher ? new Random().Next(1000, 9999) : id }, isNewTeacher);
        }

        reservationSystem.Run();
    }
}
