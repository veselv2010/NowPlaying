using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NowPlaying.Wpf.Controls.UserSettings.Controls
{
    public class CurrentKeyControlViewModel : ReactiveObject
    {
        [Reactive] public string CurrentKey { get; set; }
    }
}
