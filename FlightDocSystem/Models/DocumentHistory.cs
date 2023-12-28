using System.Text.Json.Serialization;

namespace FlightDocSystem.Models
{
    public class DocumentHistory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Version { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int DocumentID { get; set; }

        [JsonIgnore]
        public Document? Document { get; set; }
    }
}
