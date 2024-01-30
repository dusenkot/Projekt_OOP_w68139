using Projekt_OOP_Taras_Dushenko;
public class Reservation
{
    public int ReservationId { get; set; }
    public Course ReservedCourse { get; set; }
    public Person ReservedFor { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Room ReservedRoom { get; set; }
}
