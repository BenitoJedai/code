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

//using global::System.Collections.Generic;

[assembly:
    ScriptResources("assets/mahjong/bamboo"),
    ScriptResources("assets/mahjong/characters"),
    ScriptResources("assets/mahjong/dots"),
    ScriptResources("assets/mahjong/dragons"),
    ScriptResources("assets/mahjong/flowers"),
    ScriptResources("assets/mahjong/seasons"),
    ScriptResources("assets/mahjong/special"),
    ScriptResources("assets/mahjong/winds"),

]

namespace Mahjong.js
{
    [Script]
    class Asset
    {
        public string Source = "assets/mahjong";


        public static implicit operator IHTMLImage(Asset e)
        {
            return e.Source;
        }
    }

    [Script]
    class SpecialAsset : Asset
    {

        public static implicit operator SpecialAsset(string e)
        {
            var v = new SpecialAsset();

            v.Source += "/special/" + e + ".png";

            return v;
        }
    }

    [Script]
    class BambooAsset : Asset
    {
        public static Asset[] Collection
        {
            get
            {
                return new BambooAsset[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            }
        }

        public static implicit operator BambooAsset(string e)
        {
            var v = new BambooAsset();

            v.Source += "/bamboo/" + e + ".png";

            return v;
        }
    }

    [Script]
    class CharacterAsset : Asset
    {
        public static Asset[] Collection
        {
            get
            {
                return new CharacterAsset[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", 
                               };
            }
        }

        public static implicit operator CharacterAsset(string e)
        {
            var v = new CharacterAsset();

            v.Source += "/characters/" + e + ".png";

            return v;
        }
    }

    [Script]
    class DragonAsset : Asset
    {
        public static Asset[] Collection
        {
            get
            {
                return new DragonAsset[] { "c", "f" };
            }
        }

        public static implicit operator DragonAsset(string e)
        {
            var v = new DragonAsset();

            v.Source += "/dragons/" + e + ".png";

            return v;
        }
    }

    [Script]
    class WindAsset : Asset
    {
        public static Asset[] Collection
        {
            get
            {
                return new WindAsset[] { "e", "n", "s", "w" };
            }
        }

        public static implicit operator WindAsset(string e)
        {
            var v = new WindAsset();

            v.Source += "/winds/" + e + ".png";

            return v;
        }
    }

    [Script]
    class DotsAsset : Asset
    {
        public static Asset[] Collection
        {
            get
            {
                return new DotsAsset[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            }
        }

        public static implicit operator DotsAsset(string e)
        {
            var v = new DotsAsset();

            v.Source += "/dots/" + e + ".png";

            return v;
        }
    }

    [Script]
    class SeasonAsset : Asset
    {
        public static Asset[] Collection
        {
            get
            {
                return new SeasonAsset[] { "aut", "spr", "sum", "win" };
            }
        }

        public static implicit operator SeasonAsset(string e)
        {
            var v = new SeasonAsset();

            v.Source += "/seasons/" + e + ".png";

            return v;
        }
    }

    [Script]
    class FlowerAsset : Asset
    {
        public static Asset[] Collection
        {
            get
            {
                return new FlowerAsset[] { "bam", "flum", "mum", "orc" };
            }
        }

        public static implicit operator FlowerAsset(string e)
        {
            var v = new FlowerAsset();

            v.Source += "/flowers/" + e + ".png";

            return v;
        }
    }
}
