using CoursePaper.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursePaper.Repository
{
    public class LeaderBoardRepository : ILeaderBoardRepository
    {
        private readonly UserDBContext _dbContext;

        public LeaderBoardRepository(UserDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<List<User>> GetTopUsersByPointsAsync(int take)
        {
            return await _dbContext.Users
                .OrderByDescending(u => u.Score)
                .Take(take)
                .ToListAsync();
        }
    }
}
