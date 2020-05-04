using HelloWorldRUI.Themes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace HelloWorldRUI.Controls.Header
{
    public class HeaderViewModel : ReactiveObject
    {
        [Reactive] public Theme Theme { get; set; }
    }
}
