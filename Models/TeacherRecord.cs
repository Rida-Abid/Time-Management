
using System.ComponentModel.DataAnnotations.Schema;

namespace TTMS.Models
{
    public class TeacherRecord
    {
        public int TeacherID;
        public string Title;
        public string Firstname;
        public string Surname;
        public string Subject { get; set; }
        public string Email;
        public DateTime DateCreated;

    }

}
