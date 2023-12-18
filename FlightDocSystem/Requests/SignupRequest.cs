using System.ComponentModel.DataAnnotations;

namespace FlightDocSystem.Requests
{
    public class SignupRequest
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required, EmailAddress]
        [VietjetAirEmailAttribute(ErrorMessage = "Email must belong to domain @vietjetair.com")]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
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
