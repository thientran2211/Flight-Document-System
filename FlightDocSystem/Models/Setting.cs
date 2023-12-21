using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlightDocSystem.Models
{
    [Table("Settings")]
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public int Theme { get; set; }
        public string? Logo { get; set; }
        public bool? Captcha { get; set; }
        public int UserID { get; set; }

        public User? User { get; set; }
    }
}
