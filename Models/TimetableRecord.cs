
using System.ComponentModel.DataAnnotations.Schema;

namespace TTMS.Models
{
    public class TimetableRecord
    {
        public int TimetableID;
        public string Name;
        public int TeacherID;
        public int SubjectID;
        public int ClassID;
        public int LessonID;
        public int DayID;
        public DateTime DateCreated;

    }

}
