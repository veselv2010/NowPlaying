using ReactiveUI;

namespace NowPlaying.Wpf.Controls.Common.Toggle
{
    public abstract class ToggleBase : ReactiveUserControl<ToggleViewModel>
    {
        public bool IsToggled { get => ViewModel.IsToggled; set => Toggle(value); }

        public void Toggle(bool? newToggled = null)
        {
            ViewModel.IsToggled = newToggled ?? !ViewModel.IsToggled;

            System.Console.WriteLine(ViewModel.IsToggled);
        }
    }
}
