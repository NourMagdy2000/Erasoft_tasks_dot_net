using HealthCare.Data;
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
        public IActionResult Add(int doctorId)
        {
            ViewBag.DoctorId = doctorId;
            return View();
         

        }

        [HttpPost]
        public RedirectToActionResult Add(int doctorId, ApointmentFields apointmentFields)
        {

            var doctor = _db.Doctors.FirstOrDefault(d => d.Id == doctorId);
            if (doctor is not null)
            {       
                var apointment = new HealthCare.Models.Apointment()
            {
                DoctorId = doctorId,
                PatientName = apointmentFields.PatientName,
                Date = apointmentFields.Date,
                Time = apointmentFields.Time
            };

            _db.Apointments.Add(apointment);
            _db.SaveChanges();

         }

            return RedirectToAction(nameof(Index));
        }

    }
}
