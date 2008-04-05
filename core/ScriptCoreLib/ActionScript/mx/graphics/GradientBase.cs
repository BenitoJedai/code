using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.mx.graphics
{
    /// <summary>
    /// http://livedocs.adobe.com/flex/3/langref/mx/graphics/GradientBase.html
    /// </summary>
    [Script(IsNative=true)]
    public class GradientBase : EventDispatcher
    {
        /// <summary>
        /// An Array of GradientEntry objects defining the fill patterns for the gradient fill.
        /// </summary>
        public GradientEntry[] entries {get; set; }

    }
}
