using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace FlightDocSystem.Models
{
    public class User
    {
        public User() 
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PasswordSalt { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public int GroupId { get; set; }


        [JsonIgnore]
        public List<Group> Groups { get; } = new();
        [JsonIgnore]
        public Role? Role { get; set; }
        [JsonIgnore]
        public Setting? Setting { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }  
}
