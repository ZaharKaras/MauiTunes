using AndroidX.Browser.Trusted;
using MauiTunes.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTunes.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly RestClient _client;
        public SpotifyService(RestClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<Album>> GetAlbums(string albumName, AuthorizationToken token)
        {
            var albums = new List<Album>();
            var request = new RestRequest("/v1/search");
            request.AddHeader("Authorization", $"Bearer {token.AccessToken}");
            request.AddParameter("q", albumName);
            request.AddParameter("type", "album");
            
            var response = await _client.GetAsync(request);

            if(response.IsSuccessStatusCode)
            {
                albums = JsonConvert.DeserializeObject<List<Album>>(response.Content);
            }

            return albums;
        }

        public Task<IEnumerable<Artist>> GetArtis(string artistName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Track>> GetTracks(string trackName)
        {
            throw new NotImplementedException();
        }
    }
}
