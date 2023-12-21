using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlightDocSystem.Models
{
    public class Document
    {
        public int DocumentID { get; set; }
        public string? DocumentName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public double Version { get; set; }
        public string? Note { get; set; }
        public string? Creator {  get; set; }
        public int DocTypeId { get; set; }
        public int FlightId { get; set; }

        [JsonIgnore]
        public Flight? Flight { get; set; }
        [JsonIgnore]
        public DocType? DocType { get; set; }
    }
}
