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
        Task<IEnumerable<Album>> GetAlbums(string albumName, AuthorizationToken token);
        Task<IEnumerable<Artist>> GetArtis(string artistName);
        Task<IEnumerable<Track>> GetTracks(string trackName);
    }
}
