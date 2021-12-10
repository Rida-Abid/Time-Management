using Microsoft.AspNetCore.Mvc;

namespace TTMS.Controllers
{
    public class ClassController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
