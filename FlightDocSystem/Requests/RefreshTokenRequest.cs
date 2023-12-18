namespace FlightDocSystem.Requests
{
    public class RefreshTokenRequest
    {
        public int UserID { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
