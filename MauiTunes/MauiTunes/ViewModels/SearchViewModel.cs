using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiTunes.Entities;
using MauiTunes.Services;
using MauiTunes.Models;

namespace MauiTunes.ViewModels
{
    public partial class SearchViewModel : ViewModel
    {
        private ISpotifyService spotifyService;
        private ISpotifyAccountService spotifyAccountService;
        private AuthorizationToken token;

        public SearchViewModel(ISpotifyService spotifyService, ISpotifyAccountService spotifyAccountService)
        {
            this.spotifyService = spotifyService;
            this.spotifyAccountService = spotifyAccountService;
            GetToken();
        }

        private async void GetToken()
        {
            token = await spotifyAccountService.GetToken("541e4eb4340743ef80eb76cb7bace234", "2114609948734fc9b529f44c50f3d3cc");
        }

        [ObservableProperty]
        private bool isSearching;

        [ObservableProperty]
        private bool hasResult;

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private ObservableCollection<SearchItemViewModel> artists;

        [ObservableProperty]
        private ObservableCollection<SearchItemViewModel> albums;

        [ObservableProperty]
        private ObservableCollection<SearchItemViewModel> tracks;

        [RelayCommand]
        private void StartSearch()
        {
            IsSearching = true;
        }
        [RelayCommand]
        private async Task Search()
        {
            try
            {
                IsBusy = true;

                //var types = "artist,album,track";

                var resultArtists = await spotifyService.GetArtist(SearchText, token);
                var resultAlbums = await spotifyService.GetAlbums(SearchText, token);
                var resultTracks = await spotifyService.GetTracks(SearchText, token);


                var artists = resultArtists.Artists.Items.Select(x => new SearchItemViewModel()
                {
                    Id = x.Id,
                    Title = x.Name,
                    ImageUrl = x.Images.Any() ? x.Images.First().Url : null,
                    TapCommand = NavigateToArtistCommand,
                }).ToList();

                Artists = new ObservableCollection<SearchItemViewModel>(artists);

                var albums = resultAlbums.Albums.Items.Select(x => new SearchItemViewModel()
                {
                    Id = x.Id,
                    Title = x.Name,
                    ImageUrl = x.Images.Any() ? x.Images.First().Url : null,
                    TapCommand = NavigateToAlbumCommand,
                });

                Albums = new ObservableCollection<SearchItemViewModel>(albums);

                var tracks = resultTracks.Tracks.Items.Select(x => new SearchItemViewModel()
                {
                    Id = x.Id,
                    Title = x.Name,
                    SubTitle = x.Artists.Any() ? x.Artists.First().Name : null,
                    ImageUrl = x.Album.Images.Any() ? x.Album.Images.First().Url : null,
                    TapCommand = NavigateToTrackCommand,
                });

                Tracks = new ObservableCollection<SearchItemViewModel>(tracks);

                HasResult = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            IsBusy = false;
        }

        [RelayCommand]
        private async Task NavigateToArtist(string id)
        {
            await Navigation.NavigateTo("Artist", id);
        }

        [RelayCommand]
        private void NavigateToAlbum(string id)
        {

        }

        [RelayCommand]
        private void NavigateToTrack(string id)
        {

        }

    }
    public class SearchItemViewModel : ViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageUrl { get; set; }
        public ICommand TapCommand { get; set; }
    }
}
