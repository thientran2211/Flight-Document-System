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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; } 

        [ForeignKey("Role")]
        [JsonIgnore]
        public int RoleID { get; set; }
        [ForeignKey("Group")]
        [JsonIgnore]
        public int GroupID { get; set; }
        [ForeignKey("Document")]
        [JsonIgnore]
        public int DocumentID { get; set; }

        /*[JsonIgnore]*/
        public Role? Role { get; set; }
        /*[JsonIgnore]*/
        public Group? Group { get; set; }

        [JsonIgnore]
        public Setting? Setting { get; set; }

        [JsonIgnore]
        public ICollection<Document>? Documents { get; set; }
    }  
}
