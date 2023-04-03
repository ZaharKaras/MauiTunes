using MauiTunes.Services;
using SpotifyAPI.Web;

namespace MauiTunes;

public partial class MainPage : ContentPage
{
	int count = 0;
	private ISpotifyAccountService _spotifyAccountService { get; init; }
	private ISpotifyService _spotifyService { get; init; }

	public MainPage(ISpotifyAccountService spotifyAccountService, ISpotifyService spotifyService)
	{
		InitializeComponent();
		_spotifyAccountService = spotifyAccountService;
		_spotifyService = spotifyService;
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

