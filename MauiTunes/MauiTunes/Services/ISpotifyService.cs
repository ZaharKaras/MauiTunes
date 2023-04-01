using MauiTunes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTunes.Services
{
    public interface ISpotifyService
    {
        Task<Album> GetAlbum(string albumName);
        Task<Artist> GetArtis(string artistName);
        Task<Track> GetTrack(string trackName);
    }
}
