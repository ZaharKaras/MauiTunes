using MauiTunes.Entities;
using MauiTunes.Models;
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
        Task<SearchResult> GetArtist(string artistName, AuthorizationToken token);
        Task<SearchResult> GetTracks(string trackName, AuthorizationToken token);
    }
}
