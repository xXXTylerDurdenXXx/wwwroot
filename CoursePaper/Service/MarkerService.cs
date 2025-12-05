using CoursePaper.Models;
using CoursePaper.Models.DTO;
using CoursePaper.Repository;
using Microsoft.EntityFrameworkCore;

namespace CoursePaper.Service
{

    public class MarkerService : IMarkerService
    {
        private readonly UserDBContext _context;
        private readonly IMarkerRepository _repository;

        public MarkerService(UserDBContext context, IMarkerRepository markerRepository)
        {
            _context = context;
            _repository = markerRepository;
        }
        public Marker AddMarker(MarkerCreateRequest marker)
        {
            var duplicates = _repository
            .GetAllMarkers()
            .Where(m =>
                Math.Abs(m.Latitude - marker.Latitude) < 0.00001 &&
                Math.Abs(m.Longitude - marker.Longitude) < 0.00001);
            if (duplicates.Any())
                throw new Exception("Метка с такими координатами уже существует");

            var newMarker = new Marker
            {
                Latitude = marker.Latitude,
                Longitude = marker.Longitude,
            };
            return _repository.AddMarker(newMarker);
        }

        public bool DeleteMarker(int id)
        {
            var exists = _repository
                .GetAllMarkers()
                .Where(m => id == m.Id);
            if (exists.Any())
                return _repository.DeleteMarker(id);
            else
                throw new Exception("Метки с такими координатами не существует существует");

        }

        public IEnumerable<Marker> GetAllMarkers()
        {
            return _repository.GetAllMarkers();
        }

        public Marker GetMarkerById(int id)
        {
            return _repository.GetMarkerById(id);
        }
    }
}
