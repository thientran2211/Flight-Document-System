namespace FlightDocSystem.DTO
{
    public class SettingDTO
    {
        public int Theme { get; set; }
        public string? Logo { get; set; }
        public bool? Captcha { get; set; }
        public int UserID { get; set; }

        public RegisterDTO? userDTO { get; set; }
    }
}
