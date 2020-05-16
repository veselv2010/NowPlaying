using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows.Input;

namespace NowPlaying.Wpf.Controls.UserSettings.Controls
{
    public class CurrentKeyControlViewModel : ReactiveObject
    {
        [Reactive] public string CurrentSourceKey { get; set; }
    }
}
