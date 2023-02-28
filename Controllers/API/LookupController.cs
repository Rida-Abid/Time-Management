using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TTMS.Models;

namespace TTMS.Controllers.API
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
        public IEnumerable<SubjectRecord> GetSubjectsByTeacher(int Id)
        {
            
            return db.GetSubjectsByTeacherId(Id);
            
        }

        [HttpGet]
        public IEnumerable<ClassRecord> GetClassesByTeacher(int Id)
        {

            return db.GetClassesByTeacherId(Id);

        }




    }
}
