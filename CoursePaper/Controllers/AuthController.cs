using CoursePaper.Service;
using Microsoft.AspNetCore.Mvc;
using CoursePaper.Models;
using CoursePaper.Models.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoursePaper.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly IPasswordResetService _passwordResetService;

        public AuthController(IAuthService userService, ILogger<AuthController> logger, IPasswordResetService passwordResetService)
        {
            _authService = userService;
            _logger = logger;
            _passwordResetService = passwordResetService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginRequest request)
        {
            if (!ModelState.IsValid) 
                return View(request);
            var result = _authService.Login(request);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View(request);
            }
         

            _logger.LogInformation("Пользователь {Email} успешно прошёл аутентификацию.", request.Email);
            HttpContext.Session.SetString("AccessToken", result.Token);

            
            return RedirectToAction("Index", "Map");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new CreateUserRequest());
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(CreateUserRequest request)
        {
            
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest(errors);


            }
            

            var result = _authService.Register(request);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View(request);
            }

            HttpContext.Session.SetString("AccessToken", result.Token);

            return RedirectToAction("Login", "Auth");
        }

        
        [HttpPost]
        public IActionResult Logout()
        {
             HttpContext.Session.Remove("AccessToken");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            bool result = await _passwordResetService.ResetPasswordAsync(email);

            if (!result)
            {
                ModelState.AddModelError("", "Пользователь с таким email не найден");
                return View();
            }
            ViewBag.Message = "Новый пароль отправлен вам на email)";
            return View();
        }
    }
}
