using NowPlaying.Wpf.Themes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NowPlaying.Wpf.Controls.Header
{
    public class HeaderViewModel : ReactiveObject
    {
        [Reactive] public Theme Theme { get; set; }
    }
}
