using MauiTunes.Entities;
using MauiTunes.Models;
using MauiTunes.Services;
using SpotifyAPI.Web;
using System.Collections.ObjectModel;

namespace MauiTunes;

public partial class MainPage : ContentPage
{
	private ISpotifyAccountService _spotifyAccountService;
	private ISpotifyService _spotifyService;
	public ObservableCollection<SearchResult> Art { get; set; }

    public MainPage(ISpotifyAccountService spotifyAccountService, ISpotifyService spotifyService)
	{
		InitializeComponent();
		_spotifyAccountService = spotifyAccountService;
		_spotifyService = spotifyService;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		var token = await _spotifyAccountService.GetToken("541e4eb4340743ef80eb76cb7bace234", "2114609948734fc9b529f44c50f3d3cc");

		//var result = await _spotifyService.GetArtist("three days grace", token);
		var result = await _spotifyService.GetTracks("i hate everything about you", token);
		//var artists = result.Artists.Items;
		var tracks = result.Tracks.Items;

		var observableArtist = new ObservableCollection<SearchResult>();
		Art = observableArtist;
		albumsList.ItemsSource = tracks;

    }
}

