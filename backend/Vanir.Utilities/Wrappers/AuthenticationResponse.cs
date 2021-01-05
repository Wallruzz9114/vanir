namespace Vanir.Utilities.Wrappers
{
    public class AuthenticationResponse
    {
        public string TokenPath { get; set; }
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public string AuthType { get; set; }
        public int MinutesToExpiration { get; set; }
    }
}