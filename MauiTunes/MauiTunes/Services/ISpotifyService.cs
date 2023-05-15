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
        Task<SearchResult> GetAlbums(string albumName, AuthorizationToken token);
        Task<Albums> GetAlbumsByArtistId(string artistId, AuthorizationToken token);
        Task<SearchResult> GetArtist(string artistName, AuthorizationToken token);
        Task<SearchResult> GetArtistById(string artistId, AuthorizationToken token);
        Task<SearchResult> GetTracks(string trackName, AuthorizationToken token);
        Task<Tracks> GetTrackByAlbumId(string albumId, AuthorizationToken token);
        Task<Track> GetTrackById(string trackId, AuthorizationToken token);
    }
}
