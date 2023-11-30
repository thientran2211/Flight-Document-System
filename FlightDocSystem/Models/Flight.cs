namespace FlightDocSystem.Models
{
    public class Flight
    {
        public int FlightID { get; set; }
        public string? FlightName { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public string? Route { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public ICollection<Document> documents { get; set; }
    }
}
