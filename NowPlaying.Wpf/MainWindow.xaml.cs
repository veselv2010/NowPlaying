using NowPlaying.Core.Api;
using NowPlaying.Core.Api.Spotify;
using NowPlaying.Core.Config;
using NowPlaying.Core.GameProcessHook;
using NowPlaying.Core.InputSender;
using NowPlaying.Core.Steam;
using NowPlaying.Core.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using NowPlaying.Core.Api.WindowsManager;

namespace NowPlaying.Wpf
{
    public partial class MainWindow : Window
    {
        private ISteamService _steamService;
        private PathResolver _pathResolver;
        private ConfigWriter _configWriter;
        private GameProcess _gameProcess;
        private IInputSender _keySender;
        private SteamContext _userContext;
        private ITrackInfoUpdater _playbackInfoUpdater;
        private IEnumerable<User> _accounts;
        private UserSettingsWorker _appConfigWorker;
        private UserSettings _appConfig;

        public MainWindow()
        {
            InitializeComponent();

            HeaderBlock.CloseButton.MouseLeftButtonDown +=
                new MouseButtonEventHandler((o, s) => Application.Current.Shutdown());
            HeaderBlock.CollapseButton.MouseLeftButtonDown +=
                new MouseButtonEventHandler((o, s) => WindowState = WindowState.Minimized);
            _appConfigWorker = new UserSettingsWorker();
            _appConfig = _appConfigWorker.ReadConfigFile();

            _steamService = OperatingSystem.IsWindows() ? new SteamServiceWindows() : new SteamServiceLinux();

            _keySender = new InputSenderWindows();
            _pathResolver = new PathResolver();
            _gameProcess = new GameProcess();
        }

        private string _lastTrackFullName; //local files handling
        private void onPlaybackStateUpdate(IPlaybackResponse playbackState)
        {
            if (playbackState == null)
                return;

            PlayingTrackControl.Update(playbackState);

            string gameName = _gameProcess.CurrentProcess?.WindowName ?? "";
            UserSettingsBlock.Update(_userContext.LastAccount, gameName, _appConfig.LastProvider.ToString());

            if (_lastTrackFullName == playbackState.FullName)
                return;

            _configWriter.RewriteKeyBinding(playbackState);

            if (!string.IsNullOrEmpty(playbackState.CoverUrl))
            {
                BackgroundCover.Update(playbackState.CoverUrl);
            }

            _lastTrackFullName = playbackState.FullName;

            if (UserSettingsBlock.AutosendCheck.IsToggled && _gameProcess.IsValid)
                _keySender.SendSystemInput(UserSettingsBlock.CurrentVirtualKey);
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            AcrylicMaterial.EnableBlur(this);
            this.Hide();
            if (_appConfig.LastProvider == PlaybackStateProvider.SPOTIFY)
            {
                string redirectUrl = @"http://localhost:8888/";

                var requestsManager = new SpotifyRequestsManager("7633771350404368ac3e05c9cf73d187",
                "29bd9ec2676c4bf593f3cc2858099838", redirectUrl);

                Console.WriteLine("Awaiting user authorization...");
                using (var server = new AuthServer(redirectUrl))
                {
                    string authUrl = requestsManager.GetAuthUrl().Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {authUrl}") { CreateNoWindow = true });
                    string code = await server.GetAuthCode();

                    if (code == default)
                        Application.Current.Shutdown();

                    await requestsManager.StartTokenRequests(code);

                }

                _playbackInfoUpdater = new SpotifyTrackUpdater(requestsManager);
            }
            else
            {
                _playbackInfoUpdater = new WindowsMediaManager();
            }

            _gameProcess.Start();

            _userContext = _steamService.GetSteamContext();
            _accounts = _userContext.GetAccounts();
            int steamid3 = _accounts.FirstOrDefault((x) => x.Name == _userContext.LastAccount).SteamId3;
            string writePath = _pathResolver.GetWritePath(_gameProcess.CurrentProcess, _userContext.UserdataPath, steamid3.ToString());

            _configWriter = new ConfigWriter(writePath, _appConfig.CfgText);

            _playbackInfoUpdater.OnPlaybackStateUpdate += onPlaybackStateUpdate;

            UserSettingsBlock.CurrentAccountText.Text = _userContext.LastAccount;
            UserSettingsBlock.UpdateKey(_appConfig.LastUsedKey);

            if (_appConfig.IsAutoSendEnabled)
                UserSettingsBlock.AutosendCheck.Toggle();

            _playbackInfoUpdater.StartPlaybackUpdate();
            this.Show();
        }

        private void HeaderBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OnMouseLeftButtonDown(e);
                DragMove();
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _appConfig.IsAutoSendEnabled = UserSettingsBlock.AutosendCheck.IsToggled;
            _appConfig.LastUsedKey = UserSettingsBlock.CurrentKeyControl.CurrentKeyTextBlock.Text;
            _appConfigWorker.SaveConfigFile(_appConfig);

            _playbackInfoUpdater.Dispose();
            _gameProcess.Dispose();
            _playbackInfoUpdater.Dispose();
        }
    }
}
