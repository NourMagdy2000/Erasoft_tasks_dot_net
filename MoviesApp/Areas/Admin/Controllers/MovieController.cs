using Microsoft.AspNetCore.Mvc;

namespace MoviesApp.Areas.Admin.Controllers
{
    public class MovieController : Controller
    {

        Data_access.ApplicationDbContext  _dbContext =new Data_access.ApplicationDbContext();
        public IActionResult Index()
        {

            var movies=_dbContext.Movies.AsQueryable();
            return View(movies.AsEnumerable());
        }
    }
}
