using ReactiveUI;
using System.Windows.Input;
using NowPlaying.Core.InputSender;
using System.Windows;

namespace NowPlaying.Wpf.Controls.UserSettings.Controls
{
    public partial class CurrentKeyControlBase : ReactiveUserControl<CurrentKeyControlViewModel>
    {

    }

    public partial class CurrentKeyControl : CurrentKeyControlBase
    {
        private IInputSender inputSender = new InputSenderWindows();
        public CurrentKeyControl()
        {
            InitializeComponent(); 
        }

        private void KeyControlMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.CurrentKeyTextBlock.KeyDown += KeyboardKeyDown;
        }

        private void KeyboardKeyDown(object sender, KeyEventArgs e)
        {
            ushort virtualKey = (ushort)KeyInterop.VirtualKeyFromKey(e.Key);
            string sourceKey = inputSender.GetSourceKey(virtualKey);
            this.ViewModel.CurrentSourceKey = sourceKey;
            this.CurrentKeyTextBlock.KeyDown -= KeyboardKeyDown;
            MessageBox.Show("keydown fired");
        }
    }
}
