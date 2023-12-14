using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlightDocSystem.Models
{
    [Table("Permissions")]
    public class Permission
    {
        [Key]
        public int PermissionID { get; set; }
        public string? PermissionName { get; set; }
        [JsonIgnore]
        public ICollection<Group>? Groups { get; set; }
    }
}
