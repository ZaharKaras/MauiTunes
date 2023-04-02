using MauiTunes.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.Media.Protection.PlayReady;

namespace MauiTunes.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly RestClient _client;
        private Token _token;

        public SpotifyService()
        {
            _client = new RestClient("https://accounts.spotify.com/api/token");
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", "541e4eb4340743ef80eb76cb7bace234");
            request.AddParameter("client_secret", "2114609948734fc9b529f44c50f3d3cc");

            var response = _client.Post(request);

            if(response.IsSuccessStatusCode)
            {
                _token = JsonConvert.DeserializeObject<Token>(response.Content);
            }
            
        }
        public Task<Album> GetAlbum(string albumName)
        {
            throw new NotImplementedException();
        }

        public Task<Artist> GetArtis(string artistName)
        {
            throw new NotImplementedException();
        }

        public Task<Track> GetTrack(string trackName)
        {
            throw new NotImplementedException();
        }

    }
}
