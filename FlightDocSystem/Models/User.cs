using System.ComponentModel.DataAnnotations;
using System;
using System.Text.RegularExpressions;

namespace FlightDocSystem.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Enter Account Email")]
        [RegularExpression(@"^.*@vietjetair\.com$", ErrorMessage = "Email must belong to the domain name @vietjetair.com")]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int Phone { get; set; }
        public string? Role { get; set; }
        public Group? Group { get; set; }
        public ICollection<Setting> Settings { get; set; }
    }
}
