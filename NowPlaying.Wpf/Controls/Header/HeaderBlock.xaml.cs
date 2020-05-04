using NowPlaying.Wpf.Themes;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NowPlaying.Wpf.Controls.Header
{
    public class HeaderBlockBase : ReactiveUserControl<HeaderViewModel>
    {
        // https://reactiveui.net/api/reactiveui/reactiveusercontrol_1/#Remarks
    }

    public partial class HeaderBlock : HeaderBlockBase
    {
        public HeaderBlock()
        {
            ViewModel = new HeaderViewModel();

            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel,
                        vm => vm.Theme,
                        view => view.ToggleTheme.ViewModel.IsToggled,
                        ThemeToBool, BoolToTheme)
                    .DisposeWith(d);

                //this.OneWayBind(ViewModel, vm => vm.Theme, v => v.HelpTextBlock.Text, theme => theme.ToString())
                //    .DisposeWith(d);
            });
        }

        private readonly Theme ToggledTheme = Theme.Black;

        private bool ThemeToBool(Theme theme) => theme == ToggledTheme;
        private Theme BoolToTheme(bool isToggled) => isToggled ? ToggledTheme : Theme.White;

        private void CloseButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // TODO: вроде это не оч
            System.Environment.Exit(0);
        }
    }
}
