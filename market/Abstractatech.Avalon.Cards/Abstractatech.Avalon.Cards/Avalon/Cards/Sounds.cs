using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Avalon.Cards
{
    public class Sounds
    {
        public static class Assets
        {
            public static string deal { get { return "assets/Abstractatech.Avalon.Cards/deal.mp3"; } }
            public static string click { get { return "assets/Abstractatech.Avalon.Cards/click.mp3"; } }
            public static string drag { get { return "assets/Abstractatech.Avalon.Cards/drag.mp3"; } }
            public static string win { get { return "assets/Abstractatech.Avalon.Cards/win.mp3"; } }
        }

        public Action deal;
        public Action click;
        public Action drag;
        public Action win;

        public Sounds()
        {
            this.deal = delegate { };
            this.click = delegate { };
            this.drag = delegate { };
            this.win = delegate { };
        }
    }
}
