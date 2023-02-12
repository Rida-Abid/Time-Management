using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTMS.ViewModels
{
    public class SubjectViewModel
    {

        public IEnumerable<Subject> Subjects { get; set; }
        
    }

    public class Subject
    {
        public int SubjectID { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
