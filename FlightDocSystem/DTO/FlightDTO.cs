using FlightDocSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace FlightDocSystem.DTO
{
    public class FlightDTO
    {
        public string? FlightNo { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DepartureTime { get; set; }
        public string? PointOfLoading { get; set; }
        public string? PointOfUnloading { get; set; }
        public string? Route { get; set; }
    }
}
