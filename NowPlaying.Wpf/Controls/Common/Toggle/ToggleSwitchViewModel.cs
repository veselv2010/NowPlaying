using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NowPlaying.Wpf.Controls.Common.Toggle
{
    public class ToggleSwitchViewModel : ReactiveObject
    {
        [Reactive] public bool IsToggled { get; set; }
    }
}
