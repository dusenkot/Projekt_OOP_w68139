using System;
namespace Projekt
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Classroom ReservedClassroom { get; set; }
    }
}

