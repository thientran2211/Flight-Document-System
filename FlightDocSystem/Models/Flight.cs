using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocSystem.Models
{
    [Table("Flight")]
    public class Flight
    {
        [Key]
        public int FlightID { get; set; }
        public string? FlightName { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public string? Route { get; set; }
    }
}
