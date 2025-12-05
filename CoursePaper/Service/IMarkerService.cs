using CoursePaper.Models;
using CoursePaper.Models.DTO;

namespace CoursePaper.Service
{
    public interface IMarkerService
    {
        /// <summary>
        /// Получить маркер по ID
        /// </summary>
        Marker GetMarkerById(int id);

        /// <summary>
        /// Получить список всех маркеров
        /// </summary>
        IEnumerable<Marker> GetAllMarkers();

        /// <summary>
        /// Добавить новый маркер
        /// </summary>
        Marker AddMarker(MarkerCreateRequest marker);

        /// <summary>
        /// Удалить маркер по ID
        /// </summary>
        bool DeleteMarker(int id);
    }
}
