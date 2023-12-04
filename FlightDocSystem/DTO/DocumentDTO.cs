using FlightDocSystem.Models;

namespace FlightDocSystem.DTO
{
    public class DocumentDTO
    {
        public int DocumentID { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal Version { get; set; }
        public string Note { get; set; } = string.Empty;

        public int FlightID { get; set; }
        public Flight? Flight { get; set; }
    }
}
