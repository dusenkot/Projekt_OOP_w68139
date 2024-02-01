class Room
{
    public int RoomNumber { get; set; }
    public bool IsReserved { get; set; }
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    public Reservation ReservedSubject { get; set; }

    public Room(int roomNumber)
    {
        RoomNumber = roomNumber;
        IsReserved = true;
    }
}
