using FlightDocSystem.Models;
using System.Text.Json.Serialization;

namespace FlightDocSystem.DTO
{
    public class DocumentDTO
    {
        public string? Note { get; set; }
        [JsonIgnore]
        public decimal Version { get; set; } = 1.0m;
        public int GroupId { get; set; }
        public int FlightID { get; set; }
        public int DocTypeId { get; set; }
        public int UserId { get; set; }
    }
}
