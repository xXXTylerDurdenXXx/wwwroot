using Microsoft.AspNetCore.Mvc;

namespace CoursePaper.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // TODO: логика проверки пользователя

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Register(string name, string email, string password)
        {
            if (ModelState.IsValid)
            {
                // регистрация пользователя
            }
            return RedirectToAction("Register", "Auth");
        }

        
        [HttpPost]
        public IActionResult Logout()
        {
            // выход пользователя
            return RedirectToAction("Login");
        }
    }
}
