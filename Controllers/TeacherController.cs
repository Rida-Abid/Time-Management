using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TTMS.Controllers
{
    public class TeacherController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        
        [Authorize]
        public IActionResult AddEdit()
        {
            return View();
        }
    }
}
