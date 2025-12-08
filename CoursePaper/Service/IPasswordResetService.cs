namespace CoursePaper.Service
{
    public interface IPasswordResetService
    {
        Task<bool> ResetPasswordAsync(string email);
    }
}
