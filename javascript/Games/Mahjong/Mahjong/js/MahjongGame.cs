//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

using global::System.Collections.Generic;
using System.Linq;
using System;



namespace Mahjong.js
{
    [Script]
    class LoadedAsset
    {
        public IHTMLImage Image;

        public RankAsset Asset;

        public static implicit operator LoadedAsset(RankAsset e)
        {
            return new LoadedAsset { Asset = e, Image = e };
        }
    }



    [Script, ScriptApplicationEntryPoint]
    public class MahjongGame
    {
        public MahjongGame()
        {
            Native.Document.body.style.backgroundColor = Color.Gray;

            var s = new Asset.Settings();

            var last = default(VisibleTile);

            #region CreateTile
            Func<int, int, LoadedAsset, VisibleTile> CreateTile =
                (x, y, i) =>
                {
                    var a = new VisibleTile(i, s);


                    a.Background.AttachToDocument();
                    a.Background.SetCenteredLocation(x, y);

                    a.Background.onclick += delegate
                    {
                        var next = a;

                        if (last != null)
                        {
                            if (last.IsMatch(a))
                            {
                                System.Console.WriteLine("match!");

                                last.Hide();
                                a.Hide();

                                next = null;
                            }
                        }

                        System.Console.WriteLine("click: " + a.Info.Asset.Suit + " / " + a.Info.Asset.Rank);

                        last = next;
                    };

                    a.Background.onmouseover +=
                        delegate { a.Background.style.Opacity = 0.8; };


                    a.Background.onmouseout +=
                        delegate { a.Background.style.Opacity = 1; };

                    return a;
                };
            #endregion



            Action<int, IEnumerable<RankAsset>> CreateTiles =
                (y, a) =>
                {
                    int c = 0;



                    foreach (var v in a.AsEnumerable())
                    {


                        c++;

                        CreateTile((s.OuterWidth + 2) * c, (s.OuterHeight + 2) * y, (LoadedAsset)v);
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


            var stack1 = stuff.Randomize();


            CreateTile(220, 220, stack1.ElementAt(0));



        }




        static MahjongGame()
        {
            typeof(MahjongGame).Spawn();

        }


    }

}
