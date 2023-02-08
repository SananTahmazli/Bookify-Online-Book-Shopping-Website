using Microsoft.AspNetCore.Mvc;
using Service.Abstracts;

namespace Presentation.Controllers.Profile
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Profile/Get/{id}")]
        public IActionResult Get(int id)
        {
            var result = _userService.Get(id);

            if (result == null)
            {
                ViewBag.Error = "Username is not found!";
                return View();
            }
            return View(result);
        }
    }
}