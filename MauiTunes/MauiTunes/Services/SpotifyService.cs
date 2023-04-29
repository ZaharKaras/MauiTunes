using MauiTunes.Entities;
using MauiTunes.Models;
using Newtonsoft.Json;
using RestSharp;


namespace MauiTunes.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly RestClient _client;
        public SpotifyService()
        {
            _client = new RestClient("https://accounts.spotify.com");
        }
        public async Task<IEnumerable<Album>> GetAlbums(string albumName, AuthorizationToken token)
        {
            var albums = new List<Album>();
            var request = new RestRequest("/v1/search");
            request.AddHeader("Authorization", $"{token.TokenType} {token.AccessToken}");
            request.AddParameter("q", albumName);
            request.AddParameter("type", "album");
            
            var response = await _client.GetAsync(request);

            if(response.IsSuccessStatusCode)
            {
                albums = JsonConvert.DeserializeObject<List<Album>>(response.Content);
            }

            return albums;
        }

        public async Task<SearchResult> GetArtist(string artistName, AuthorizationToken token)
        {
            var client = new RestClient("https://api.spotify.com/v1/search");
            client.AddDefaultHeader("Authorization", $"Bearer {token.AccessToken}");
            var request = new RestRequest($"?q={artistName}&type=artist", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var result = JsonConvert.DeserializeObject<SearchResult>(response.Content);
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<SearchResult> GetTracks(string trackName, AuthorizationToken token)
        {
            //var tracks = new List<Track>();
            //var request = new RestRequest("/v1/search");
            //request.AddHeader("Authorization", $"{token.TokenType} {token.AccessToken}");
            //request.AddParameter("q", trackName);
            //request.AddParameter("type", "track");

            //var response = await _client.GetAsync(request);

            //if (response.IsSuccessStatusCode)
            //{
            //    tracks = JsonConvert.DeserializeObject<List<Track>>(response.Content);
            //}

            //return tracks;

            var client = new RestClient("https://api.spotify.com/v1/search");
            client.AddDefaultHeader("Authorization", $"Bearer {token.AccessToken}");
            var request = new RestRequest($"?q={trackName}&type=track", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var result = JsonConvert.DeserializeObject<SearchResult>(response.Content);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
