using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

namespace MineSweeper.js
{
    public sealed class MineSweeperSettings
    {
        public string X = "8";
        public string Y = "8";
        public string Mines = "0.2";
    }

    public class MineSweeperGame
    {
        public static readonly MineSweeperSettings DefaultData = new MineSweeperSettings();

        public readonly MineSweeperSettings Data;

       public  readonly MineSweeperPanel Panel;

        public MineSweeperGame(MineSweeperSettings _Data = null, IHTMLElement _Owner = null)
        {
            this.Data = _Data;

            if (this.Data == null)
                this.Data = DefaultData;

            var Settings = new
            {
                X = this.Data.X.ToInt32(8),
                Y = this.Data.Y.ToInt32(8),
                Mines = this.Data.Mines.ToDouble(0.2)
            };


            Panel = new MineSweeperPanel(
                Settings.X,
                Settings.Y,
                Settings.Mines,
                new Assets()
            );

            if (_Owner == null)
                Panel.Control.AttachToDocument();
            else
                _Owner.replaceWith(Panel.Control);
        }
    }
}
