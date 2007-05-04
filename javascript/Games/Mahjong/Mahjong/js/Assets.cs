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



namespace Mahjong.js
{
    [Script]
    class Asset
    {
        public string Source = "assets";

        
    }

    [Script]
    class BambusAsset : Asset
    {
        public static Asset[] Collection
        {
            get
            {
                return new BambusAsset[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            }
        }

        public static implicit operator BambusAsset(string e)
        {
            var v = new BambusAsset();

            v.Source += "/bambus/" + e + ".png";

            return v;
        }
    }

    [Script]
    class GlyphAsset : Asset
    {
        public static Asset[] Collection
        {
            get
            {
                return new GlyphAsset[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", 
                               "c", "e", "f", "n", "s", "w" };
            }
        }

        public static implicit operator GlyphAsset(string e)
        {
            var v = new GlyphAsset();

            v.Source += "/glyphs/" + e + ".png";

            return v;
        }
    }

    [Script]
    class RingAsset : Asset
    {
        public static Asset[] Collection
        {
            get
            {
                return new RingAsset[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            }
        }

        public static implicit operator RingAsset(string e)
        {
            var v = new RingAsset();

            v.Source += "/rings/" + e + ".png";

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
