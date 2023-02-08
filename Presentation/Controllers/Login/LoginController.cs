using DTO.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Service.Abstracts;
using System.Security.Claims;

namespace Presentation.Controllers.Login
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UserDTO dto)
        {
            try
            {
                _userService.Create(dto);
                return RedirectToAction("SignIn", "Login");
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(UserDTO dto)
        {
            try
            {
                dto = _userService.Login(dto);
                Authenticate(dto);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("SignIn", "Login");
        }

        private void Authenticate(UserDTO dto)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", dto.Id.ToString()),
                new Claim("FullName", dto.FullName),
                new Claim("Username", dto.Username),
                new Claim("Email", dto.Email),
                new Claim("BirthDate", dto.BirthDate.ToString("MM/dd/yyyy")),
                new Claim(ClaimTypes.Role, dto.RoleName),
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "ApplicationCookie");

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }
    }
}