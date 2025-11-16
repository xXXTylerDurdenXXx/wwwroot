namespace CoursePaper.Models
{
    using Microsoft.EntityFrameworkCore;
    public class UserDBContext : DbContext
    {
        DbSet<User> Users { get; set; }

        public UserDBContext(DbContextOptions<UserDBContext> options) :  base(options) { }
    }
}
