//using Android.Media;
using MauiTunes.Entities;
using MauiTunes.Models;
using MauiTunes.Services;
using Plugin.Maui.Audio;
using SpotifyAPI.Web;
using System.Collections.ObjectModel;

namespace MauiTunes;

public partial class MainPage : ContentPage
{
	private ISpotifyAccountService _spotifyAccountService;
	private ISpotifyService _spotifyService;
	private ISpotifyPlayer _spotifyPlayer;
	private List<Track> _tracks;
	private AuthorizationToken token;
	private int count = 0;
	public ObservableCollection<SearchResult> Art { get; set; }

    public MainPage(ISpotifyAccountService spotifyAccountService, ISpotifyService spotifyService, ISpotifyPlayer spotifyPlayer)
	{
		InitializeComponent();
		_spotifyAccountService = spotifyAccountService;
		_spotifyService = spotifyService;
		_spotifyPlayer = spotifyPlayer;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		token = await _spotifyAccountService.GetToken("541e4eb4340743ef80eb76cb7bace234", "2114609948734fc9b529f44c50f3d3cc");

		var result = await _spotifyService.GetTracks("three days grace", token);
		_tracks = result.Tracks.Items;

		var observableArtist = new ObservableCollection<SearchResult>();
		Art = observableArtist;
		albumsList.ItemsSource = _tracks;

	}

	private async void Button_Clicked_1Async(object sender, EventArgs e)
	{
		var preview_url = _tracks[0].PreviewUrl;
		if(count == 1)
		{
			_spotifyPlayer.StopeAudioAsync();
			count--;
		}
		else
		{
            count++;
            _spotifyPlayer.PlayAudioAsync();

        }
    }
}

