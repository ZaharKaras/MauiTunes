using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTunes.Entities;
using MauiTunes.Models;
using MauiTunes.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTunes.ViewModels
{
    public partial class TrackViewModel : ViewModel
    {
        private ISpotifyPlayer player;
        private ISpotifyService spotifyService;
        private AuthorizationToken token;
        private string trackId;
        [ObservableProperty]
        public List<Track> trackQueue;
        public TrackViewModel(ISpotifyPlayer player, ISpotifyService spotifyService)
        {
            this.player = player;
            this.spotifyService = spotifyService;
        }

        public override async Task OnParameterSet()
        {
            await base.OnParameterSet();

            try
            {
                IsBusy = true;

                IDictionary<AuthorizationToken, IEnumerable<Track>> parametr = (IDictionary<AuthorizationToken, IEnumerable<Track>>)NavigationParameter;
                token = parametr.First().Key;
                var tracks = (List<Track>)parametr.First().Value;

                TrackQueue = tracks.ToList();

                var track = tracks.FirstOrDefault();

                track = await spotifyService.GetTrackById(track.Id, token);
                trackId = track.Id;

                TopImage = track.Album.Images.Any() ? track.Album.Images.First().Url : null;
                Name = track.Name;
                PreviewUrl = track.PreviewUrl;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            IsBusy = false;
        }

        [ObservableProperty]
        private string topImage, name;
        [ObservableProperty]
        private string previewUrl;

        [ObservableProperty]
        private ObservableCollection<SearchItemViewModel> albums = new();

        [RelayCommand]
        async Task PlayPreviousTrackAsync()
        {
            int index = trackQueue.FindIndex(x => x.Id == trackId);
            index--;

            if(index < 0)
            {
                var trackFromQueue = trackQueue.Last();
                var track = await spotifyService.GetTrackById(trackFromQueue.Id, token);
                trackId = track.Id;
                TopImage = track.Album.Images.Any() ? track.Album.Images.First().Url : null;
                Name = track.Name;
                PreviewUrl = track.PreviewUrl;
            }
            else
            {
                var trackFromQueue = trackQueue[index];
                var track = await spotifyService.GetTrackById(trackFromQueue.Id, token);
                trackId = track.Id;
                TopImage = track.Album.Images.Any() ? track.Album.Images.First().Url : null;
                Name = track.Name;
                PreviewUrl = track.PreviewUrl;
            }
        }

        [RelayCommand]
        async Task PlayNextTrack()
        {
            int index = trackQueue.FindIndex(x => x.Id == trackId);
            index++;

            if (index > trackQueue.Count-1)
            {
                var trackFromQueue = trackQueue.First();
                var track = await spotifyService.GetTrackById(trackFromQueue.Id, token);
                trackId = track.Id;
                TopImage = track.Album.Images.Any() ? track.Album.Images.First().Url : null;
                Name = track.Name;
                PreviewUrl = track.PreviewUrl;
            }
            else
            {
                var trackFromQueue = trackQueue[index];
                var track = await spotifyService.GetTrackById(trackFromQueue.Id, token);
                trackId = track.Id;
                TopImage = track.Album.Images.Any() ? track.Album.Images.First().Url : null;
                Name = track.Name;
                PreviewUrl = track.PreviewUrl;
            }
        }

        
        public async Task NavigateToTrack()
        {
            int index = TrackQueue.FindIndex(s => s.Id == trackId);
            var tracks = TrackQueue.Skip(index).Concat(TrackQueue.Take(index)).ToList();

            IDictionary<AuthorizationToken, IEnumerable<Track>> parametrs = new Dictionary<AuthorizationToken, IEnumerable<Track>>()
            {
                {token, tracks}
            };

            await Navigation.NavigateTo("Track", parametrs);
        }
    }
}
