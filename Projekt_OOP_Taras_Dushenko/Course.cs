using System;
namespace Projekt_OOP_Taras_Dushenko
{
	public class Course
	{
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<Student> EnrolledStudents { get; set; } = new List<Student>();
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public string Room { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public static Course CreateCourse(int courseId, string courseName)
        {
            return new Course { CourseId = courseId, CourseName = courseName };
        }

    }

}


