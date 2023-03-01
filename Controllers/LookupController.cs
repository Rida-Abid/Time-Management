using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TTMS.Models;

namespace TTMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : Controller
    {
        private readonly DataController db;
        public LookupController()
        {
            db = new DataController();
        }

        [HttpGet]
        [Route("GetSubjectsByTeacher")]
        public IEnumerable<SubjectRecord> GetSubjectsByTeacher(int Id)
        {

            return db.GetSubjectsByTeacherId(Id);

        }

        [HttpGet]
        [Route("GetClassesByTeacher")]

        public IEnumerable<ClassRecord> GetClassesByTeacher(int Id)
        {

            return db.GetClassesByTeacherId(Id);

        }




    }
}
