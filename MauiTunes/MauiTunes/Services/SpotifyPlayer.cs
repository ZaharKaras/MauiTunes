using MauiTunes.Entities;
using Plugin.Maui.Audio;
using RestSharp;



namespace MauiTunes.Services
{
    public class SpotifyPlayer : ISpotifyPlayer
    {
        private RestClient _client;
        private readonly IAudioManager _audioManager;
        private IAudioPlayer _player;
        private string _source;
        private string url;
        public SpotifyPlayer(IAudioManager audioManager)
        {
            _audioManager = audioManager;
            _client = new RestClient("https://api.spotify.com/v1");
        }

        public async Task<bool> PlayTrack(string trackId, AuthorizationToken token)
        {
            var url = "https://api.spotify.com/v1/me/player/play";
            var playLoad = new
            {
                uris = new[] { $"spotify:track:{trackId}" }
            };

            var request = new RestRequest(url, Method.Put);
            request.AddHeader("Authorization", $"Bearer {token.AccessToken}");
            request.AddJsonBody(playLoad);
            var response = await _client.ExecuteAsync(request);

            return response.IsSuccessStatusCode;
        }

        public async Task PlayAudioAsync()
        {
            if(_player != null )
            {
                _player.Play();
                return;
            }

            var url = "https://p.scdn.co/mp3-preview/c973eb4df46a3fb0f780964d2be3861e3b84eeff?cid=541e4eb4340743ef80eb76cb7bace234";
            
            string cacheDir = FileSystem.Current.CacheDirectory;


            var localFile = $"{cacheDir}\\{Path.GetFileName(url)}";

            if (!File.Exists(localFile))
            {

                using (var client = new HttpClient())
                {
                    var uri = new Uri(url);
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    using (var _stream = await response.Content.ReadAsStreamAsync())
                    {
                        var fileInfo = new FileInfo(localFile);
                        using (var fileStream = fileInfo.OpenWrite())
                        {
                            await _stream.CopyToAsync(fileStream);
                        }
                    }
                }
            }

            var stream = File.OpenRead(localFile);

            _source = localFile;

            _player = _audioManager.CreatePlayer(stream);
            
            _player.Play();

            //await Task.Delay(4000);

            //_player.Pause();

            //await Task.Delay(4000);

            //_player.Play();
        }

        public Task StopeAudioAsync()
        {
            _player.Stop();
            return Task.CompletedTask;
        }

        public Task PauseAudioAsync()
        {
            _player.Pause();
            return Task.CompletedTask;
        }

        public double GetDuration()
        {
            if (_player is not null) return _player.Duration;
            return 0;
        }

        public double GetPosition()
        {
            if (_player is not null) return _player.CurrentPosition;
            return 0;
        }

        public string GetSource()
        {
            return _source;
        }
    }
}
