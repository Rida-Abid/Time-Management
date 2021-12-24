namespace TTMS.Models
{
    public class TeacherViewModel
    {
        public int TeacherID { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string FullName => FirstName + " " + Surname;

    }
}
