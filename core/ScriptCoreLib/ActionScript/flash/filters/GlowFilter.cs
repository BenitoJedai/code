using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.filters
{
    [Script(IsNative = true)]
    public class GlowFilter : BitmapFilter
    {
        /// <summary>
        /// The alpha transparency value for the color.
        /// </summary>
        public double alpha { get; set; }

        /// <summary>
        /// The amount of horizontal blur.
        /// </summary>
        public double blurX { get; set; }

        /// <summary>
        /// The amount of vertical blur.
        /// </summary>
        public double blurY { get; set; }

        /// <summary>
        /// The color of the glow.
        /// </summary>
        public uint color { get; set; }

        /// <summary>
        /// Specifies whether the glow is an inner glow.
        /// </summary>
        public bool inner { get; set; }

        /// <summary>
        /// Specifies whether the object has a knockout effect.
        /// </summary>
        public bool knockout { get; set; }

        /// <summary>
        /// The number of times to apply the filter.
        /// </summary>
        public int quality { get; set; }

        /// <summary>
        /// The strength of the imprint or spread.
        /// </summary>
        public double strength { get; set; }

        public GlowFilter()
        {

        }
    }
}
