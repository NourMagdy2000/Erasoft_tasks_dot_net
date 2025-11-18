using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Data_access;
using MoviesApp.Models;
using MoviesApp.Repos;
using MoviesApp.ViewModels;
using System.Threading.Tasks;

namespace MoviesApp.Areas.Admin.Controllers
{

    [Area("Admin")]

    public class CinemaController : Controller
    {

        IRepository<Cinema> CinemaRepository;

        IRepository<Movie> MovieRepository;
        IRepository<Cinema_movies> cmRepository;
        public CinemaController(IRepository<Cinema> CinemaRepository, IRepository<Movie> MovieRepository, IRepository<Cinema_movies> cmRepository
)

        {
            this.cmRepository = cmRepository;
            this.MovieRepository = MovieRepository;
            this.CinemaRepository = CinemaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var Cinemas = await CinemaRepository.GetAsync();

            return View(Cinemas.AsEnumerable());
        }

        [HttpGet]
        public async Task<IActionResult> AddCinema()
        {

            var movies = await MovieRepository.GetAsync();

            return View( movies)
            ;
        }
        [HttpPost]
        public async Task<IActionResult> AddCinema(Cinema cinema ,List<int> SelectedMovies )
        {


            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid Inputs";
                return RedirectToAction (nameof(Index));
            }

           
            await CinemaRepository.AddAsync(cinema);
            await CinemaRepository.CommitAsync();
            if (SelectedMovies != null)
            {
                foreach (var movieId in SelectedMovies)
                {
                    var relation = new Cinema_movies
                    {
                        CinemaId = cinema.Id,
                        MovieId = movieId
                    };

                    await cmRepository.AddAsync(relation);
                }
                await cmRepository.CommitAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCinema(int id)
        {
            var CinemaInDB = await CinemaRepository.GetOneAsync(c => c.Id == id, includes: [m => m.Cinema_Movies], trackd: false);
           var movies = await MovieRepository.GetAsync();


            IEnumerable<Cinema_movies> cinmaMovies = await cmRepository.GetAsync(cm => cm.CinemaId == id);
            List<int> cinemaMoviesIds = new List<int>();
            foreach (var c in cinmaMovies)
            { 
            cinemaMoviesIds.Add(c.MovieId);
            
            
            }
            return View(new UpdateCinemaVM() {Cinema= CinemaInDB ,Movies=movies,CinemaMovies=cinemaMoviesIds});
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCinema(Cinema Cinema,List<int>SelectedMovies)
        {

            var CinemaInDB = await CinemaRepository.GetOneAsync(c => c.Id == Cinema.Id, trackd: true);



            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid Inputs";
                return View(Cinema);
            }
            CinemaInDB.Name = Cinema.Name;
            await CinemaRepository.CommitAsync();
         var record_cinema_Movies= await cmRepository.GetAsync(cm => cm.CinemaId == Cinema.Id);
            foreach (var record in record_cinema_Movies)
            {
                cmRepository.Delete(record);
            }
            if (SelectedMovies != null)
            {
                foreach (var movieId in SelectedMovies)
                {
                    Cinema_movies relation = new Cinema_movies
                    {
                         CinemaId = Cinema.Id,
                        MovieId = movieId
                    };

                    await cmRepository.AddAsync(relation);
                }
                await cmRepository.CommitAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteCinema(int id)
        {


            var CinemaInDB = await CinemaRepository.GetOneAsync(c => c.Id == id, trackd: false);

            if (CinemaInDB != null)
            {

                CinemaRepository.Delete(CinemaInDB);
                await CinemaRepository.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
