using Microsoft.AspNetCore.Mvc;

namespace SjukhusJournal.Controllers
{
    public class PatientListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
