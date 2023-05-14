using CommunityToolkit.Mvvm.ComponentModel;
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

namespace MauiTunes.ViewModels
{
    public partial class ArtistViewModel : ViewModel
    {
        private readonly ISpotifyService spotifyService;

        public ArtistViewModel(ISpotifyService spotifyService)
        {
            this.spotifyService = spotifyService;
        }

        public override async Task OnParameterSet()
        {
            await base.OnParameterSet();

            try
            {
                IsBusy = true;

                IDictionary<AuthorizationToken, Artist> parametr = (IDictionary<AuthorizationToken, Artist>)NavigationParameter;
                var token = parametr.First().Key;
                var artist = parametr.First().Value;

                //List<Task> tasks = new();

                //var artistTask = spotifyService.GetArtist(art.Id, token);
                var resultAlbums = await spotifyService.GetAlbumsByArtistId(artist.Id, token);

                //await Task.WhenAll();

                //var artist = artistTask.Result.Artists.Items[0];

                TopImage = artist.Images.Any() ? artist.Images.First().Url : null;
                Name = artist.Name;
                Followers = artist.Followers.Total;

                //var newAlbums = resultAlbums.Albums.Items.Select(x => new SearchItemViewModel()
                //{
                //    Id = x.Id,
                //    Title = x.Name,
                //    ImageUrl = x.Images.Any() ? x.Images.First().Url : null,
                //    //TapCommand = NavigateToAlbumCommand,
                //});

                //Albums = new(newAlbums);
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

        [ObservableProperty]
        private ObservableCollection<SearchItemViewModel> albums = new();
    }
}
