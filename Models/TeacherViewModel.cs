
namespace TTMS.Models
{
    public class TeacherViewModel
    {

        public int TeacherID { get; set; }

        public string Title { get; set; }
        
        public string Email { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string FullName => Firstname + " " + Surname;

    }
}
