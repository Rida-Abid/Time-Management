
using Microsoft.AspNetCore.Mvc.Rendering;


namespace TTMS.ViewModels
{
    public class TeacherViewModel
    {
        
        public int TeacherID { get; set; }

        public string Title { get; set; }
        
        public string Email { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public IEnumerable<SelectListItem> Subjects { get; set; }

        public string FullName => Firstname + " " + Surname;

    }
}
