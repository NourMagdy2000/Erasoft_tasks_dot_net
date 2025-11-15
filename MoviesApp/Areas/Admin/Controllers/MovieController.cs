using Microsoft.AspNetCore.Mvc;
using MoviesApp.Models;
using MoviesApp.Repos;
using MoviesApp.ViewModels;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MoviesApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class MovieController : Controller
    {

        IRepository<Movie> MovieRepository;
        IRepository<Category> CategoryRepository;
        IRepository<Movie_sub_imgs> movie_sub_imgsRepository;


        public MovieController(IRepository<Movie> MovieRepository, IRepository<Category> categoryRepository, IRepository<Movie_sub_imgs> movie_sub_imgsRepository)
        {
            this.MovieRepository = MovieRepository;
            this.CategoryRepository = categoryRepository;
            this.movie_sub_imgsRepository = movie_sub_imgsRepository;

        }

        public async Task<IActionResult> Index()
        {
            var Movies = await MovieRepository.GetAsync();

            return View(Movies.AsEnumerable());
        }

        [HttpGet]
        public async Task<IActionResult> AddMovie()
        {
            var categories = await CategoryRepository.GetAsync();


            return View(new AddMovieVM() { Categories = categories as List<Category>, Movie = new Movie() });
        }
        [HttpPost]
        public async Task<IActionResult> AddMovie(Movie Movie, IFormFile File, List<IFormFile> SubImages)
        {


            //if (!ModelState.IsValid)
            //{
            //    TempData["Error"] = "Invalid Inputs";
            //    return View(Movie);
            //}
            if (File is not null && File.Length > 0)
            {

                var FileName = Guid.NewGuid().ToString() + "-" + File.FileName;
                var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "files");

                if (!Directory.Exists(wwwRootPath))
                {
                    Directory.CreateDirectory(wwwRootPath);
                }

                var FilePath = Path.Combine(wwwRootPath, FileName);
                using (var stream = System.IO.File.Create(FilePath))
                {
                    File.CopyTo(stream);
                }
                Movie.Img = FileName;
            }

            var addedMovie = await MovieRepository.AddAsync(Movie);
            await MovieRepository.CommitAsync();
            if (SubImages is not null)
            {
                foreach (var item in SubImages)
                {
                    if (item.Length > 0)
                    {
                        //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                        var SubImageName = Guid.NewGuid().ToString() + "-" + item.FileName;
                        var SubImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\files\\Movie_sub_imgs", SubImageName);
                        using (var stream = System.IO.File.Create(SubImagePath))
                        {
                            item.CopyTo(stream);
                        }
                        var movieSubImage = new Movie_sub_imgs()
                        {
                            img = SubImageName,
                            Movieid = addedMovie.Entity.Id
                        };
                        //_context.movieSubImages.Add(movieSubImage);
                        await movie_sub_imgsRepository.AddAsync(movieSubImage);
                        //_context.SaveChanges();
                        await movie_sub_imgsRepository.CommitAsync();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> UpdateMovie(int id)
        {
            var MovieInDB = await MovieRepository.GetOneAsync(c => c.Id == id,includes:[p=>p.movie_Sub_Imgs], trackd: false);

            var categories = await CategoryRepository.GetAsync();
            ViewBag.CategoryId= MovieInDB.CategoryId;
            return View(new AddMovieVM() { Categories = categories as List<Category>, Movie =MovieInDB });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateMovie(Movie Movie, IFormFile File, List<IFormFile> SubImages)
        {

            var MovieInDB = await MovieRepository.GetOneAsync(c => c.Id == Movie.Id, trackd: false);



            //if (!ModelState.IsValid)
            //{
            //    TempData["Error"] = "Invalid Inputs";
            //    return View(Movie);
            //}
            Movie updatedMovie = new Movie() { Id = Movie.Id, Title = Movie.Title, Description = Movie.Description, Date = Movie.Date, CategoryId = Movie.CategoryId, Price = Movie.Price, Img = Movie.Img, Status=Movie.Status,};

            if (File is not null && File.Length > 0)
            {

                var FileName = Guid.NewGuid().ToString() + "-" + File.FileName;
                var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "files");

                if (!Directory.Exists(wwwRootPath))
                {
                    Directory.CreateDirectory(wwwRootPath);
                }

                var FilePath = Path.Combine(wwwRootPath, FileName);
                using (var stream = System.IO.File.Create(FilePath))
                {
                    File.CopyTo(stream);
                }
                updatedMovie.Img = FileName;
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\files\\", MovieInDB.Img);

                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

            }
            else { updatedMovie.Img = MovieInDB.Img; }


            MovieRepository.Update(updatedMovie);
            await MovieRepository.CommitAsync();

            if (SubImages is not null)
            {
                foreach (var item in SubImages)
                {
                    if (item.Length > 0)
                    {
                        var SubImageName = Guid.NewGuid().ToString() + "-" + item.FileName;
                        var SubImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\files\\Movie_sub_imgs", SubImageName);
                        using (var stream = System.IO.File.Create(SubImagePath))
                        {
                            item.CopyTo(stream);
                        }
                        var movieSubImage = new Movie_sub_imgs()
                        {
                            img = SubImageName,
                            Movieid = Movie.Id
                        };
                        //_context.movieSubImages.Add(movieSubImage);
                        await movie_sub_imgsRepository.AddAsync(movieSubImage);
                        //_context.SaveChanges();
                        await movie_sub_imgsRepository.CommitAsync();

                    }
                }

            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteSubImage(int movieId, string img)
        {
            //var movieSubImage = _context.movieSubImages.FirstOrDefault(p => p.movieId == movieId && p.Img == img);
            var movieSubImage = await movie_sub_imgsRepository.GetOneAsync(p => p.Movieid == movieId && p.img == img);

            if (movieSubImage is null)
                return RedirectToAction("NotFoundPage", "Home");
            //_context.movieSubImages.Remove(movieSubImage);
            movie_sub_imgsRepository.Delete(movieSubImage);
            await movie_sub_imgsRepository.CommitAsync();
            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\Movie_sub_imgs", movieSubImage.img);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            //_context.SaveChanges();
            await movie_sub_imgsRepository.CommitAsync();



            return RedirectToAction(nameof(UpdateMovie), new { id = movieId });

        }
        public async Task<IActionResult> DeleteMovie(int id)
        {
            //var movie = _context.movies.Include(p=>p.movieSubImages).FirstOrDefault(c => c.Id == id);
            var movie = await MovieRepository.GetOneAsync(c => c.Id == id, includes: [p => p.movie_Sub_Imgs]);
            if (movie is null)
                return RedirectToAction(nameof(Index));

            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\files", movie.Img);

            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
            if (movie.movie_Sub_Imgs.Count > 0)
            {
                foreach (var img in movie.movie_Sub_Imgs)
                {
                    var subImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\Movie_sub_imgs", img.img);
                    if (System.IO.File.Exists(subImgPath))
                    {
                        System.IO.File.Delete(subImgPath);
                    }
                }
            }
            //_context.movies.Remove(movie);
            MovieRepository.Delete(movie);
            //_context.SaveChanges();
            await MovieRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
