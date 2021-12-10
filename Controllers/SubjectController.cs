using Microsoft.AspNetCore.Mvc;

namespace TTMS.Controllers
{
    public class SubjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
