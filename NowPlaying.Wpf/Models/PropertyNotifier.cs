using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NowPlaying.Wpf.Models
{
    public abstract class PropertyNotifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
