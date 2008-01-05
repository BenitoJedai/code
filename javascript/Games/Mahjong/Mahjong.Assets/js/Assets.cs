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

using System.Linq;

//using global::System.Collections.Generic;

[assembly:
    ScriptResources("assets/Mahjong/bamboo"),
    ScriptResources("assets/Mahjong/characters"),
    ScriptResources("assets/Mahjong/dots"),
    ScriptResources("assets/Mahjong/dragons"),
    ScriptResources("assets/Mahjong/flowers"),
    ScriptResources("assets/Mahjong/seasons"),
    ScriptResources("assets/Mahjong/special"),
    ScriptResources("assets/Mahjong/winds"),

]

namespace Mahjong.js
{
    [Script]
    public class RankAsset : Asset
    {
        public string Suit;
        public string Rank;

        public override IHTMLImage ToImage()
        {
            return this.Source + "/" + this.Suit + "/" + this.Rank + ".png";
        }
    }

    [Script]
    public abstract class Asset
    {
        protected string Source = "assets/Mahjong";


        [Script]
        public class Settings
        {
            public int OuterWidth = 32;
            public int OuterHeight = 46;

            public int InnerWidth = 24;
            public int InnerHeight = 38;

            public IHTMLImage BackgroundTile = (SpecialAsset)"tile0";
        }
        
        static public RankAsset[] Bamboo { get { return BambooAsset.Collection; } }
        static public RankAsset[] Characters { get { return CharacterAsset.Collection; } }
        static public RankAsset[] Dots { get { return DotsAsset.Collection; } }
        static public RankAsset[] Dragons { get { return DragonAsset.Collection; } }
        static public RankAsset[] Flowers { get { return FlowerAsset.Collection; } }
        static public RankAsset[] Seasons { get { return SeasonAsset.Collection; } }
        static public RankAsset[] Winds { get { return WindAsset.Collection; } }

        public static implicit operator IHTMLImage(Asset e)
        {
            return e.ToImage();
        }

        public abstract IHTMLImage ToImage();
    }

    [Script]
    class SpecialAsset : RankAsset
    {
        public SpecialAsset()
        {
            this.Suit = "special";
        }

        public static implicit operator SpecialAsset(string Rank)
        {
            return new SpecialAsset { Rank = Rank };
        }
    }

    [Script]
    class BambooAsset : RankAsset
    {
        public BambooAsset()
        {
            this.Suit = "bamboo";
        }
        public static RankAsset[] Collection
        {
            get
            {
                return new BambooAsset[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            }
        }

        public static implicit operator BambooAsset(string Rank)
        {
            return new BambooAsset { Rank = Rank };
        }
    }

    [Script]
    class CharacterAsset : RankAsset
    {
        public CharacterAsset()
        {
            this.Suit = "characters";
        }

        public static RankAsset[] Collection
        {
            get
            {
                return new CharacterAsset[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", 
                               };
            }
        }

        public static implicit operator CharacterAsset(string Rank)
        {
            return new CharacterAsset { Rank = Rank };
        }
    }

    [Script]
    class DragonAsset : RankAsset
    {
        public DragonAsset()
        {
            this.Suit = "dragons";
        }

        public static RankAsset[] Collection
        {
            get
            {
                return new DragonAsset[] { "c", "f" };
            }
        }

        public static implicit operator DragonAsset(string Rank)
        {
            return new DragonAsset { Rank = Rank };
        }
    }

    [Script]
    class WindAsset : RankAsset
    {
        public WindAsset()
        {
            this.Suit = "winds";
        }

        public static RankAsset[] Collection
        {
            get
            {
                return new WindAsset[] { "e", "n", "s", "w" };
            }
        }

        public static implicit operator WindAsset(string Rank)
        {
            return new WindAsset { Rank = Rank };
        }
    }

    [Script]
    class DotsAsset : RankAsset
    {
        public DotsAsset()
        {
            this.Suit = "dots";
        }

        public static RankAsset[] Collection
        {
            get
            {
                return new DotsAsset[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            }
        }

        public static implicit operator DotsAsset(string Rank)
        {
            return new DotsAsset { Rank = Rank };
        }
    }

    [Script]
    class SeasonAsset : RankAsset
    {
        public SeasonAsset()
        {
            this.Suit = "seasons";
        }

        public static RankAsset[] Collection
        {
            get
            {
                return new SeasonAsset[] { "aut", "spr", "sum", "win" };
            }
        }

        public static implicit operator SeasonAsset(string Rank)
        {
            return new SeasonAsset { Rank = Rank };
        }
    }

    [Script]
    class FlowerAsset : RankAsset
    {
        public FlowerAsset()
        {
            this.Suit = "flowers";
        }

        public static RankAsset[] Collection
        {
            get
            {
                return new FlowerAsset[] { "bam", "flum", "mum", "orc" };
            }
        }

        public static implicit operator FlowerAsset(string Rank)
        {
            return new FlowerAsset { Rank = Rank };
        }
    }
}
