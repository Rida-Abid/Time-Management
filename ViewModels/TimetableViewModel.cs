using Microsoft.AspNetCore.Mvc.Rendering;

namespace TTMS.ViewModels
{
    public class TimetableViewModel
    {
        public List<SelectListItem> Timetables { get; set; }

        public int TimetableID { get; set; }

        public string Name { get; set; }


        //public int LessonID { get; set; }

        //public int TeacherID { get; set; }

        //public int ClassID { get; set; }

        //public int SubjectID { get; set; }



    }

}