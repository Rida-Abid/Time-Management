namespace TTMS.Models
{
    public class LessonViewModel
    {
        public IEnumerable<Lesson> Lessons { get; set; }

        

    }

    public class Lesson
    {
        public int LessonID { get; set; }

        public int LessonNo { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


    }
}
