using Microsoft.AspNetCore.Mvc;
using MoviesApp.Data_access;
using MoviesApp.Models;

namespace MoviesApp.Areas.Admin.Controllers
{

    
    public class CinemaController : Controller
    {

        ApplicationDbContext _context=new ApplicationDbContext();
        public IActionResult Index()
        {

            var cinemas = _context.Cinemas.AsQueryable();

            return View(cinemas.AsEnumerable());
        }

        public IActionResult AddCinema(Cinema cinema)
        {

            var cinemas = _context.Cinemas.AsQueryable();

            return View(cinemas.AsEnumerable());
        }
    }
}
