using Microsoft.Extensions.Logging;
using System.Net.Http;
using MauiTunes.Services;
using RestSharp;
using MauiTunes.View;
using CommunityToolkit.Maui;
using TinyMvvm;
using MauiTunes.ViewModels;

namespace MauiTunes;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseTinyMvvm()
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<SearchView>();
        builder.Services.AddTransient<SearchViewModel>();
        builder.Services.AddTransient<ISpotifyAccountService, SpotifyAccountService>();
        builder.Services.AddTransient<ISpotifyService, SpotifyService>();
        builder.Services.AddTransient<ISpotifyPlayer, SpotifyPlayer>();

        // Регистрация HttpClient и настройка BaseAddress для каждого сервиса
        //builder.Services.AddHttpClient<ISpotifyAccountService, SpotifyAccountService>(opt => opt.BaseAddress = new Uri("https://accounts.spotify.com/api/token"));
        //builder.Services.AddHttpClient<ISpotifyService, SpotifyService>(opt => opt.BaseAddress = new Uri("https://accounts.spotify.com"));

        return builder.Build();
	}
}
