using CoursePaper.Models;
using CoursePaper.Repository;

namespace CoursePaper.Service
{
    public class LeaderBoardService : ILeaderBoardService
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public LeaderBoardService(ILeaderBoardRepository leaderBoardRepository)
        {
            _leaderBoardRepository = leaderBoardRepository;
        }
        public async Task<List<LeaderBoardModel>> GetTopAsync(int take = 50)
        {
            var users = await _leaderBoardRepository.GetTopUsersByPointsAsync(take);

            return users.Select(u => new LeaderBoardModel
            {
                Id = u.Id,
                Name = u.Name,
                Score = u.Score,
            }).ToList();
        }
    }
}
