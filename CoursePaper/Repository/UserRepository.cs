using CoursePaper.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursePaper.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDBContext _context;

        public UserRepository(UserDBContext context)
        {
            _context = context;
        }
        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u =>
            u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            else return false;
        }

        public User AddScoreUser(User user, int score)
        {
            var userr = _context.Users.FirstOrDefault(u =>
            u.Id == user.Id);
            if (userr != null)
            {
                userr.Score += score;
                _context.SaveChanges();
                return userr;
            }
            return user;
        }
       

        public User ExistUser(string loginOrEmail)
        {
            var user = _context.Users
               .FirstOrDefault(u => u.Email == loginOrEmail);


            return user;
        }

        public User GetUserById(int id)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u =>
                  u.Id == id);
            if (user != null)
                return user;
            else return null;
        }

        public User GetByEmail(string email)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == email);
            return user;
        }
        public User UpdateUser( User user)
        {
            var userr = _context.Users.FirstOrDefault(u =>
            u.Id == user.Id);
            if (userr != null)
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            return user;
        }

        User RemoveScoreUser(User user)
        {
            var userr = _context.Users.FirstOrDefault(u =>
            u.Id == user.Id);
            if (userr != null)
            {
                user.Score = 0;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            return user;
        }

        User IUserRepository.RemoveScoreUser(User user)
        {
            var userr = _context.Users.FirstOrDefault(u =>
             u.Id == user.Id);
            if (userr != null)
            {
                user.Score = 0;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            return user;
        }
    }
}
