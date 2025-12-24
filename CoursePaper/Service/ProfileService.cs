using CoursePaper.Models;
using CoursePaper.Models.DTO;
using CoursePaper.Repository;

namespace CoursePaper.Service
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;
        

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }
        public UserProfileDTO GetProfile(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            return new UserProfileDTO
            {
                Id = userId,
                Name = user.Name,
                Email = user.Email,
                AvatarPath = user.AvatarPath,
                Score = user.Score,
                
            };
        }

        public void UpdateAvatar(int userId, string avatarPath)
        {
            var user = _userRepository.GetUserById(userId);

            if (user == null)
                throw new Exception("Пользователь не найден");

            user.AvatarPath = avatarPath;

            _userRepository.UpdateUser(user);
        }

        public void UpdateProfile(int userId, UpdateProfileDTO dto)
        {
            var user = _userRepository.GetUserById(userId);

            if (user == null)
                throw new Exception("Пользователь не найден");

            user.Name = dto.Name;

            _userRepository.UpdateUser(user);
        }
    }
}
