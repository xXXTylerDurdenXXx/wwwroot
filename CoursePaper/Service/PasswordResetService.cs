
using CoursePaper.Repository;
using System.Security.Cryptography;
using System.Text;

namespace CoursePaper.Service
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public PasswordResetService(IUserRepository userRepository, IEmailService emailService)
        {
            _emailService = emailService;
            _userRepository = userRepository;   
        }
        public async Task<bool>  ResetPasswordAsync(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null) 
                return false;
            var newPassword = GeneratePassword();
            user.PasswordHash = GetHashPassword(newPassword);

            _userRepository.UpdateUser(user);

            await _emailService.SendEmailAsync(email,
            "Восстановление пароля",
            $"Ваш новый пароль: <b>{newPassword}</b>");

            return true;
        }

        private string GeneratePassword(int length = 10)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var rnd = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        private string GetHashPassword(string password)
        {
            byte[] bytepass = Encoding.ASCII.GetBytes(password);
            var hashBytes = SHA256.HashData(bytepass);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
