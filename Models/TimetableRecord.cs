
using System.ComponentModel.DataAnnotations.Schema;

namespace TTMS.Models
{
    public class TimetableRecord
    {
        public int TimetableID;
        public string Name;
        public int Teacher;
        public int Subject;
        public int Class;
        public int Lesson;
        public int Day;
        public DateTime DateCreated;

    }

}
