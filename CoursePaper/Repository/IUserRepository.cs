using CoursePaper.Models;

namespace CoursePaper.Repository
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        User AddUser(User user);
        bool DeleteUser(int id);
        User AddScoreUser(User user, int score);
        User RemoveScoreUser(User user);
        User UpdateUser( User user);
        User GetByEmail(string email);
        User ExistUser(string loginOrEmail);

    }
}
