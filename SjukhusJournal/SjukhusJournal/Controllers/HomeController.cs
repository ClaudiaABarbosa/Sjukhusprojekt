using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SjukhusJournal.Models;

namespace SjukhusJournal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
