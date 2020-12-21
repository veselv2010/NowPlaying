using NowPlaying.Core.Api;
using NowPlaying.Core.Config;
using NowPlaying.Core.GameProcessHook;
using NowPlaying.Core.InputSender;
using NowPlaying.Core.Steam;
using NowPlaying.Wpf.Auth;
using System.Collections.Generic;
using System.Timers;
using System.Windows;

namespace NowPlaying.Wpf
{
    public partial class MainWindow : Window
    {
        private ISteamService steamService;
        private PathResolver pathResolver;
        private LoginUsersReader usersReader;
        private ConfigWriter configWriter;
        private GameProcess gameProcess;
        private IInputSender keySender;
        private SteamInfo userContext;
        private SpotifyRequestsManager spotify;
        private IDictionary<string, int> accounts;

        private Timer trackUpdateTimer;
        public MainWindow()
        {
            InitializeComponent();

            spotify = new SpotifyRequestsManager("7633771350404368ac3e05c9cf73d187",
                "29bd9ec2676c4bf593f3cc2858099838", @"https://www.google.com/");
            steamService = new SteamServiceWindows();
            pathResolver = new PathResolver();
            keySender = new InputSenderWindows();
            userContext = steamService.GetSteamInfo();

            usersReader = new LoginUsersReader(userContext.LoginUsersPath);
            accounts = usersReader.Read();

            gameProcess = new GameProcess();
            gameProcess.Start();

            int userdataContext = accounts[userContext.LastAccount];
            string writePath = pathResolver.GetWritePath(gameProcess.CurrentProcess, userContext, userdataContext.ToString());
            configWriter = new ConfigWriter(writePath);

            trackUpdateTimer = new Timer(1000);
            trackUpdateTimer.AutoReset = true;
            trackUpdateTimer.Elapsed += updateTrackInfo;

            UserSettingsBlock.CurrentAccountText.Text = userContext.LastAccount;
        }

        string lastTrackId;
        private async void updateTrackInfo(object sender, ElapsedEventArgs e)
        {
            var currentTrack = await spotify.GetCurrentTrack();
            PlayingTrackControl.CurrentTrack = currentTrack;

            if (!gameProcess.IsValid || lastTrackId == currentTrack.Id)
                return;

            lastTrackId = currentTrack.Id;
            configWriter.RewriteKeyBinding(currentTrack);

            if(UserSettingsBlock.AutosendCheck.IsToggled)
                keySender.SendSystemInput(UserSettingsBlock.CurrentVirtualKey);
        }

        private string AskCode()
        {
            using (var auth = new AuthWindow(spotify.GetAuthUrl()))
            {
                auth.ShowDialog();
                return auth.Code;
            }
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            AcrylicMaterial.EnableBlur(this);
            this.Hide();
            string code = AskCode();

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
