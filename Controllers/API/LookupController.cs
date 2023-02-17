using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TTMS.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly DataController db;
        public LookupController()
        {
            db = new DataController();
        }

        //[HttpGet]
        //public JsonResult Get(int Id)
        //{
        //    db.GetSubjectsByTeacherId(Id);
        //    return
        //}
        
        

        
    }
}
