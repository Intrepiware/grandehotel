using Newtonsoft.Json;
using System;

namespace GrandeHotelApi.Models.Auth
{
    public class OktaToken
    {
        private int _expiresIn;

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn
        {
            get { return _expiresIn; }
            set { 
                _expiresIn = value;
                ExpiresAt = DateTime.UtcNow.AddSeconds(value);
            }
        }

        public DateTime ExpiresAt { get; private set; }
        

        public string Scope { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        public bool IsValidAndNotExpiring
        {
            get
            {
                return !string.IsNullOrWhiteSpace(AccessToken) && ExpiresAt > DateTime.UtcNow.AddSeconds(30);
            }
        }
    }
}
