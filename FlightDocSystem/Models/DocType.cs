using System.Text.Json.Serialization;

namespace FlightDocSystem.Models
{
    public class DocType
    {
        public int Id { get; set; }
        public string? DocTypeName { get; set; }

        [JsonIgnore]
        public Document? Document { get; set; }
    }
}
