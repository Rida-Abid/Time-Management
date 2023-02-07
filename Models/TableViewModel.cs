namespace TTMS.Models
{
    public class TableViewModel
    {
        public IEnumerable<Table> Tables { get; set; }

        

    }

    public class Table
    {
        public int LessonID { get; set; }

        public int TeacherID { get; set; }

        public int RoomID { get; set; }

        public int SubjectID { get; set; }

    }
}
