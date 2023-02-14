using Microsoft.AspNetCore.Mvc.Rendering;

namespace TTMS.ViewModels
{
    public class ClassViewModel
    {
        public List<SelectListItem> Classes { get; set;}

        public int ClassID { get; set; }

        public string Name { get; set; }

    }

    
}
