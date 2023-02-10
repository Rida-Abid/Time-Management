
using System.ComponentModel.DataAnnotations.Schema;

namespace TTMS.Models
{
    public class SubjectRecord
    {
        public int SubjectID { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated;

    }

}
