using CoursePaper.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoursePaper.Controllers
{
    [Authorize]
    public class LeaderBoardController : Controller
    {
        private readonly ILeaderBoardService _leaderBoardService;

        public LeaderBoardController(ILeaderBoardService leaderBoardService)
        {
            _leaderBoardService = leaderBoardService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var model = await _leaderBoardService.GetTopAsync(50);
            return View("LeaderBoard",model);
        }
    }
}
