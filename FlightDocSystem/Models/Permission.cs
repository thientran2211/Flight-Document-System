using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlightDocSystem.Models
{
    public class Permission
    {
        public int PermissionID { get; set; }
        public string? PermissionName { get; set; }


        [JsonIgnore]
        public Group? Group { get; set; }
    }
}
