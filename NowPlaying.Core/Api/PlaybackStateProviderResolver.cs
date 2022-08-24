using NowPlaying.Core.Api.Spotify;
using NowPlaying.Core.Api.WindowsManager;
using NowPlaying.Core.Settings;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NowPlaying.Core.Api
{
    public sealed class PlaybackStateProviderResolver
    {
        public Task<ITrackInfoUpdater> ResolveTrackInfoUpdater(PlaybackStateProvider provider)
        {
            return provider switch
            {
                PlaybackStateProvider.SPOTIFY => getSpotifyProvider(),
                PlaybackStateProvider.WINDOWSRT => getWindowsProvider(),
                _ => throw new NotImplementedException($"Invalid provider: {provider.GetType().Name}")
            };
        }

        private async Task<ITrackInfoUpdater> getSpotifyProvider()
        {
            string redirectUrl = @"http://localhost:8888/";
            var requestsManager = new SpotifyRequestsManager("7633771350404368ac3e05c9cf73d187",
    "29bd9ec2676c4bf593f3cc2858099838", redirectUrl);

            using (var server = new AuthServer(redirectUrl))
            {
                string authUrl = requestsManager.GetAuthUrl().Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {authUrl}") { CreateNoWindow = true });
                string code = await server.GetAuthCode();

                await requestsManager.StartTokenRequests(code);
            }

            return new SpotifyTrackUpdater(requestsManager);
        }

        private async Task<ITrackInfoUpdater> getWindowsProvider()
        {
            return new WindowsMediaManager();
        }
    }
}
