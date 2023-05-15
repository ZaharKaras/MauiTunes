using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTunes.Entities;
using MauiTunes.Models;
using MauiTunes.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiTunes.ViewModels
{
    public partial class ArtistViewModel : ViewModel
    {
        private readonly ISpotifyService spotifyService;
        private AuthorizationToken token;

        public ArtistViewModel(ISpotifyService spotifyService)
        {
            this.spotifyService = spotifyService;
        }

        [ObservableProperty]
        private ObservableCollection<ArtistItemViewModel> albums;

        private List<Album> albumsResult;

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

        public override async Task OnParameterSet()
        {
            await base.OnParameterSet();

            try
            {
                IsBusy = true;

                IDictionary<AuthorizationToken, Artist> parametr = (IDictionary<AuthorizationToken, Artist>)NavigationParameter;
                token = parametr.First().Key;
                var artist = parametr.First().Value;

               
                var resultAlbums = await spotifyService.GetAlbumsByArtistId(artist.Id, token);

                albumsResult = resultAlbums.Items;

                TopImage = artist.Images.Any() ? artist.Images.First().Url : null;
                Name = artist.Name;
                Followers = artist.Followers.Total;

                var albums = resultAlbums.Items.Select(x => new ArtistItemViewModel()
                {
                    Id = x.Id,
                    Title = x.Name,
                    ImageUrl = x.Images.Any() ? x.Images.First().Url : null,
                    TapCommand = NavigateToAlbumCommand,
                });

                Albums = new ObservableCollection<ArtistItemViewModel>(albums);

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
        private int followers;

    }

    public class ArtistItemViewModel : ViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public ICommand TapCommand { get; set; }
    }
}
