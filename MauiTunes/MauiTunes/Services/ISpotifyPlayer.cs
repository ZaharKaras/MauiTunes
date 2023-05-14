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
        Task<bool> PlayTrack(string trackId, AuthorizationToken token);
        public Task PlayAudioAsync();
        public Task StopeAudioAsync();
        public Task PauseAudioAsync();
        public double GetDuration();
        public double GetPosition();
        public string GetSource();

    }
}
