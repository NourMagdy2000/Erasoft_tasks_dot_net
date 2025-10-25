using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using To_do_app.Data_access;

namespace To_do_app.Controllers
{
    public class TaskController : Controller
    {

        ApplicationDbContext _dbContext = new ApplicationDbContext();
        public IActionResult Index()
        {

            var tasks = _dbContext.Tasks.AsQueryable();

            return View(tasks.AsEnumerable());
        }
        [HttpGet]
        public IActionResult Add()
        {

            return View(new Models.Task());
        }
        [HttpPost]
        public IActionResult Add(Models.Task task, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file is not null && file.Length > 0)
                {

                    var fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "files");

                    if (!Directory.Exists(wwwRootPath))
                    {
                        Directory.CreateDirectory(wwwRootPath);
                    }

                    var filePath = Path.Combine(wwwRootPath, fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                    task.File = fileName;
                }
                _dbContext.Tasks.Add(task);
                _dbContext.SaveChanges(); return RedirectToAction(nameof(Index));
            }
            else return View( nameof(Add));

        }
        public RedirectToActionResult Delete(int id)
        {
            var task = _dbContext.Tasks.FirstOrDefault(t => t.Id == id);
            _dbContext.Tasks.Remove(task);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.TaskId = id;
            var task = _dbContext.Tasks.FirstOrDefault(t => t.Id == id);

            return View(task);
        }
        [HttpPost]
        public RedirectToActionResult Update(Models.Task task, IFormFile file)
        {
            var taskInDatabase = _dbContext.Tasks.AsNoTracking().FirstOrDefault(t => t.Id == task.Id);
            {
                if (file is not null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "files");

                    if (!Directory.Exists(wwwRootPath))
                    {
                        Directory.CreateDirectory(wwwRootPath);
                    }

                    var filePath = Path.Combine(wwwRootPath, fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                    task.File = fileName;
                    var oldFilePath = Path.Combine(wwwRootPath, taskInDatabase.File);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);

                    }
                }
                else
                {
                    task.File = taskInDatabase.File;
                }

                _dbContext.Tasks.Update(task);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

        }
        public IActionResult ViewTask(int id)
        {
            var task = _dbContext.Tasks.FirstOrDefault(t => t.Id == id);

            return View(task);
        }
    }
}
