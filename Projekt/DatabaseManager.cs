using System;
namespace Projekt
{
    public class DatabaseManager
    {
        private const string ClassroomFilePath = "Classrooms.txt";
        private const string ReservationFilePath = "Reservations.txt";

        public List<Classroom> LoadClassrooms()
        {
            List<Classroom> classrooms = new List<Classroom>();

            try
            {
                if (File.Exists(ClassroomFilePath))
                {
                    var lines = File.ReadAllLines(ClassroomFilePath);
                    foreach (var line in lines)
                    {
                        var data = line.Split(',');
                        classrooms.Add(new Classroom { Id = int.Parse(data[0]), Name = data[1], IsAvailable = bool.Parse(data[2]) });
                    }
                }
            }
            catch (Exception ex)
            {
                // Обробка помилок завантаження даних
                Console.WriteLine($"Error loading classrooms: {ex.Message}");
            }

            return classrooms; // Повертаємо значення за замовчуванням або порожній список
        }


        public void SaveClassrooms(List<Classroom> classrooms)
        {
            try
            {
                var lines = classrooms.Select(c => $"{c.Id},{c.Name},{c.IsAvailable}");
                File.WriteAllLines(ClassroomFilePath, lines);
            }
            catch (Exception ex)
            {
                // Обробка помилок збереження даних
                Console.WriteLine($"Error saving classrooms: {ex.Message}");
            }
        }

        public List<Reservation> LoadReservations()
        {
            List<Reservation> reservations = new List<Reservation>();

            try
            {
                if (File.Exists(ReservationFilePath))
                {
                    var lines = File.ReadAllLines(ReservationFilePath);
                    foreach (var line in lines)
                    {
                        var data = line.Split(',');
                        reservations.Add(new Reservation
                        {
                            Id = int.Parse(data[0]),
                            StartTime = DateTime.Parse(data[1]),
                            EndTime = DateTime.Parse(data[2]),
                            ReservedClassroom = new Classroom { Id = int.Parse(data[3]) }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Обробка помилок завантаження даних
                Console.WriteLine($"Error loading reservations: {ex.Message}");
            }

            return reservations; // Повертаємо значення за замовчуванням або порожній список
        }


        public void SaveReservations(List<Reservation> reservations)
        {
            try
            {
                var lines = reservations.Select(r => $"{r.Id},{r.StartTime},{r.EndTime},{r.ReservedClassroom.Id}");
                File.WriteAllLines(ReservationFilePath, lines);
            }
            catch (Exception ex)
            {
                // Обробка помилок збереження даних
                Console.WriteLine($"Error saving reservations: {ex.Message}");
            }
        }
            public DatabaseManager()
        {
            Console.WriteLine($"Classroom file path: {Path.GetFullPath(ClassroomFilePath)}");
            Console.WriteLine($"Reservation file path: {Path.GetFullPath(ReservationFilePath)}");
        }

    }
}

