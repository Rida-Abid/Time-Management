
using System.ComponentModel.DataAnnotations.Schema;

namespace TTMS.Models
{
    public class TimetableRecord
    {
        public int TimetableID;
        public string  Teacher;
        public string Subject;
        public string Class;
        public string Lesson;
        public string Day;
        public DateTime DateCreated;

    }

}
