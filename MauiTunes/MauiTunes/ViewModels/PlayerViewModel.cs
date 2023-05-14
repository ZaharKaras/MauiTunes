using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTunes.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace MauiTunes.ViewModels
{
    public partial class PlayerViewModel: ObservableObject
    {
        private ISpotifyPlayer _player;
        private bool isPlaying;
        [ObservableProperty]
        public string source;
        [ObservableProperty]
        public double duration;
        public PlayerViewModel(ISpotifyPlayer player) 
        {
            _player = player;
            duration = _player.GetDuration();
            source = _player.GetSource();
            isPlaying = false;
        }
       
        [RelayCommand]
        async void Play()
        {
            await _player.PlayAudioAsync();
            isPlaying = true;
        }

        [RelayCommand]
        async void Pause()
        {
            await _player.PauseAudioAsync();
            isPlaying = false;
        }
    }
}
