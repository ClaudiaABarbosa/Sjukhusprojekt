using Microsoft.AspNetCore.Mvc;

namespace SjukhusJournal.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
