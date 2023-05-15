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
        private async Task NavigateToArtist(string id)
        {
            var artist = artistsResult.FirstOrDefault(a => a.Id == id);

            IDictionary<AuthorizationToken, Artist> parametrs = new Dictionary<AuthorizationToken, Artist>()
            {
                {token, artist}
            };

            await Navigation.NavigateTo("Artist", parametrs);
        }

        [RelayCommand]
        private async void NavigateToAlbum(string id)
        {
            var album = albumsResult.FirstOrDefault(b => b.Id == id);

            IDictionary<AuthorizationToken, Album> parametrs = new Dictionary<AuthorizationToken, Album>()
            {
                { token, album }
            };

            await Navigation.NavigateTo("Album", parametrs);
        }

        [RelayCommand]
        private async void NavigateToTrack(string id)
        {
            int index = tracksResult.FindIndex(s => s.Id == id);

            var tracks = tracksResult.Skip(index).Concat(tracksResult.Take(index)).ToList();

            IDictionary<AuthorizationToken, IEnumerable<Track>> parametrs = new Dictionary<AuthorizationToken, IEnumerable<Track>>()
            {
                {token, tracks}
            };

            await Navigation.NavigateTo("Track", parametrs);
        }

        private List<Artist> artistsResult;
        private List<Track> tracksResult;
        private List<Album> albumsResult;

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

                artistsResult = resultArtists.Artists.Items;
                tracksResult = resultTracks.Tracks.Items;
                albumsResult = resultAlbums.Albums.Items;

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
