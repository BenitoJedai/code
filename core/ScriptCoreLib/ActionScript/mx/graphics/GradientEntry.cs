using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.mx.graphics
{
    /// <summary>
    /// http://livedocs.adobe.com/flex/3/langref/mx/graphics/GradientEntry.html
    /// </summary>
    [Script(IsNative = true)]
    public class GradientEntry : EventDispatcher
    {
        public GradientEntry()
        {

        }

        public GradientEntry(uint color, double ratio, double alpha)
        {

        }

        /// <summary>
        /// The transparency of a gradient fill.
        /// </summary>
        public double alpha { get; set; }

        /// <summary>
        /// The color value for a gradient fill.
        /// </summary>
        public uint color { get; set; }

        /// <summary>
        /// Where in the graphical element, as a percentage from 0.0 to 1.0, Flex starts the transition to the associated color.
        /// </summary>
        public double ratio { get; set; }


    }
}
