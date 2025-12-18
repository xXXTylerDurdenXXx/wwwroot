namespace CoursePaper.Repository
{
    using CoursePaper.Models;
    public interface ILeaderBoardRepository
    {
        Task<List<User>> GetTopUsersByPointsAsync(int take);    
    }
}
