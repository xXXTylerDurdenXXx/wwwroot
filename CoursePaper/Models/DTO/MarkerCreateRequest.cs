using System.ComponentModel.DataAnnotations;

namespace CoursePaper.Models.DTO
{
    public class MarkerCreateRequest
    {
        [Required]
        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public double Longitude { get; set; }
    }
}
