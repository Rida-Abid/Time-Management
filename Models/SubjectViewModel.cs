namespace TTMS.Models
{
    public class SubjectViewModel
    {

        public IEnumerable<Subject> Subjects { get; set; }

    }

    public class Subject
    {
        public int SubjectID { get; set; }

        public string Name { get; set; }

    }
}
