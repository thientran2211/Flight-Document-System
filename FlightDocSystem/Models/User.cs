using System.ComponentModel.DataAnnotations;
using System;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FlightDocSystem.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Phone { get; set; }
        public bool IsActive { get; set; } 

        [ForeignKey("Role")]
        public int RoleID { get; set; }
        [ForeignKey("Group")]
        public int GroupID { get; set; }
        [ForeignKey("Document")]
        [JsonIgnore]
        public int DocumentID { get; set; }

        [JsonIgnore]
        public Role Role { get; set; }
        [JsonIgnore]
        public Group Group { get; set; }

        [JsonIgnore]
        public Setting? Setting { get; set; }

        [JsonIgnore]
        public ICollection<Document>? Documents { get; set; }
    }  
}
