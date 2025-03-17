using Microsoft.AspNetCore.Mvc;

namespace SjukhusJournal.Controllers
{
    public class JournalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detaljer()
        {
            return View();
        }
    }
}
