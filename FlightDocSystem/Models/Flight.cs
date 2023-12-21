using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlightDocSystem.Models
{
    public class Flight
    {
        public int FlightID { get; set; }
        public string? FlightNo { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DepartureTime { get; set; }
        public string? PointOfLoading { get; set; }
        public string? PointOfUnloading {  get; set; }
        public string? Route { get; set; }

    }
}
