using MauiTunes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTunes.Services
{
    public interface ISpotifyPlayer
    {
        public Task<bool> PlayTrack(string trackId, AuthorizationToken token);
    }
}
