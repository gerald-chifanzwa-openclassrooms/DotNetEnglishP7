using System;

namespace WebApi.Domain
{
    public class AuthenticationOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan? TokenLifespan { get; set; }
    }
}
