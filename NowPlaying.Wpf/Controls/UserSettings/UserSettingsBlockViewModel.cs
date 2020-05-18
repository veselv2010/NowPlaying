using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NowPlaying.Wpf.Controls.UserSettings
{
    public class UserSettingsBlockViewModel : ReactiveObject
    {
        [Reactive] public string CurrentSourceKey { get; set; }
    }
}
