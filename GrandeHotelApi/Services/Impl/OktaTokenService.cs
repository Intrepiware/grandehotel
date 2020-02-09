using GrandeHotelApi.Models.Auth;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GrandeHotelApi.Services.Impl
{
    public class OktaTokenService : ITokenService
    {
        OktaToken _token = new OktaToken();
        IOptions<OktaSettings> _oktaSettings;

        public OktaTokenService(IOptions<OktaSettings> oktaSettings)
        {
            _oktaSettings = oktaSettings;
        }
        public async Task<string> GetToken()
        {
            if(!_token.IsValidAndNotExpiring) _token = await GetNewAccessToken();

            return _token.AccessToken;
        }

        private async Task<OktaToken> GetNewAccessToken()
        {
            HttpClient client = new HttpClient();
            string client_id = _oktaSettings.Value.ClientId;
            string client_secret = _oktaSettings.Value.ClientSecret;
            byte[] clientCreds = System.Text.Encoding.UTF8.GetBytes($"{client_id}:{client_secret}");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", System.Convert.ToBase64String(clientCreds));

            Dictionary<string, string> postMessage = new Dictionary<string, string>();
            postMessage.Add("grant_type", "client_credentials");
            postMessage.Add("scope", "access_token");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _oktaSettings.Value.TokenUrl)
            {
                Content = new FormUrlEncodedContent(postMessage)
            };
            HttpResponseMessage response = await client.SendAsync(request);
            if(response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OktaToken>(json);
            }
            else
            {
                throw new ApplicationException("Unable to retrieve access token from Okta");
            }
        }
    }
}
