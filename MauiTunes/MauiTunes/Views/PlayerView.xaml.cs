using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using MauiTunes.ViewModels;
using System.ComponentModel;

namespace MauiTunes.Views;

public partial class PlayerView : ContentPage
{
    public PlayerView(PlayerViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        mediaElement.PropertyChanged += MediaElement_PropertyChanged;
    }

    void MediaElement_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == MediaElement.DurationProperty.PropertyName)
        {
            positionSlider.Maximum = mediaElement.Duration.TotalSeconds;
        }
    }

    void OnPositionChanged(object sender, MediaPositionChangedEventArgs e)
    {
        positionSlider.Value = e.Position.TotalSeconds;
    }

    void OnPlayClicked(object? sender, EventArgs e)
    {
        mediaElement.Play();
    }

    void OnPauseClicked(object sender, EventArgs e)
    {
        mediaElement.Pause();
    }
    void Slider_DragCompleted(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(sender);

        var newValue = ((Slider)sender).Value;
        mediaElement.SeekTo(TimeSpan.FromSeconds(newValue));
        mediaElement.Play();
    }

    void Slider_DragStarted(object sender, EventArgs e)
    {
        mediaElement.Pause();
    }
}