using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TTMS.ViewModels
{
    public class SubjectViewModel
    {

        public List<SelectListItem> Subjects { get; set; }
        public int SubjectID { get; set; }

        public string Name { get; set; }

    }

    
}
