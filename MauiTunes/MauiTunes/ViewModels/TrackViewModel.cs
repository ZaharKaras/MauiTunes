using CommunityToolkit.Mvvm.ComponentModel;
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

                IDictionary<AuthorizationToken, Track> parametr = (IDictionary<AuthorizationToken, Track>)NavigationParameter;
                var token = parametr.First().Key;
                var track = parametr.First().Value;

                //List<Task> tasks = new();

                track = await spotifyService.GetTrackById(track.Id, token);

                //var artistTask = spotifyService.GetArtist(singer.Name, token);
                //var albumsTask = spotifyService.GetAlbums(singer.Name, token);

                //await Task.WhenAll(artistTask, albumsTask);

                //var artist = artistTask.Result;

                TopImage = track.Album.Images.Any() ? track.Album.Images.First().Url : null;
                Name = track.Name;
                PreviewUrl = track.PreviewUrl;

                //var albumResult = albumsTask.Result.Items.Select(x => new SearchItemViewModel()
                //{
                //    Id = x.Id,
                //    Title = x.Name,
                //    SubTitle = string.Join(",", x.Artists.Select(a => a.Name)),
                //    ImageUrl = x.Images.Any() ? x.Images.First().Url : null
                //});

                //Albums = new(albumResult);
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
    }
}
