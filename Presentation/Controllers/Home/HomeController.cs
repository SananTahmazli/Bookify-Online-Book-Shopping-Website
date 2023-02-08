using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Home
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}