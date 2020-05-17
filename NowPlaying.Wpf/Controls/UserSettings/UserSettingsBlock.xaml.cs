using ReactiveUI;
using NowPlaying.Wpf.Controls.UserSettings.Controls;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Input;
using NowPlaying.Core.InputSender;

namespace NowPlaying.Wpf.Controls.UserSettings
{
    public partial class UserSettingsBlockBase : ReactiveUserControl<UserSettingsBlockViewModel>
    {

    }

    public partial class UserSettingsBlock : UserSettingsBlockBase
    {
        private IInputSender inputSender = new InputSenderWindows();
        public UserSettingsBlock()
        {
            ViewModel = new UserSettingsBlockViewModel();
            InitializeComponent();

            this.WhenActivated(d => {
                this.OneWayBind(ViewModel, vm => vm.CurrentSourceKey, v => v.CurrentKeyControl.CurrentKeyTextBlock.Text)
                    .DisposeWith(d);
            });
        }

        private void CurrentKeyControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += KeyboardKeyDown;
            this.ViewModel.CurrentSourceKey = "press bind button";
        }

        private void KeyboardKeyDown(object sender, KeyEventArgs e)
        {
            CurrentKeyControl.MouseLeftButtonDown -= CurrentKeyControl_MouseLeftButtonDown;

            ushort virtualKey = (ushort)KeyInterop.VirtualKeyFromKey(e.Key);
            string sourceKey = inputSender.GetSourceKey(virtualKey);

            this.ViewModel.CurrentSourceKey = sourceKey;

            Window.GetWindow(this).KeyDown -= KeyboardKeyDown;
            CurrentKeyControl.MouseLeftButtonDown += CurrentKeyControl_MouseLeftButtonDown;
        }
    }
}
