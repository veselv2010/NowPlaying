using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowPlaying.Core.Steam
{
    public sealed class User
    {
        public string Name { get; }
        public int SteamId3 { get; }

        public User(string name, int steamId32)
        {
            Name = name;
            SteamId3 = steamId32;
        }
    }
}
