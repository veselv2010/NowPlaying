using NowPlaying.Core;
using NowPlaying.Core.Api;
using NowPlaying.Core.Config;
using NowPlaying.Core.GameProcessHook;
using NowPlaying.Core.InputSender;
using NowPlaying.Core.Steam;
using NowPlaying.Wpf.Auth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NowPlaying.Wpf
{
    public partial class MainWindow : Window
    {
        private ISteamService steamService;
        private PathResolver pathResolver;
        private ConfigWriter configWriter;
        private GameProcess gameProcess;
        private IInputSender keySender;
        private SteamContext userContext;
        private SpotifyRequestsManager spotify;
        private IDictionary<string, int> accounts;

        private Timer trackUpdateTimer;
        public MainWindow()
        {
            InitializeComponent();

            spotify = new SpotifyRequestsManager("7633771350404368ac3e05c9cf73d187",
                "29bd9ec2676c4bf593f3cc2858099838", @"http://localhost:8888/");
            steamService = new SteamServiceWindows();
            pathResolver = new PathResolver();
            keySender = new InputSenderWindows();
            userContext = steamService.GetSteamContext();

            accounts = userContext.GetAccounts();

            gameProcess = new GameProcess();
            gameProcess.Start();

            int steamid3 = accounts[userContext.LastAccount];
            string writePath = pathResolver.GetWritePath(gameProcess.CurrentProcess, userContext.UserdataPath, steamid3.ToString());
            configWriter = new ConfigWriter(writePath);

            trackUpdateTimer = new Timer(1000);
            trackUpdateTimer.AutoReset = true;
            trackUpdateTimer.Elapsed += updateTrackInfo;

            ConsolePaste.Text = "bind \"key\" \"exec audio.cfg\"";
            UserSettingsBlock.CurrentAccountText.Text = userContext.LastAccount;
        }

        private string lastTrackId;
        private async void updateTrackInfo(object sender, ElapsedEventArgs e)
        {
            var currentTrack = await spotify.GetCurrentTrack();
            PlayingTrackControl.CurrentTrack = currentTrack;

            await Dispatcher.Invoke(async () =>
            {
                var image = await GetAlbumImage(currentTrack.CoverUrl);
                AlbumCover.Source = image;
                UserSettingsBlock.CurrentGameText.Text = gameProcess.CurrentProcess?.WindowName ?? "";
            });

            if (!gameProcess.IsValid || lastTrackId == currentTrack.Id)
                return;

            lastTrackId = currentTrack.Id;

            configWriter.RewriteKeyBinding(currentTrack);

            if(UserSettingsBlock.AutosendCheck.IsToggled)
                keySender.SendSystemInput(UserSettingsBlock.CurrentVirtualKey);
        }

        private async Task<BitmapImage> GetAlbumImage(string url)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(url, UriKind.Absolute);
            bitmap.EndInit();
            return bitmap;
        }

        private async Task<string> AskCode()
        {
            using (var auth = new AuthServer(@"http://localhost:8888/"))
            {
                string authUrl = spotify.GetAuthUrl().Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {authUrl}") { CreateNoWindow = true });
                return await auth.GetAuthCode();
            }
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            AcrylicMaterial.EnableBlur(this);
            this.Hide();
            string code = await AskCode();

            if (code == default)
                Application.Current.Shutdown();

            await spotify.StartTokenRequests(code);
            trackUpdateTimer.Start();
            this.Show();
        }

        private void HeaderBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                OnMouseLeftButtonDown(e);
                this.DragMove();
            }
            catch (System.InvalidOperationException)
            {
                return;
            }
        }
    }
}
