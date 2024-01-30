using Newtonsoft.Json;

public class ReservationManager
{
    private List<Reservation> reservations = new List<Reservation>();

    public void AddReservation(Reservation reservation)
    {
        reservations.Add(reservation);
        Console.WriteLine("Reservation added successfully.");
    }

    public void UpdateReservation(Reservation reservation, DateTime newStartTime)
    {
        reservation.StartTime = newStartTime;
        Console.WriteLine("Reservation updated successfully.");
    }

    public void DeleteReservation(int reservationId)
    {
        reservations.RemoveAll(r => r.ReservationId == reservationId);
        Console.WriteLine("Reservation deleted successfully.");
    }

    public List<Reservation> GetAllReservations() => reservations;

    public static void UpdateReservationsFile(List<Teacher> teachers)
    {
        string filePath = "reservations.json"; // Specify your file path
        List<Reservation> allReservations = teachers.SelectMany(t => t.Reservations).ToList();

        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        };

        File.WriteAllText(filePath, JsonConvert.SerializeObject(allReservations, settings));
    }
}
