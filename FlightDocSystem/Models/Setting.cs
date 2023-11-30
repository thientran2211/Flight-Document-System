namespace FlightDocSystem.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public int Theme { get; set; }
        public string? Logo { get; set; }
        public bool? Captcha { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
