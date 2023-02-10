using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TTMS.Models;
using TTMS.ViewModels;

namespace TTMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AccountController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Initial load of singin page
            // User is presented with this page to enter in their username/password
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AccountViewModel model)
        {
            // Called after signin button is clicked.
            // model variable contains email and password enter in the textboxes on the sigin in page

            // we make sure that model state is valid i.e 
            // email has been entered and its length is less than 20 characters
            // password has been entered and its length is less than 20 characters
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Email/password match
            if (model.EmailAddress == "admin@tms.com" && model.Password == "admin")
            {
                await CreateIdentityAndSignin(model.EmailAddress);
            }

            // Incorrect credentials so
            // Add appropriate error message to a property in ViewBag(special container which is accessible from cshtml pages)
            // So we can inform the user of incorrect email/password.

            // TODO: Add Error property to AccountViewModel class so we don't have to rely on mvc container properties
            // Instead we use strongly typed property in our models(i.e. classes)
            ViewBag.Error = "Incorrect username or password, please try again.";
            return View();
        }

        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "account");
        }

        private async Task CreateIdentityAndSignin(string emailAddress)
        {
            // create users claims 
            var claims = new List<Claim>
                {
                    // create *required* claims
                    new Claim(ClaimTypes.NameIdentifier.ToString(), "Admin"),
                    new Claim(ClaimTypes.Email.ToString(), emailAddress)
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                RedirectUri = "/Home/Index" // redirect to home controller on successful signin
            };

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                authProperties);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
