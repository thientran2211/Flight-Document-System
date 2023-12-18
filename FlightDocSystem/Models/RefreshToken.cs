namespace FlightDocSystem.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TokenHash { get; set; }
        public string TokenSalt { get; set; }
        public DateTime Ts {  get; set; }
        public DateTime ExpireDate { get; set; }
        public virtual User User { get; set; }
    }
}
