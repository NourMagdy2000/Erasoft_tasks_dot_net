using Microsoft.AspNetCore.Mvc;
using MoviesApp.Data_access;
using MoviesApp.Models;
using MoviesApp.Repos;

namespace MoviesApp.Areas.Admin.Controllers
{

    [Area("Admin")]

    public class CinemaController : Controller
    {

        IRepository<Cinema> CinemaRepository;



        public CinemaController(IRepository<Cinema> CinemaRepository)
        {
            this.CinemaRepository = CinemaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var Cinemas = await CinemaRepository.GetAsync();

            return View(Cinemas.AsEnumerable());
        }

        [HttpGet]
        public IActionResult AddCinema()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCinema(Cinema Cinema)
        {


            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid Inputs";
                return View(Cinema);
            }


            await CinemaRepository.AddAsync(Cinema);
            await CinemaRepository.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCinema(int id)
        {
            var CinemaInDB = await CinemaRepository.GetOneAsync(c => c.Id == id, trackd: false);


            return View(CinemaInDB);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCinema(Cinema Cinema)
        {

            var CinemaInDB = await CinemaRepository.GetOneAsync(c => c.Id == Cinema.Id, trackd: false);



            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid Inputs";
                return View(Cinema);
            }
            Cinema updatedCinema = new Cinema() { Id = Cinema.Id, Name = Cinema.Name };


            CinemaRepository.Update(updatedCinema);
            await CinemaRepository.CommitAsync();
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
