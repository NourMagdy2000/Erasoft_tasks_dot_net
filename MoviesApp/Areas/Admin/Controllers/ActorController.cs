using MoviesApp.Repos;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Data_access;
using MoviesApp.Models;
using System.Threading.Tasks;
using MoviesApp.ViewModels;

namespace MoviesApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ActorController : Controller
    {

        IRepository<Actor> actorRepository;



        public ActorController(IRepository<Actor> actorRepository)
        {
            this.actorRepository = actorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var actors = await actorRepository.GetAsync();

            return View(actors.AsEnumerable());
        }

        [HttpGet]
        public IActionResult AddActor()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddActor(Actor actor, IFormFile Img)
        {


            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid Inputs";
                return View(actor);
            }
            if (Img is not null && Img.Length > 0)
            {

                var ImgName = Guid.NewGuid().ToString() + "-" + Img.FileName;
                var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "files");

                if (!Directory.Exists(wwwRootPath))
                {
                    Directory.CreateDirectory(wwwRootPath);
                }

                var ImgPath = Path.Combine(wwwRootPath, ImgName);
                using (var stream = System.IO.File.Create(ImgPath))
                {
                    Img.CopyTo(stream);
                }
                actor.Img = ImgName;
            }

            await actorRepository.AddAsync(actor);
            await actorRepository.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task <IActionResult>  UpdateActor(int id)
        {
            var ActorInDB = await actorRepository.GetOneAsync(c => c.Id == id, trackd: false);


            return View(ActorInDB);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateActor(UpdateActorVM updateActorVM)
        {

            var ActorInDB = await actorRepository.GetOneAsync(c => c.Id == updateActorVM.Id, trackd: false);



            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid Inputs";
                return View(updateActorVM);
            }
            Actor updatedActor = new Actor() { Id=updateActorVM.Id, Name = updateActorVM.Name, Nantionality = updateActorVM.Nantionality };

            if (updateActorVM.File is not null && updateActorVM.File.Length > 0)
            {

                var ImgName = Guid.NewGuid().ToString() + "-" + updateActorVM.File.FileName;
                var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "files");

                if (!Directory.Exists(wwwRootPath))
                {
                    Directory.CreateDirectory(wwwRootPath);
                }

                var ImgPath = Path.Combine(wwwRootPath, ImgName);
                using (var stream = System.IO.File.Create(ImgPath))
                {
                    updateActorVM.File.CopyTo(stream);
                }
                updatedActor.Img = ImgName;
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\files\\", ActorInDB.Img);

                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

            }
            else {   updatedActor.Img = ActorInDB.Img; }
             
             actorRepository.Update(updatedActor);
            await actorRepository.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult>  DeleteActor(int id)
        {

            
            var ActorInDB = await actorRepository.GetOneAsync(c => c.Id == id, trackd: false);

            if (ActorInDB != null)
            {

                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\files\\", ActorInDB.Img);

                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                actorRepository.Delete(ActorInDB);
                await actorRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
