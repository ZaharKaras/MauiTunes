using MauiTunes.Entities;
using RestSharp;
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
