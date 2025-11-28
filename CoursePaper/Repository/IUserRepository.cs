using CoursePaper.Models;

namespace CoursePaper.Repository
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        User AddUser(User user);
        bool DeleteUser(int id);
        User UpdateUser(int id, User user);
        User ExistUser(string loginOrEmail);

    }
}
