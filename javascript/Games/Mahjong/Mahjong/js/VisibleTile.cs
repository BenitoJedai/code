//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

using global::System.Collections.Generic;
using System.Linq;


namespace Mahjong.js
{
    [Script]
    class VisibleTile
    {
        public readonly TileInfo Info;
        public readonly Asset.Settings Settings;

        public readonly IHTMLDiv Background = new IHTMLDiv();
        public readonly IHTMLDiv Display = new IHTMLDiv();




        public VisibleTile(TileInfo Info, Asset.Settings Settings)
        {
            this.Settings = Settings;
            this.Info = Info;

            this.Display.style.SetLocation(
                Settings.OuterWidth - Settings.InnerWidth - 1, 1, Settings.InnerWidth, Settings.InnerHeight);

            Info.Image.ToBackground(this.Display.style);

            this.Background.style.SetSize(Settings.OuterWidth, Settings.OuterHeight);
            this.Background.appendChild(this.Display);

            Settings.BackgroundTile.ToBackground(Background);
        }

        public bool IsMatch(VisibleTile a)
        {
            if (a == null)
                return false;

            // rule 1 - rank and suit must match

            if (a.Info.Asset.Rank != this.Info.Asset.Rank)
                return false;

            if (a.Info.Asset.Suit != this.Info.Asset.Suit)
                return false;

            return true;
        }

        public void Hide()
        {
            this.Background.Dispose();
        }
    }

}
