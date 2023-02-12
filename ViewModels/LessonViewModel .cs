namespace TTMS.ViewModels
{
    public class LessonViewModel
    {
        public IEnumerable<Lesson> Lessons { get; set; }

        

    }

    public class Lesson
    {
        public int LessonID { get; set; }

        public string LessonNo { get; set; }

        public decimal Duration { get; set; }
       


    }
}
