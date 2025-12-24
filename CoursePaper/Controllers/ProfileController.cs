using CoursePaper.Models.DTO;
using CoursePaper.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoursePaper.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IWebHostEnvironment _environment;

        public ProfileController(IProfileService profileService, IWebHostEnvironment environment)
        {
            _profileService = profileService;
            _environment = environment;
        }
        [Authorize]
        public IActionResult Index()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var profile = _profileService.GetProfile(userId);
            return View(profile);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Update(UpdateProfileDTO dto)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                var profile = _profileService.GetProfile(userId);
                profile.Name = dto.Name;
                
                return View("Index", profile);
            }

            _profileService.UpdateProfile(userId, dto);
            return RedirectToAction("Index");
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult UploadAvatar(IFormFile avatar)
        {
            if (avatar == null || avatar.Length == 0)
                return BadRequest("Файл не выбран");

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var currentProfile = _profileService.GetProfile(userId);

            if (!string.IsNullOrEmpty(currentProfile.AvatarPath) && !currentProfile.AvatarPath.Contains("default-avatar.png"))
            {
                string oldFilePath = Path.Combine(_environment.WebRootPath, currentProfile.AvatarPath.TrimStart('/'));

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

                string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "avatars");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string fileName = $"{userId}_{Guid.NewGuid()}{Path.GetExtension(avatar.FileName)}";
            string filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                avatar.CopyTo(stream);
            }

            
            string relativePath = $"/uploads/avatars/{fileName}";
            _profileService.UpdateAvatar(userId, relativePath);

            return RedirectToAction("Index");
        }
    }
}
