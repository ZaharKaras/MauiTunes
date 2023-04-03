//using AndroidX.Browser.Trusted;
using MauiTunes.Entities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTunes.Services
{
    public class SpotifyAccountService: ISpotifyAccountService
    {
        private readonly RestClient _client;
        
        public SpotifyAccountService(RestClient client)
        {
            _client = client;
        }

        public async Task<AuthorizationToken> GetToken(string clientId, string clientSecret)
        {
            var token = new AuthorizationToken();

            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", $"{clientId}");
            request.AddParameter("client_secret", $"{clientSecret}");

            var response = await _client.PostAsync(request);

            if (response.IsSuccessStatusCode)
            {
                token = JsonConvert.DeserializeObject<AuthorizationToken>(response.Content);
            } 

            return token;
        }
    }
}
