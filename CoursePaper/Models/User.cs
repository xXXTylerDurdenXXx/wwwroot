namespace CoursePaper.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Score { get; set; } = 0;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; set; }

        public bool IsActive { get; set; }

        public string? AvatarPath { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}
