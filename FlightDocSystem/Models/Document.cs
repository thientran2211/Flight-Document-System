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
        public decimal? Version { get; set; }
        public string? Note { get; set; }
        public string? Creator { get; set; }
        public string? File {  get; set; }
        public int DocTypeId { get; set; }
        public int FlightId { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public string? UserNameUpdate { get; set; }

        [JsonIgnore]
        public Flight? Flight { get; set; }
        [JsonIgnore]
        public DocType? DocType { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public ICollection<DocumentHistory>? History { get; set; }
    }
}
