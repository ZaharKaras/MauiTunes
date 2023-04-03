using Microsoft.Extensions.Logging;
using System.Net.Http;
using MauiTunes.Services;


namespace MauiTunes;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<ISpotifyAccountService, SpotifyAccountService>();
		builder.Services.AddTransient<ISpotifyService, SpotifyService>();
		builder.Services.AddHttpClient<ISpotifyAccountService, SpotifyAccountService>(opt => opt.BaseAddress = new Uri("https://accounts.spotify.com/api/token"));
		builder.Services.AddHttpClient<ISpotifyService, SpotifyService>(opt => opt.BaseAddress = new Uri("https://accounts.spotify.com"));

//#if DEBUG
//		builder.Logging.AddDebug();
//#endif

		return builder.Build();
	}
}
