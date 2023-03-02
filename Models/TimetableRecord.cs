
using System.ComponentModel.DataAnnotations.Schema;

namespace TTMS.Models
{
    public class TimetableRecord
    {
        public int TimetableID;
        public string  Teacher { get; set; }
        public string Subject { get; set; }
        public string Class { get; set; }
        public string Lesson { get; set; }
        public string Day { get; set; }
        public DateTime DateCreated;

    }

}
