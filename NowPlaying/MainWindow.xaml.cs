﻿using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NowPlaying.ApiResponses;
using System;

namespace NowPlaying
{
    public partial class MainWindow : Window
    {
        DateTime TokenExpireTime;

        private CancellationTokenSource _cancellationGetSpotifyUpdates;

        protected string LastPlayingTrackId { get; set; }

        public MainWindow()
        {
            this.InitializeComponent();

            for (int i = 0; i < SteamIdLooker.Accounts.Count; i++)
                this.AccountsList.Items.Add(SteamIdLooker.Accounts[i]);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();

            var browserWindow = new OAuth.BrowserWindow();
            browserWindow.ShowDialog();

            if (browserWindow.ResultToken == null)
            {
                this.Close();
                return;
            }

            AppInfo.SpotifyAccessToken = browserWindow.ResultToken;
            TokenExpireTime = DateTime.Now.AddSeconds(browserWindow.ExpireTime);
            this.Show();
        }

        private void ButtonDo_Click(object sender, RoutedEventArgs e)
        {
            if (TokenExpireTime < DateTime.Now)
            {
                ButtonDo.Content = "spotify token expired!";   
                LabelTokenExpired.Visibility = Visibility.Visible;
                return;
            }

            CurrentTrackResponse trackResp = Requests.GetCurrentTrack(AppInfo.SpotifyAccessToken);

            if (trackResp == null)
                return;

            this.UpdateInterfaceTrackInfo(trackResp);

            if (this.AccountsList.SelectedItem == null)
                return;

            var cfgWriter = new ConfigWriter(this.TextBoxKeyBind.Text, $@"{SteamIdLooker.UserdataPath}\{this.GetSelectedAccountId().ToString()}\730\local\cfg\audio.cfg");
            cfgWriter.RewriteKeyBinding(trackResp);
        }

        private async void ToggleSwitch_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!this.SpotifySwitch.Toggled)
            {
				this.ChangeUIState(MainWindowUIState.Idle);
                this._cancellationGetSpotifyUpdates?.Cancel();
                return;
            }

            if (this.AccountsList.SelectedItem == null)
            {
                this.SpotifySwitch.TurnOff();
                MessageBox.Show("Выберите аккаунт"); // Здесь просто MessageBox какой-нибудь пользователю алертнуть мол он в край ебнулся
                return;
            }

            if (!GlobalVariables.SourceEngineAllowedKeys.Contains(this.TextBoxKeyBind.Text.ToLower()))
            {
                this.SpotifySwitch.TurnOff();
                MessageBox.Show("такой кнопки в кантре нет");
                return;
            }

            if (this.TextBoxKeyBind.Text == "")
            {
                this.SpotifySwitch.TurnOff();
                MessageBox.Show("Не назначена клавиша чата");
                return;
            }

            this.ButtonDo_Click(this, null); // force first request to not wait for the Thread.Sleep(1000)

			this.ChangeUIState(MainWindowUIState.NpcWork);

            string keyboardButton = this.AccountsList.SelectedItem.ToString();
            this._cancellationGetSpotifyUpdates = new CancellationTokenSource();

            var cfgWriter = new ConfigWriter(this.TextBoxKeyBind.Text, $@"{SteamIdLooker.UserdataPath}\{this.GetSelectedAccountId().ToString()}\730\local\cfg\audio.cfg");

            await Task.Factory.StartNew((/* сюда серануть keyboardButton как нибудь*/) =>
            {                           // чтобы потом его можно было использовать внутри этого блока
                while (true)
                {
                    Thread.Sleep(1000);

                    CurrentTrackResponse trackResp = Requests.GetCurrentTrack(AppInfo.SpotifyAccessToken);

                    this.Dispatcher.Invoke(() => this.UpdateInterfaceTrackInfo(trackResp));

                    if (trackResp?.Id != this.LastPlayingTrackId)
                    {
                        cfgWriter.RewriteKeyBinding(trackResp);
                        this.LastPlayingTrackId = trackResp.Id;
                    }

                    if (this._cancellationGetSpotifyUpdates.IsCancellationRequested)
                        return;

                    if (TokenExpireTime < DateTime.Now)
                    {
                        cfgWriter.RewriteKeyBinding("spotify token expired!");
                        LabelTokenExpired.Visibility = Visibility.Visible;
                        return;
                    }
                       
                }
            });
        }

		private void UpdateInterfaceTrackInfo(CurrentTrackResponse trackResp)
        {
            this.LabelWithButton.Content = this.TextBoxKeyBind.Text;

            if (trackResp == null)
            {
                this.LabelFormatted.Content = "";
                this.ButtonDo.Content = "Nothing is playing!";
                return;
            }

            if (trackResp.IsLocalFile)
				this.LabelLocalFilesWarning.Visibility = Visibility.Visible;
            else
				this.LabelLocalFilesWarning.Visibility = Visibility.Collapsed;

            this.LabelFormatted.Content = trackResp.FormattedName;
            this.ButtonDo.Content = $"{trackResp.FullName} | {trackResp.ProgressMinutes}:{trackResp.ProgressSeconds:00}/{trackResp.DurationMinutes}:{trackResp.DurationSeconds:00}";
        }

        private int GetSelectedAccountId()
        {
            SteamIdLooker.AccountNameToSteamid3.TryGetValue(this.AccountsList.SelectedItem.ToString(), out int selectedAccountId);
            return selectedAccountId;
        }

        private void ButtonPath_Click(object sender, RoutedEventArgs e)
        {
            this.TextBoxPath.Text = SteamIdLooker.UserdataPath + $@"\{this.GetSelectedAccountId().ToString()}\730\local\cfg\audio.cfg";
        }

        private void AccountsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.SpotifySwitch.Toggled && this._cancellationGetSpotifyUpdates != null)
            {
                this.SpotifySwitch.TurnOff();
                this._cancellationGetSpotifyUpdates?.Cancel();
            }
        }


		private void ChangeUIState(MainWindowUIState idle)
		{
			switch(idle)
			{
				case MainWindowUIState.NpcWork:
				{
                    this.LabelNpcDisclaimer.Visibility = Visibility.Collapsed;
                    this.TextBoxPath.Visibility        = Visibility.Collapsed;
					this.ButtonPath.Visibility	       = Visibility.Collapsed;

					this.LabelFormatted.Visibility    = Visibility.Visible;
					this.LabelWithButton.Visibility   = Visibility.Visible;
					this.LabelCurrentKey.Visibility   = Visibility.Visible;
					this.LabelCurrentTrack.Visibility = Visibility.Visible;
				}
				break;

				case MainWindowUIState.Idle:
				{
                    this.LabelNpcDisclaimer.Visibility = Visibility.Visible;
                    this.TextBoxPath.Visibility        = Visibility.Visible;
					this.ButtonPath.Visibility         = Visibility.Visible;

					this.LabelFormatted.Visibility    = Visibility.Collapsed;
					this.LabelWithButton.Visibility   = Visibility.Collapsed;
					this.LabelCurrentKey.Visibility   = Visibility.Collapsed;
					this.LabelCurrentTrack.Visibility = Visibility.Collapsed;
				}
				break;
			}
		}

        private void ButtonSourceKeys_Click(object sender, RoutedEventArgs e)
        {
            if (!ProcessWorker.IsSourceKeysFileExists())
                MessageBox.Show("не найден файл с биндами (SourceKeys.txt)");
        }
    }
}

//-_=ICON BY SCOUTPAN_=