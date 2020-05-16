using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using NowPlaying.Wpf.Themes;

namespace NowPlaying.Wpf.Controls.UserSettings
{
    public class UserSettingsBlockViewModel : ReactiveObject
    {
        [Reactive] public Theme Theme { get; set; }
    }
}
