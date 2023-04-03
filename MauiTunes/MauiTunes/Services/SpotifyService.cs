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

namespace MauiTunes.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly RestClient _restClient;
        private AuthorizationToken _token;
        public SpotifyService(RestClient restClient)
        {
            _restClient = restClient;
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
