using Microsoft.AspNetCore.Mvc;
using MoviesApp.Models;
using MoviesApp.Repos;
using MoviesApp.ViewModels;

namespace MoviesApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {

        IRepository<Category> CategoryRepository;



        public CategoryController(IRepository<Category> CategoryRepository)
        {
            this.CategoryRepository = CategoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var Categorys = await CategoryRepository.GetAsync();

            return View(Categorys.AsEnumerable());
        }

        [HttpGet]
        public IActionResult AddCategory()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category Category)
        {


            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid Inputs";
                return View(Category);
            }
         

            await CategoryRepository.AddAsync(Category);
            await CategoryRepository.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var CategoryInDB = await CategoryRepository.GetOneAsync(c => c.Id == id, trackd: false);


            return View(CategoryInDB);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(Category category)
        {

            var CategoryInDB = await CategoryRepository.GetOneAsync(c => c.Id == category.Id, trackd: false);



            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid Inputs";
                return View(category);
            }
            Category updatedCategory = new Category() { Id = category.Id, Name = category.Name };


            CategoryRepository.Update(updatedCategory);
            await CategoryRepository.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {


            var CategoryInDB = await CategoryRepository.GetOneAsync(c => c.Id == id, trackd: false);

            if (CategoryInDB != null)
            {

                CategoryRepository.Delete(CategoryInDB);
                await CategoryRepository.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
