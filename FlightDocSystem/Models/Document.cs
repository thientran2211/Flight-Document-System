namespace FlightDocSystem.Models
{
    public class Document
    {
        public int DocumentID { get; set; }
        public string? DocumentName { get; set; }
        public string? Type { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal Version { get; set; }
        public string? Note { get; set; }
        public int FlightID { get; set; }

        public Flight? Flight { get; set; }
    }
}
