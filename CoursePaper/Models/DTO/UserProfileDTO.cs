namespace CoursePaper.Models.DTO
{
    public class UserProfileDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string? AvatarPath { get; set; }

        public int Score { get; set; }
    }
}
