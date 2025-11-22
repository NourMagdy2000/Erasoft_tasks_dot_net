using Microsoft.AspNetCore.Mvc;

namespace MoviesApp.Areas.Identity.Controllers
{  [Area ("Identity")]
    public class AccountController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }


    }
}
