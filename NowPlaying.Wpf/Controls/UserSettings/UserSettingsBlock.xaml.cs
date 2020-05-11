using ReactiveUI;
using NowPlaying.Wpf.Controls.Common;

namespace NowPlaying.Wpf.Controls.UserSettings
{
    public partial class UserSettingsBlockBase : ReactiveUserControl<UserSettingsBlockViewModel>
    {

    }

    public partial class UserSettingsBlock : UserSettingsBlockBase
    {
        public UserSettingsBlock()
        {

            InitializeComponent();
        }
    }
}
