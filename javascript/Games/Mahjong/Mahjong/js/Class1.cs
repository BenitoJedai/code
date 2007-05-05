//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Query;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

using global::System.Collections.Generic;



namespace Mahjong.js
{
    [Script]
    class TileInfo
    {
        public IHTMLImage Image;
    }

    [Script]
    class Tile
    {
        public readonly TileInfo Info;
        public readonly Asset.Settings Settings;

        public readonly IHTMLDiv Background = new IHTMLDiv();
        public readonly IHTMLDiv Display = new IHTMLDiv();




        public Tile(TileInfo Info, Asset.Settings Settings)
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
    }

    [Script]
    public class Class1
    {
        public const string Alias = "Class1";
        public const string DefaultData = "Class1Data";

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {
            Native.Document.body.style.backgroundColor = Color.Gray;

            var s = new Asset.Settings();

            #region CreateTile
            Func<int, int, TileInfo, Tile> CreateTile =
                (x, y, i) =>
                {
                    var a = new Tile(i, s);

                    a.Background.attachToDocument();
                    a.Background.SetCenteredLocation(x, y);

                    a.Background.onmouseover +=
                        delegate { a.Background.style.Opacity = 0.8; };


                    a.Background.onmouseout +=
                        delegate { a.Background.style.Opacity = 1; };

                    return a;
                };
            #endregion


         
            

            Action<int, IEnumerable<Asset>> CreateTiles =
                (y, a) =>
                {
                    int c = 0;



                    foreach (var v in a.AsEnumerable())
                    {
                        var i = new TileInfo { Image = v.Source };

                        c++;

                        CreateTile((s.OuterWidth + 2) * c, (s.OuterHeight + 2) * y, i);
                    }
                };

            var stuff = Asset.Bamboo.
                            Concat(Asset.Characters).
                            Concat(Asset.Dots).
                            Concat(Asset.Dragons).
                            Concat(Asset.Flowers).
                            Concat(Asset.Seasons).
                            Concat(Asset.Winds);




            CreateTiles(1, Asset.Bamboo);
            CreateTiles(2, Asset.Dots);
            CreateTiles(3, Asset.Characters);
            CreateTiles(4, Asset.Winds);
            CreateTiles(5, Asset.Dragons);
            CreateTiles(6, Asset.Seasons);
            CreateTiles(7, Asset.Flowers);
            CreateTiles(8, stuff.Randomize());
            CreateTiles(9, stuff.Randomize());


            



        }




        static Class1()
        {
            //Console.EnableActiveXConsole();

            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );

        }


    }

}
