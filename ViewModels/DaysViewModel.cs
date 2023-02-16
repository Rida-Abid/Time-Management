using Microsoft.AspNetCore.Mvc.Rendering;

namespace TTMS.ViewModels
{
    public class DaysViewModel
    {
        public List<SelectListItem> Days { get; set; }
        public int DayID { get; set; }

        public string Name { get; set; }


    }

    
}
