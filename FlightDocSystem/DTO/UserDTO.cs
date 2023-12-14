using System.ComponentModel.DataAnnotations;

namespace FlightDocSystem.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string? UserName { get; set; }

        [RegularExpression(@"^[0-9]{10,11}$", ErrorMessage = "Invalid phone number.")]
        public string? Phone { get; set; }

        [Required, EmailAddress]
        [VietjetAirEmailAttribute(ErrorMessage = "Email must belong to domain @vietjetair.com")]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int GroupID { get; set; }
        [Required]
        public int RoleID { get; set; }
    }

    public class VietjetAirEmailAttribute : ValidationAttribute
    {
        private const string AllowedDomain = "vietjetair.com";

        public override bool IsValid(object value)
        {
            if (value == null || !(value is string))
            {
                return true;
            }

            string email = (string)value;

            // Kiểm tra xem địa chỉ email có thuộc tên miền 'vietjetair.com' hay không
            if (email.EndsWith($"@{AllowedDomain}", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // Nếu không hợp lệ, trả về thông điệp lỗi
            ErrorMessage = "Email must belong to domain @vietjetair.com";
            return false;
        }
    }
}
