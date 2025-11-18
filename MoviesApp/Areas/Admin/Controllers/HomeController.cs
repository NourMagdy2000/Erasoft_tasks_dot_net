using Microsoft.AspNetCore.Mvc;
using MoviesApp.Models;
using MoviesApp.Repos;
using MoviesApp.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MoviesApp.Areas.Admin.Controllers

{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IRepository<Cinema> _cinemaRepositiry;
        private readonly IRepository<Movie> _MovieRepositiry;
        private readonly IRepository<Category> _categoryRepositiry;
        private readonly IRepository<Actor> _actorRepositiry;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger , IRepository<Cinema> _cinemaRepositiry, IRepository<Category> _categoryRepositiry, IRepository<Movie> _MovieRepositiry, IRepository<Actor> _actorRepositiry)
        {
            _logger = logger;
            this._cinemaRepositiry = _cinemaRepositiry;
            this._categoryRepositiry = _categoryRepositiry;
            this._MovieRepositiry = _MovieRepositiry;
            this._actorRepositiry = _actorRepositiry;
        }

        public async Task<IActionResult> Index()
        {   
            int cinemas=   await _cinemaRepositiry.CountAsync();
            int actors=   await _actorRepositiry.CountAsync();
            int categories=   await _categoryRepositiry.CountAsync();
            int movies=   await _MovieRepositiry.CountAsync();

            return View(new HomeVM() { 
                ActorNumber=actors,
                CategoryNumber=categories,
                CinemaNumber=cinemas,
                MovieNumber= movies
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
