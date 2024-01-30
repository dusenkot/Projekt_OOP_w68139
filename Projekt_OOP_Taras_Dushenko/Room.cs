public class Room
{
    public int RoomNumber { get; set; }
    public bool IsReserved { get; set; }
    public string Name { get; set; } // Add this property
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();


    public Room(int roomNumber, string name)
    {
        RoomNumber = roomNumber;
        IsReserved = false;
        Name = name;
    }
}
