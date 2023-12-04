using FlightDocSystem.Models;

namespace FlightDocSystem.DTO
{
    public class FlightDTO
    {
        public int FlightID { get; set; }
        public string FlightName { get; set; } = string.Empty;
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public string Route { get; set; } = string.Empty;
        public int UserID { get; set; }
        public User User { get; set; }
        public ICollection<Document> documents { get; set; }
    }
}
