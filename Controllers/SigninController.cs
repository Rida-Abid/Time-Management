using Microsoft.AspNetCore.Mvc;

namespace TTMS.Controllers
{
    public class SigninController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
