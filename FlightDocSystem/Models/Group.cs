using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;
using System.Text.Json.Serialization;

namespace FlightDocSystem.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public string? GroupName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Note {  get; set; }
        public int PermissionId { get; set; }
       
        [JsonIgnore]
        public Permission? Permission { get; set; }
        [JsonIgnore]
        public ICollection<User> Users { get; set; }
    }    
}
