namespace MS.Identity.API.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public string Inssuer { get; set; }
        public string ValidIn { get; set; }
    }
}
