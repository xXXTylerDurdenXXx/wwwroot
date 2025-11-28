namespace CoursePaper.Models
{
    using Microsoft.EntityFrameworkCore;
    public class UserDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDBContext(DbContextOptions<UserDBContext> options) :  base(options) { }
    }
}
