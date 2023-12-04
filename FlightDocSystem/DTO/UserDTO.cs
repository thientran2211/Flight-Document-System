using System.ComponentModel.DataAnnotations;

namespace FlightDocSystem.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Enter Account Email")]
        [RegularExpression(@"^.*@vietjetair\.com$", ErrorMessage = "Email must belong to the domain name @vietjetair.com")]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int Phone { get; set; }
        public string? Role { get; set; }
    }
}
