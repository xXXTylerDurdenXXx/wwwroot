using CoursePaper.Models.DTO;
using CoursePaper.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoursePaper.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        private readonly IMarkerService _markerService;

        public MapController(IMarkerService markerService)
        {
            _markerService = markerService;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View("Map");

        }

        [HttpGet]
        public IActionResult GetMarkers()
        { 
            var markers = _markerService.GetAllMarkers();
            return Json(markers);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddMarker([FromBody] MarkerCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var marker = _markerService.AddMarker(request);
                return Json(marker);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult DeleteMarker(int id)
        {
            var result = _markerService.DeleteMarker(id);
            return Ok(new {success = result });
        }
    }
}
