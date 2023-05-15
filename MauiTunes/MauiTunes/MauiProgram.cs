using Microsoft.Extensions.Logging;
using System.Net.Http;
using MauiTunes.Services;
using RestSharp;
using MauiTunes.Views;
using CommunityToolkit.Maui;
using TinyMvvm;
using MauiTunes.ViewModels;
using Plugin.Maui.Audio;
using MauiTunes.Views;
//using Java.Security.Cert;

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
            .UseMauiCommunityToolkitMediaElement()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<SearchView>();
        builder.Services.AddTransient<SearchViewModel>();
        builder.Services.AddTransient<ISpotifyAccountService, SpotifyAccountService>();
        builder.Services.AddTransient<ISpotifyService, SpotifyService>();
        builder.Services.AddTransient<ISpotifyPlayer, SpotifyPlayer>();

        builder.Services.AddSingleton(AudioManager.Current);

        //View
        builder.Services.AddTransient<PlayerView>();
        builder.Services.AddTransient<ArtistView>();
        builder.Services.AddTransient<TrackView>();
        builder.Services.AddTransient<AlbumView>();

        //ViewModel
        builder.Services.AddTransient<PlayerViewModel>();
        builder.Services.AddTransient<ArtistViewModel>();
        builder.Services.AddSingleton<TrackViewModel>();
        builder.Services.AddTransient<AlbumViewModel>();

        // Регистрация HttpClient и настройка BaseAddress для каждого сервиса
        //builder.Services.AddHttpClient<ISpotifyAccountService, SpotifyAccountService>(opt => opt.BaseAddress = new Uri("https://accounts.spotify.com/api/token"));
        //builder.Services.AddHttpClient<ISpotifyService, SpotifyService>(opt => opt.BaseAddress = new Uri("https://accounts.spotify.com"));

        Routing.RegisterRoute("Artist", typeof(ArtistView));
        Routing.RegisterRoute("Track", typeof(TrackView));
        Routing.RegisterRoute("Album", typeof(AlbumView));

        return builder.Build();
	}
}
