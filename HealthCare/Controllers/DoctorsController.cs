using HealthCare.Data;
using HealthCare.Models;
using HealthCare.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Controllers
{
    public class DoctorsController : Controller
    {

        ApplicationDbContext _db = new ApplicationDbContext();

        public IActionResult Index()
        {

            var doctors = _db.Doctors.Include(c=> c.Category).AsQueryable();
            return View(doctors.AsEnumerable());
        }
        [HttpGet]
        public IActionResult Add(int id)
        {
            ViewBag.DoctorId = id;
            return View();
         

        }

        [HttpPost]
        public RedirectToActionResult Add( Apointment apointment)
        {

            var doctor = _db.Doctors.FirstOrDefault(d => d.Id == apointment.DoctorId);
            if (doctor is not null)
            {       
                var apointment2 = new HealthCare.Models.Apointment()
            {
                DoctorId = apointment.DoctorId,
                PatientName = apointment.PatientName,
                Date = apointment.Date,
                Time = apointment.Time
            };

            _db.Apointments.Add(apointment2);
            _db.SaveChanges();

         }

            return RedirectToAction(nameof(Index));
        }

    }
}
