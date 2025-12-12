namespace CoursePaper.Models
{
    using Microsoft.EntityFrameworkCore;
    public class UserDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Marker> Markers { get; set; }
        public DbSet<Role> Roles { get; set; }

        public UserDBContext(DbContextOptions<UserDBContext> options) :  base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(
             new Role { Id = 1, Name = "User" },
             new Role { Id = 2, Name = "Admin" }
             );
            
        }
    }
}
