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
    [Script, Serializable]
    public sealed class MineSweeperSettings
    {
        public string X = "8";
        public string Y = "8";
        public string Mines = "0.2";
        public string AssetsPath = Assets.DefaultPath;
    }

    [Script, ScriptApplicationEntryPoint]
    public class MineSweeperGame
    {
        public static readonly MineSweeperSettings DefaultData = new MineSweeperSettings();

        public readonly MineSweeperSettings Data;

        static MineSweeperGame()
        {
            var KnownTypes = new object[] { 
                    new MineSweeperSettings(), 
                };

            typeof(MineSweeperGame).SpawnTo<MineSweeperSettings>(KnownTypes, (data, owner) => new MineSweeperGame(data, owner));
        }

        readonly MineSweeperPanel Panel;

        public MineSweeperGame(MineSweeperSettings _Data, IHTMLElement _Owner)
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
                new Assets(this.Data.AssetsPath)
            );

            _Owner.replaceWith(Panel.Control);
        }
    }
}
