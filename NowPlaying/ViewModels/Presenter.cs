using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowPlaying.ViewModels
{
    public class Presenter : ObservableObject
    {
        private string keyBind;

        public string KeyBind
        {
            get { return keyBind; }
            set 
            {
                keyBind = value;
                RaisePropertyChangedEvent("KeyBind");
            }
        }
    }
}
