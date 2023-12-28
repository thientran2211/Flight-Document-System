using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlightDocSystem.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public Theme Theme { get; set; }
        public string? LogoName { get; set; }
        public string? Logo { get; set; }
        public bool? Captcha { get; set; }
        public int UserID { get; set; }

        public User? User { get; set; }
    }

    public enum Theme
    {
        Default = 0,
        Dark = 1,
        Light = 2
    }
}
