using Microsoft.AspNetCore.Mvc;
using MyNoteApp.Models;
using System.Diagnostics;

namespace MyNoteApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (Request.Cookies.ContainsKey("auth")) // if the user is already logged in to the system
            {
                return RedirectToAction("Index", "NotePage"); //redirects the user to the note page
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}