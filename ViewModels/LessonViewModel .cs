using Microsoft.AspNetCore.Mvc.Rendering;

namespace TTMS.ViewModels
{
    public class LessonViewModel
    {
        public List<SelectListItem> Lessons { get; set; }
        public int LessonID { get; set; }

        public string LessonNo { get; set; }

        public decimal Duration { get; set; }




    }

    
}
