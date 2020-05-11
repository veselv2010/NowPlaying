using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NowPlaying.Wpf.Controls.Common
{
    public class CustomCheckboxViewModel : ReactiveObject
    {
        [Reactive] public bool IsChecked { get; set; }
    }
}
