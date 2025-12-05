using CoursePaper.Models;

namespace CoursePaper.Repository
{
    public class MarkerRepository : IMarkerRepository
    {
        private readonly UserDBContext _context;

        public MarkerRepository(UserDBContext context)
        {
            _context = context;
        }

        public Marker AddMarker(Marker marker)
        {
            
           
            _context.Markers.Add(marker);
            _context.SaveChanges();
            return marker;
        }

        public bool DeleteMarker(int id)
        {
            var marker = _context.Markers.FirstOrDefault(m => m.Id == id);
            if (marker == null) return false;

            _context.Markers.Remove(marker);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Marker> GetAllMarkers()
        {
            return _context.Markers.ToList();
        }

        public Marker GetMarkerById(int id)
        {
            return _context.Markers.FirstOrDefault(m => m.Id == id);
        }
    }
}
