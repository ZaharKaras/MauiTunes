using MauiTunes.Entities;
using MauiTunes.Models;
using MauiTunes.Services;
using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace MauiTunes.ViewModels
{
    public partial class AlbumViewModel : ViewModel
    {
        private readonly ISpotifyService spotifyService;
        private AuthorizationToken token;

        public AlbumViewModel(ISpotifyService spotifyService)
        {
            this.spotifyService = spotifyService;
        }

        [RelayCommand]
        private async void NavigateToTrack(string id)
        {
            //var track = tracksResult.FirstOrDefault(b => b.Id == id);

            //IDictionary<AuthorizationToken, Track> parametrs = new Dictionary<AuthorizationToken, Track>()
            //{
            //    {token, track}
            //};

            //await Navigation.NavigateTo("Track", parametrs);

            int index = tracksResult.FindIndex(s => s.Id == id);

            var tracks = tracksResult.Skip(index).Concat(tracksResult.Take(index)).ToList();

            IDictionary<AuthorizationToken, IEnumerable<Track>> parametrs = new Dictionary<AuthorizationToken, IEnumerable<Track>>()
            {
                {token, tracks}
            };

            await Navigation.NavigateTo("Track", parametrs);
        }

        [ObservableProperty]
        private ObservableCollection<AlbumItemViewModel> tracks;


        private List<Track> tracksResult;


        public override async Task OnParameterSet()
        {
            await base.OnParameterSet();

            try
            {
                IsBusy = true;

                IDictionary<AuthorizationToken, Album> parametr = (IDictionary<AuthorizationToken, Album>)NavigationParameter;
                token = parametr.First().Key;
                var album = parametr.First().Value;

                var resultTracks = spotifyService.GetTrackByAlbumId(album.Id, token);

                await Task.WhenAll(resultTracks);

                tracksResult = resultTracks.Result.Items;

                TopImage = album.Images.Any() ? album.Images.First().Url : null;
                Name = album.Name;

                var tracks = tracksResult.Select(x => new AlbumItemViewModel()
                {
                    Id = x.Id,
                    Title = x.Name,
                    ImageUrl = TopImage,
                    TapCommand = NavigateToTrackCommand,
                });

                Tracks = new ObservableCollection<AlbumItemViewModel>(tracks);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            IsBusy = false;
        }

        [ObservableProperty]
        private string topImage, name;
    }

    public class AlbumItemViewModel : ViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public ICommand TapCommand { get; set; }
    }

}
