namespace WorkSphereAPI.Helpers
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = "";
        public string Issuer { get; set; } = "WorksphereAPI";
        public string Audience { get; set; } = "WorksphereUsers";
        public int ExpiryMinutes { get; set; } = 60;
    }

}
