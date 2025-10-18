using HealthCare.Data;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Controllers
{
    public class ApointmentsController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public IActionResult Index(int id )
        {

            var apointment = _db.Apointments.Where(a => a.DoctorId == id).AsQueryable();
            if (apointment is not null)
            {    return View(apointment.AsEnumerable());
            }
            return NotFound();

        }
    }
}
