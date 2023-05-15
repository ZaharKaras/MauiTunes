using MauiTunes.Entities;
using MauiTunes.Models;
using Newtonsoft.Json;
using Plugin.Maui.Audio;
using RestSharp;
using System.Net;
using System.Text.Json;


namespace MauiTunes.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly RestClient _client;
        public SpotifyService()
        {
            _client = new RestClient("https://api.spotify.com/v1/search");
        }

        public async Task<SearchResult> GetAlbums(string albumName, AuthorizationToken token)
        {
            var client = new RestClient("https://api.spotify.com/v1/search");
            client.AddDefaultHeader("Authorization", $"Bearer {token.AccessToken}");
            var request = new RestRequest($"?q={albumName}&type=album", Method.Get);
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

        public async Task<Albums> GetAlbumsByArtistId(string artistId, AuthorizationToken token)
        {
            var client = new RestClient("https://api.spotify.com/v1/");
            client.AddDefaultHeader("Authorization", $"Bearer {token.AccessToken}");
            var request = new RestRequest($"artists/{artistId}/albums", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var result = System.Text.Json.JsonSerializer.Deserialize<Albums>(response.Content);
                return result;
            }
            else
            {
                return null;
            }
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

        public async Task<SearchResult> GetArtistById(string artistId, AuthorizationToken token)
        {
            var client = new RestClient("https://api.spotify.com/v1/");
            client.AddDefaultHeader("Authorization", $"Bearer {token.AccessToken}");
            var request = new RestRequest($"artists/{artistId}", Method.Get);
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
            var client = new RestClient("https://api.spotify.com/v1/search");
            client.AddDefaultHeader("Authorization", $"Bearer {token.AccessToken}");
            var request = new RestRequest($"?q={trackName}&type=track", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var result = System.Text.Json.JsonSerializer.Deserialize<SearchResult>(response.Content);
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<Tracks> GetTrackByAlbumId(string albumId, AuthorizationToken token)
        {
            var client = new RestClient("https://api.spotify.com/v1/");
            client.AddDefaultHeader("Authorization", $"Bearer {token.AccessToken}");
            var request = new RestRequest($"albums/{albumId}/tracks", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var result = System.Text.Json.JsonSerializer.Deserialize<Tracks>(response.Content);
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<Track> GetTrackById(string trackId, AuthorizationToken token)
        {
            var client = new RestClient("https://api.spotify.com/v1/");
            client.AddDefaultHeader("Authorization", $"Bearer {token.AccessToken}");
            var request = new RestRequest($"tracks/{trackId}", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var result = System.Text.Json.JsonSerializer.Deserialize<Track>(response.Content);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
