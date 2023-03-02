
using System.ComponentModel.DataAnnotations.Schema;

namespace TTMS.Models
{
    public class ClassRecord
    {
        public int ClassID { get; set; }
        public string Name { get; set; }

        public DateTime DateCreated;

    }

}
