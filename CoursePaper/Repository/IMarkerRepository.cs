using CoursePaper.Models;

namespace CoursePaper.Repository
{
    public interface IMarkerRepository
    {
        Marker GetMarkerById(int id);
        IEnumerable<Marker> GetAllMarkers();
        Marker AddMarker(Marker user);
        bool DeleteMarker(int id);
    }
}
