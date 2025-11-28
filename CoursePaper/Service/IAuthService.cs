
using CoursePaper.Models.DTO;
using CoursePaper.Models;

namespace CoursePaper.Service
{

    public interface IAuthService
    {
        AuthResponse Login(LoginRequest loginRequest);
        AuthResponse Register(CreateUserRequest createUserRequest);
        AuthResponse RefreshToken(string refreshToken);
        bool ValidateToken(string token);
    }
}
