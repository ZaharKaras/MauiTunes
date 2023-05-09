using MauiTunes.Entities;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Android.Media;


namespace MauiTunes.Services
{
    public class SpotifyPlayer : ISpotifyPlayer
    {
        private RestClient _client;
        public SpotifyPlayer()
        {
            _client = new RestClient("https://api.spotify.com/v1");
        }

        public async Task<bool> PlayTrack(string trackId, AuthorizationToken token)
        {
            var url = "https://api.spotify.com/v1/me/player/play";
            var playLoad = new
            {
                uris = new[] { $"spotify:track:{trackId}" }
            };

            var request = new RestRequest(url, Method.Put);
            request.AddHeader("Authorization", $"Bearer {token.AccessToken}");
            request.AddJsonBody(playLoad);
            var response = await _client.ExecuteAsync(request);

            return response.IsSuccessStatusCode;
        }


    }
}
