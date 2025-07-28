namespace Blinko_5_minute.model
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }

        public Double Expire {  get; set; }
    }
}
