namespace MauiTunes.Views;

using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using MauiTunes.ViewModels;
using System.ComponentModel;
using TinyMvvm;

public partial class TrackView
{
    private TrackViewModel viewmodel;
    private int count;
	public TrackView(TrackViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        mediaElement.PropertyChanged += MediaElement_PropertyChanged;
        count = 0;
        viewmodel = vm;
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

    void OnPlayPauseClicked(object sender, EventArgs e)
    {
        if (count == 0)
        {
            PlayPauseButton.Source = "pause_button.png";
            mediaElement.Play();
            count++;
        }
        else
        {
            PlayPauseButton.Source = "play_button.png";
            mediaElement.Pause();
            count--;
        }
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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        mediaElement.Stop();
    }


}