using Microsoft.AspNetCore.Mvc;

namespace CoursePaper.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View("Map");
        }
    }
}
