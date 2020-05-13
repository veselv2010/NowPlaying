using ReactiveUI;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Media;

namespace NowPlaying.Wpf.Controls
{
    public partial class NpcWorkSwitch
    {
        public NpcWorkSwitch()
        {
            var toggleColors = new Dictionary<bool, SolidColorBrush>
            {
                { false, ColorsConstants.BrightRed },
                { true, ColorsConstants.SpotifyGreen },
            };

            InitializeComponent();

            NpcToggle.WhenActivated(d =>
            {
                NpcToggle.OneWayBind(NpcToggle.ViewModel,
                    vm => vm.IsToggled,
                    view => view.RectangleToggle.Fill,
                    IsToggled => toggleColors[IsToggled])
                .DisposeWith(d);
            });
        }
    }
}
