using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TTMS.Models;

namespace TTMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : Controller
    {
        private readonly DataController db;
        public UpdateController()
        {
            db = new DataController();
        }

        [HttpPost]
        [Route("UpdateTimetableEntry")]
        public bool UpdateTimetableEntry(int teacherID, int daysID, int lessonID, int subjectID, int classID)
        {

            return db.UpdateTimetableEntry(teacherID, daysID, lessonID, subjectID, classID);

        }

       




    }
}
