using Microsoft.AspNetCore.Mvc;
using MoviesApp.Models;
using MoviesApp.Repos;
using MoviesApp.ViewModels;
using System.Threading.Tasks;

namespace MoviesApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class MovieController : Controller
    {

        IRepository<Movie> MovieRepository;
        IRepository<Category> CategoryRepository;
        IRepository<Movie_sub_imgs> movie_sub_imgsRepository;


        public MovieController(IRepository<Movie> MovieRepository, IRepository<Category>categoryRepository, IRepository<Movie_sub_imgs> movie_sub_imgsRepository) 
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
            

            return View(new AddMovieVM() { Categories=categories as List<Category>,Movie=new Movie()});
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

           var addedMovie= await MovieRepository.AddAsync(Movie);
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
        //[HttpGet]
        //public async Task<IActionResult> UpdateMovie(int id)
        //{
        //    var MovieInDB = await MovieRepository.GetOneAsync(c => c.Id == id, trackd: false);


        //    return View(MovieInDB);
        //}
        //[HttpPost]
        //public async Task<IActionResult> UpdateMovie(UpdateMovieVM updateMovieVM)
        //{

        //    var MovieInDB = await MovieRepository.GetOneAsync(c => c.Id == updateMovieVM.Id, trackd: false);



        //    if (!ModelState.IsValid)
        //    {
        //        TempData["Error"] = "Invalid Inputs";
        //        return View(updateMovieVM);
        //    }
        //    Movie updatedMovie = new Movie() { Id = updateMovieVM.Id, Name = updateMovieVM.Name, Nantionality = updateMovieVM.Nantionality };

        //    if (updateMovieVM.File is not null && updateMovieVM.File.Length > 0)
        //    {

        //        var FileName = Guid.NewGuid().ToString() + "-" + updateMovieVM.File.FileName;
        //        var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "files");

        //        if (!Directory.Exists(wwwRootPath))
        //        {
        //            Directory.CreateDirectory(wwwRootPath);
        //        }

        //        var FilePath = Path.Combine(wwwRootPath, FileName);
        //        using (var stream = System.IO.File.Create(FilePath))
        //        {
        //            updateMovieVM.File.CopyTo(stream);
        //        }
        //        updatedMovie.File = FileName;
        //        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\files\\", MovieInDB.File);

        //        if (System.IO.File.Exists(oldPath))
        //        {
        //            System.IO.File.Delete(oldPath);
        //        }

        //    }
        //    else { updatedMovie.File = MovieInDB.File; }

        //    MovieRepository.Update(updatedMovie);
        //    await MovieRepository.CommitAsync();
        //    return RedirectToAction(nameof(Index));
        //}

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
