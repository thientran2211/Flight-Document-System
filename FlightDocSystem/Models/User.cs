using System.ComponentModel.DataAnnotations;
using System;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FlightDocSystem.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public int Phone { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }
        [ForeignKey("Group")]
        public int GroupID { get; set; }
        [ForeignKey("Document")]
        public int DocumentID { get; set; }

        [JsonIgnore]
        public Role? Role { get; set; }
        [JsonIgnore]
        public Group? Group { get; set; }
        [JsonIgnore]
        public Setting? Setting { get; set; }

        [JsonIgnore]
        public ICollection<Document>? Documents { get; set; }
    }
}
