using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.mx.graphics
{
    // http://livedocs.adobe.com/flex/3/langref/mx/graphics/LinearGradient.html
    [Script(IsNative = true)]
    public class LinearGradient : GradientBase
    {
        /// <summary>
        /// Controls the transition direction.
        /// </summary>
        public double angle { get; set; }

        /// <summary>
        /// Starts the fill.
        /// </summary>
        public void begin(Graphics target, Rectangle rc)
        {
        }

        /// <summary>
        /// Ends the fill.
        /// </summary>
        public void end(Graphics target)
        {
        }


    }
}
