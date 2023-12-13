using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;
using System.Text.Json.Serialization;

namespace FlightDocSystem.Models
{
    [Table("Group")]
    public class Group
    {
        [Key]
        public int GroupID { get; set; }
        public string? GroupName { get; set; }
        public DateTime? CreateDate { get; set; }
        public int NumberOfUser { get; set; }


        public int PermissionID { get; set; }
        [JsonIgnore]
        public Permission? Permission { get; set; }
    }
}
