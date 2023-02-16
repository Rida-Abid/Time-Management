using Microsoft.AspNetCore.Mvc.Rendering;

namespace TTMS.ViewModels
{
    public class TimetableViewModel
    {
        public List<SelectListItem> Timetables { get; set; }

        public List<SelectListItem> Teachers { get; set; }

        public List<SelectListItem> Subjects { get; set; }

        public List<SelectListItem> Classes { get; set; }

        public List<SelectListItem> Lessons { get; set; }

        public List<SelectListItem> Days { get; set; }

        public int TimetableID { get; set; }

        public string Name { get; set; }

       




    }

}