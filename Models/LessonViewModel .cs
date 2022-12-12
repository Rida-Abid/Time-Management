namespace TTMS.Models
{
    public class LessonViewModel
    {
        public IEnumerable<Lesson> Lessons { get; set; }

        

    }

    public class Lesson
    {
        public int TeacherID { get; set; }

        public int RoomID { get; set; }

        public int SubjectID { get; set; }

        public DateTime Date { get; set; }


    }
}
