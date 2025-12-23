using CoursePaper.Models.DTO;

namespace CoursePaper.Service
{
    public interface IProfileService
    {
        UserProfileDTO GetProfile(int userId);
        void UpdateProfile(int userId, UpdateProfileDTO dto);
        void UpdateAvatar(int userId, string avatarPath);
    }
}
