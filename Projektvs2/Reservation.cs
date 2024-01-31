using System;
 class Reservation
{
        public Teacher ReservedBy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Subject ReservedSubject { get; set; }
        public string SubjectName { get; set; }
}
