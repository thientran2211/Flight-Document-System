using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlightDocSystem.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        /*[JsonIgnore]
        public ICollection<User> Users { get; set; }*/
    }
}
