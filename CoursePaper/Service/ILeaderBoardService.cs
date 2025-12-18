namespace CoursePaper.Service


{
    using CoursePaper.Models;
    public interface ILeaderBoardService
    {
        Task<List<LeaderBoardModel>> GetTopAsync(int take = 50);
    }
}
