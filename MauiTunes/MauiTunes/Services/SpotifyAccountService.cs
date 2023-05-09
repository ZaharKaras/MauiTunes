//using AndroidX.Browser.Trusted;
using MauiTunes.Entities;
using MauiTunes.Models;
using Newtonsoft.Json;
using RestSharp;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTunes.Services
{
    public class SpotifyAccountService: ISpotifyAccountService
    {
        private RestClient _client;
        public SpotifyAccountService()
        {
            _client = new RestClient("https://accounts.spotify.com/api/token");
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
