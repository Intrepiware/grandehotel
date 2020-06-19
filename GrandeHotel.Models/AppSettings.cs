using System;
using System.Collections.Generic;
using System.Text;

namespace GrandeHotel.Models
{
    public class AppSettings
    {
        public AppSettingsJwt Jwt { get; set; }
    }

    public class AppSettingsJwt
    {
        public string Secret { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
